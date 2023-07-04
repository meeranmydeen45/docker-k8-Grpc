using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;

namespace PlatformService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            if(builder.Environment.IsDevelopment())
            {
                 builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
            }
            else
            {
                builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer("Server=mssql-clusterip-srv,1433;Initial Catalog=platformsdb;User ID=sa;Password=pa55w0rd!;Encrypt=false;"));
            }

           
            // Add services to the container.
            builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
            builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            Console.WriteLine(Environment.GetEnvironmentVariable("CommandService"));
           var app = builder.Build();

            //Data Seeding
            PrepDb.PrepPopulation(app, builder.Environment.IsProduction());

            // Configure the HTTP request pipeline.

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}