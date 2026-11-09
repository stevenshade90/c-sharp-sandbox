using Azure.Identity;
using Azure.ResourceManager;

namespace AzureIntegration_Practice
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string tenantId = "";
            var credential = GetInteractiveBrowserCredential(tenantId);
            await AttemptToLoginToTenant(credential);
        }

        static InteractiveBrowserCredential GetInteractiveBrowserCredential(string tenantId)
        {
            var loginOptions = new InteractiveBrowserCredentialOptions
            {
                TenantId = tenantId,
            };
            return new InteractiveBrowserCredential(loginOptions);
        }

        static async Task AttemptToLoginToTenant(InteractiveBrowserCredential credential)
        {
            Console.WriteLine("Opening browser to log you into Azure...");

            try
            {
                ArmClient armClient = new ArmClient(credential);
                Azure.ResourceManager.Resources.TenantResource myTenant = await armClient.GetTenants().FirstAsync();

                Console.WriteLine($"Successfully connected to Tenant ID: {myTenant.Data.TenantId}");

                await foreach (var sub in myTenant.GetSubscriptions().GetAllAsync())
                {
                    Console.WriteLine($"Found Subscription: {sub.Data.DisplayName} ({sub.Data.SubscriptionId})");
                    
                    await foreach (var rg in sub.GetResourceGroups().GetAllAsync())
                    {
                        Console.WriteLine($"\tFound Resource Group: {rg.Data.Name}");
                        
                        foreach (var resource in rg.GetGenericResources())
                        {
                            Console.WriteLine($"\t\tFound Resource: {resource.Data.Name} -- ID: {resource.Data.Id} -- Type: {resource.Data.ResourceType}");
                        }
                    }
                }   
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login failed: {ex.Message}");
            }
        }
    }
}

//foreach (var tag in sub.GetAllPredefinedTags())
//{
//    Console.WriteLine(tag.TagName);
//}
//                            await resource.DeleteAsync(Azure.WaitUntil.Completed);