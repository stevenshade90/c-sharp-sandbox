namespace ASP.NET_Practice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Determines how the web app is configured (runtime behavior, services, logging, etc.)
            // Sets the Kestrel web server by default
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            WebApplication app = builder.Build();


            // Setting middleware
            app.UseDeveloperExceptionPage();;
            app.UseStaticFiles();
            app.UseRouting();

            var people = new List<Person>
            {
                new Person("Tim", "Huddel", 55),
                new Person("Tom", "Hanks", 45),
                new Person("Will", "Ferrel", 50),
                new Person("Marty", "McFly", 60),
            };


            // MapGet() is a method that defines an endpoint, typically called after MW and before Run()
            app.MapGet("/", () => "Hello World!");

            app.MapGet("/Error", () => "An error occurred.");

            app.MapGet("/person/{name}", (string name) =>
                people.Where(x => x.FirstName.ToLower().StartsWith(name.ToLower())));

            app.MapGet("/agesearch/{age}", (int age) =>
                people.Where(x => x.Age >= age));

            // Only now does the web application start and begin listening for incoming HTTP requests
            // It is NOT listening for requests during the WebApplicationBuilder or WebApplication phases
            app.Run();
        }
    }

    public class Person(string first, string last, int age)
    {
        public string FirstName { get; set; } = first;
        public string LastName { get; set; } = last;
        public int Age { get; set; } = age;
    }
}