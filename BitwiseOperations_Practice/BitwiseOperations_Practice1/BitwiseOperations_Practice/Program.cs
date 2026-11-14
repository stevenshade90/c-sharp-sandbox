
[Flags]
public enum BikeBrands
{
    Felt=1,
    Giant=2,
    Trek=4,
    Cannondale=8,
    Fuji=16
}

namespace BitwiseOperations_Practice
{
    internal class program
    {
        static void Main()
        {
            BikeBrands purchasedBikes = BikeBrands.Felt | BikeBrands.Giant | BikeBrands.Fuji;
            BikeBrands purchasedBikes2 = BikeBrands.Giant | BikeBrands.Cannondale;

            BikePurchaseCheck(purchasedBikes);
            BikePurchaseCheck(purchasedBikes2);
        }

        public static void EnumReturn (System.Enum e)
        {
            Array enumValues = Enum.GetValues(e.GetType());

            for (int i = 0; i < enumValues.Length; i++)
            {
                Console.WriteLine("{0} : {0:D}", enumValues.GetValue(i));
            }
        }

        public static void BikePurchaseCheck (BikeBrands e)
        {
            Console.WriteLine("Felt: {0}", (e | BikeBrands.Felt) == e);
            Console.WriteLine("Giant: {0}", (e | BikeBrands.Giant) == e);
            Console.WriteLine("Trek: {0}", (e | BikeBrands.Trek) == e);
            Console.WriteLine("Cannondale: {0}", (e | BikeBrands.Cannondale) == e);
            Console.WriteLine("Fuji: {0}", (e | BikeBrands.Fuji) == e);
            Console.WriteLine();
        }
    }
}