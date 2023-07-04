using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if(isProd)
            {
                try
                {
                    context.Database.Migrate();
                }
                catch(Exception e)
                {
                   Console.WriteLine($"--> Error Occured while Migration {e.Message}");
                }
                
            }
            if(!context.Platforms.Any())
            {
                Console.WriteLine("--> Started Seeding Data");
                context.Platforms.AddRange(
                    new Platform { Name = "DotNet", Publisher = "Microsoft", Cost = "Free"},
                     new Platform { Name = "C#", Publisher = "Microsoft", Cost = "Free" },
                      new Platform { Name = "Docker", Publisher = "Docker Org", Cost = "Premium" },
                       new Platform { Name = "Kubernetes", Publisher = "Google", Cost = "Premium" });
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> Data Loaded Already");       
            }
        }
    }
}
