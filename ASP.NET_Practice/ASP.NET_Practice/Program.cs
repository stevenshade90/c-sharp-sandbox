namespace ASP.NET_Practice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            WebApplication app = builder.Build();

            RouteGroupBuilder fruitApi = app.MapGroup("/fruit");
            fruitApi.MapGet("/", () => Fruit.All);
            fruitApi.MapGet("/{id}", (int id) =>
                Fruit.All.TryGetValue(id, out var fruit)
                ? TypedResults.Ok(fruit)
                : Results.Problem(statusCode: 404));

            fruitApi.MapDelete("/{id}", (int id) =>
            {
                if (Fruit.All.Remove(id))
                {
                    return Results.Ok($"Deleted fruit {id}");
                }
                return Results.Problem(statusCode: 404);
            });

            app.MapGet("/", () => "Goodbye, world!!\nAnd also hello!");
 
            app.Run();
        }
    }

    record Fruit(string Name, int Stock)
    {
        public static Dictionary<int, Fruit> All = new Dictionary<int, Fruit>()
        {
            { 1, new Fruit("Banana", 100) },
            { 2, new Fruit("Orange", 1500) },
            { 3, new Fruit("Apple", 500) }
        };

        public static void DeleteFruit(int id)
        {
            All.Remove(id);
        }
    }
}

//class ValidationHelper : IEndpointFilter
//{
//    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
//    {
//        var name = context.GetArgument<string>(0);
//        if (string.IsNullOrEmpty(name) || !name.StartsWith('s'))
//        {
//            return Results.ValidationProblem(
//                new Dictionary<string, string[]>
//                {
//                    { "name", new[] { "Invalid name. Must start with s" } }
//                });
//        }
//        return await next(context);
//    }
//}

//// Http logging added for request properties, a filter was added for Information-level and above, then it actually needs to be added to the pipelin by calling UseHttpLogging() on the app. 
//builder.Services.AddHttpLogging(opts => opts.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties);
//builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information);

//WebApplication app = builder.Build();

//app.UseStaticFiles();
//app.UseStatusCodePages();
//app.UseRouting();

//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/error");
//}
//else
//{
//    app.UseHttpLogging();
//    app.UseDeveloperExceptionPage();
//}

//List<string> names = new List<string>() { "steve", "Tim", "Jeff" };

//app.MapGet("/", () => "Hello World!");
//app.MapGet("/about", () => "About page");
//app.MapGet("/error", () => "An error occurred.");
//app.MapGet("/name/{person}", (string person) =>
//    names.Contains(person)
//    ? TypedResults.Ok(person)
//    : Results.Problem(statusCode: 404))
//    .AddEndpointFilter<ValidationHelper>();

//app.Run();

