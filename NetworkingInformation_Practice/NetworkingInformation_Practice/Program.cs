using System.Net;
using System.Net.NetworkInformation;

namespace NetworkingInformation_Practice
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            NetworkInterface[] card = NetworkInterface.GetAllNetworkInterfaces();

            Console.WriteLine("Printing all NIC Info:");
            foreach (var currentCard in card)
            {
                if (currentCard.GetIPProperties().UnicastAddresses.Count > 0)
                {
                    var routerIp = currentCard.GetIPProperties().GatewayAddresses.FirstOrDefault()?.Address;
                    var ipAddresses = currentCard.GetIPProperties().UnicastAddresses.LastOrDefault()?.Address;
                    Console.WriteLine($"NIC: {currentCard.Name}");
                    Console.WriteLine($"\tInterface Type: {currentCard.NetworkInterfaceType}");
                    Console.WriteLine($"\tMTU: {currentCard.GetIPProperties().GetIPv4Properties().Mtu}");
                    Console.WriteLine($"\tStatus: {currentCard.OperationalStatus}");
                    Console.WriteLine($"\tInterface Type: {currentCard.NetworkInterfaceType}");
                    Console.WriteLine($"\tDescription: {currentCard.Description}");
                    Console.WriteLine($"\tSpeed: {currentCard.Speed} bits per second");
                    Console.WriteLine($"\tMAC Address: {currentCard.GetPhysicalAddress()}");
                    Console.WriteLine($"\tRouter (Gateway) IP: {routerIp}");
                    Console.WriteLine($"\tIP Address: {ipAddresses}");
                    Console.WriteLine();
                }
            }

            string baseIp = "192.168.86.";
            Task<string>[] tasks = new Task<string>[254];

            Console.WriteLine("Beginning to ping network devices...");
            for (int i = 1; i <= 254; i++)
            {
                string ip = baseIp + i;
                tasks[i - 1] = PingAddressAsync(ip);
            }

            await Task.WhenAll(tasks);
            Console.WriteLine("Scan complete\n");

            foreach(var task in tasks)
            {
                if (!string.IsNullOrEmpty(task.Result))
                {
                    await GetHostnameAsync(task.Result);
                }
            }
        }

        static async Task<string> PingAddressAsync(string ip)
        {
            using var ping = new Ping();
            try
            {
                PingReply reply = await ping.SendPingAsync(ip);
                if (reply.Status == IPStatus.Success)
                {
                    Console.WriteLine($"Found Active Device: {ip}");
                    return ip;
                }
                return string.Empty;
            }
            catch 
            {
                return string.Empty;
            }
        }

        static async Task GetHostnameAsync(string ip)
        {
            try
            {
                var entry = await Dns.GetHostEntryAsync(ip);
                Console.WriteLine($"IP: {ip, -15} -> Hostname: {entry.HostName}");
            }
            catch
            {
                Console.WriteLine($"IP: {ip, -15} -> Hostname: Unknown (DNS blocked)");
            }
        }
    }
}