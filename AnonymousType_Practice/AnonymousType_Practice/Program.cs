namespace AnonymousType_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BuildAnonymousType("George Orwell", "1984", 1949, 9.99);
            BuildAnonymousType("Harper Lee", "To Kill a Mockingbird", 1960, 7.99);
            BuildAnonymousType("Stephen King", "The Shining", 1977, 8.99);
        }

        static void BuildAnonymousType(string author, string book, int publishedYear, double price)
        {
            var bookInfo = new
            {
                DatePurchased = DateTime.Now,
                Author = author,
                Book = book,
                PublishedYear = publishedYear,
                Price = price
            };
            Console.WriteLine($"Author: {bookInfo.Author}");
            Console.WriteLine($"Date Purchased: {bookInfo.DatePurchased}");
            Console.WriteLine($"Book: {bookInfo.Book}");
            Console.WriteLine($"Published Year: {bookInfo.PublishedYear}");
            Console.WriteLine($"Price: ${bookInfo.Price}");

            ObjectInfo(bookInfo);
            Console.WriteLine();
        }

        static void ObjectInfo(object o)
        {
            Console.WriteLine($"ToString() => {o.ToString()}");
            Console.WriteLine($"GetType() => {o.GetType()}");
            Console.WriteLine($"GetType().BaseType => {o.GetType().BaseType}");
            Console.WriteLine($"GetType().Name => {o.GetType().Name}");
        }
    }
}