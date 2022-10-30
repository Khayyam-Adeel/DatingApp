using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;


namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host=CreateHostBuilder(args).Build(); //.Run();
            using var scope=host.Services.CreateScope();
            var Services=scope.ServiceProvider;

            try{
                var context=Services.GetRequiredService<DataContext>();
                await context.Database.MigrateAsync();
                await Seed.SeedUser(context);

            }catch(Exception ex){
                    var logger=Services.GetRequiredService<Logger<Program>>();
                    logger.LogError(ex,"An Error Occured During Migrations");

            }
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
