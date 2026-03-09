using MongoDB.Driver;
using WeatherApp.API.Models;
using WeatherApp.API.Services;
using WeatherApp.API.Services.Interfaces;

namespace WeatherApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string mongoHost = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true"
                ? "mongo"
                : "localhost";

            string connectionString = $"mongodb://root:rootpassword@{mongoHost}:27017/?authSource=admin&serverSelectionTimeoutMS=3000";

            MongoClient mongoClient = new MongoClient(connectionString);
            IMongoDatabase database = mongoClient.GetDatabase("mydatabase");

            IMongoCollection<Location> placesCollection = database.GetCollection<Location>("places");
            IMongoCollection<Weekday> weekdaysCollection = database.GetCollection<Weekday>("weekdays");
            IMongoCollection<Absence> absencesCollection = database.GetCollection<Absence>("absences");

            builder.Services.AddSingleton<IMongoCollection<Location>>(placesCollection);
            builder.Services.AddSingleton<IMongoCollection<Weekday>>(weekdaysCollection);
            builder.Services.AddSingleton<IMongoCollection<Absence>>(absencesCollection);

            builder.Services.AddScoped<ILocationService, LocationService>();
            builder.Services.AddScoped<IScheduleService, ScheduleService>();

            WebApplication app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}