namespace Enum_Practice
{
    class Program
    {
        const int MAX_CUST = 50;

        static void Main(string[] args)
        {
            Console.WriteLine("=== Testing BankAccount and associated enum (AccountStatus) ===");
            BankAccount[] Clients = new BankAccount[MAX_CUST];
            BankAccount stevesBank = new BankAccount() { 
                Name = "Steve",
                AccountNum = 12345612,
                Balance = 10_000,
                AccountStatus = AccountStatus.Active
            };
            Clients[0] = stevesBank;

            LoopDisplay(Clients);
            Console.WriteLine();

            Console.WriteLine("=== Testing DogInfo and associated enum (DogPurchaseState) ===");
            DogInfo[] Dogs = new DogInfo[2];
            DogInfo dog1 = new DogInfo()
            {
                PaymentStatus = DogPurchaseState.Paid,
                Breed = "Jack Russel",
                Name = "Rosie",
                Age = 10
            };

            DogInfo dog2 = new DogInfo()
            {
                PaymentStatus = DogPurchaseState.Holding,
                Breed = "Golden Retriever",
                Name = "Brooklyn",
                Age = 3,
            };

            Dogs[0] = dog1;
            Dogs[1] = dog2;

            LoopDisplay<DogInfo>(Dogs);
        }

        static void LoopDisplay<T>(IEnumerable<T> array)
        {
            if (array is DogInfo[] dog)
            {
                foreach (var currentDog in dog)
                {
                    DogInfo(currentDog);
                    Console.WriteLine();
                }
            }
            else if (array is BankAccount[] account)
            {
                foreach (var currentAccount in account)
                {
                    if (currentAccount.Name == null) continue;
                    PrintAccount(currentAccount);
                    Console.WriteLine();
                }
            }
        }

        static void PrintAccount(BankAccount account)
        {
            Console.WriteLine($"Name: {account.Name}");
            Console.WriteLine($"Account Number: {account.AccountNum}");
            Console.WriteLine($"Balance: ${account.Balance: #,##0.00}");
            Console.WriteLine($"Account Status: {account.AccountStatus}");
        }

        static void DogInfo(DogInfo dog)
        {
            Console.WriteLine($"Payment Status: {dog.PaymentStatus.ToString().ToUpper()}");
            Console.WriteLine($"Breed: {dog.Breed}");
            Console.WriteLine($"Name: {dog.Name}");
            Console.WriteLine($"Age: {dog.Age}");
        }
    }

    struct BankAccount
    {
        public AccountStatus AccountStatus { get; set; }
        public string Name { get; set; }
        public int AccountNum { get; set; }
        public int Balance { get; set; }
    }

    enum AccountStatus
    {
        Active,
        Inactive,
        Closed
    };

    struct DogInfo
    {
        public DogPurchaseState PaymentStatus;
        public string Breed;
        public string Name;
        public int Age;
    };

    enum DogPurchaseState
    {
        Paid,
        Unpaid,
        Holding
    };
}