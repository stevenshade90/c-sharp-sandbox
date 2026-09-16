using System.Net.Mime;

namespace ASP.NET_Practice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Handlers handlers = new();

            Fruit.All.Add("1", new Fruit("Banana", 100));
            Fruit.All.Add("2", new Fruit("Orange", 1500));

            // Determines how the web app is configured (runtime behavior, services, logging, etc.)
            // Sets the Kestrel web server by default
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            WebApplication app = builder.Build();


            app.MapGet("/", () => "Goodbye, world!!\nAnd also hello!");

            app.MapGet("/fruit", () => Fruit.All);

            //var getFruit = (string id) => Fruit.All[id];
            app.MapGet("/fruit/{id}", (string id) => 
                Fruit.All.TryGetValue(id, out var fruit)
                ? TypedResults.Ok(fruit)
                : Results.Problem(statusCode: 404));

            app.MapGet("/vegetables", (HttpResponse response) =>
            {
                response.StatusCode = 418;
                response.ContentType = MediaTypeNames.Text.Plain;
                response.WriteAsync("Im a veggie...\n");

                return response.WriteAsync("I'm a veggie!");
            });

            //app.MapPost("/fruit/{id}", handlers.AddFruit);

            //app.MapPut("/fruit{id}", handlers.ReplaceFruit);

            //app.MapDelete("/fruit/{id}", DeleteFruit);

            // Only now does the web application start and begin listening for incoming HTTP requests
            // It is NOT listening for requests during the WebApplicationBuilder or WebApplication phases
            app.Run();

            void DeleteFruit(string id)
            {
                Fruit.All.Remove(id);
            }
        }
    }

    record Fruit(string Name, int Stock)
    {
        public static readonly Dictionary<string, Fruit> All = new();
    }
    class Handlers
    {
        public void ReplaceFruit(string id, Fruit fruit)
        {
            Fruit.All[id] = fruit;
        }

        public void AddFruit(string id, Fruit fruit)
        {
            Fruit.All.Add(id, fruit);
        }
    }
}