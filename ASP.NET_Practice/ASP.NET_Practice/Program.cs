using Microsoft.AspNetCore.HttpLogging;

namespace ASP.NET_Practice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Determines how the web app is configured (runtime behavior, services, logging, etc.)
            // Sets the Kestrel web server by default
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


            // Logging middleware for HTTP logging
            builder.Services.AddHttpLogging(opts =>
                opts.LoggingFields = HttpLoggingFields.RequestPropertiesAndHeaders);

            // Customization for HTTP Logging
            builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information); // Severity level of 'Information' or higher will be logged


            // Creates the web application instance with the configuration defined in the builder
            // Defines how the app responds to HTTP requests, including middleware and endpoints
            WebApplication app = builder.Build();

            // The middleware was set up, and now it is added to the middleware pipeline conditionally based on the environment
            // If this isn't here, logging won't occur in the terminal display
            if (app.Environment.IsDevelopment())
            {
                app.UseHttpLogging();
            }

            app.UseWelcomePage();

            // This is the endpoint which defines how to handle a request that uses the 'get' HTTP verb
            // Routing middleware selects the appropriate endpoint here
            app.MapGet("/", () =>
            {
                string x = "Hello World!\n";
                string y = "Goodbye World!\n";
                string z = x + y;
                return z;
            });

            app.MapGet("/person", () => new Person("John", "Doe"));


            // Only now does the web application start and begin listening for incoming HTTP requests
            // It is NOT listening for requetss during the WebApplicationBuilder or WebApplication phases
            app.Run();
        }

        public record Person(string FirstName, string LastName);
    }
}