using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Projeto_Web_CRUD.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Web_CRUD {
    public class Program {
        public static void Main(string[] args) {
            //CreateHostBuilder(args).Build().Run();

            var host = CreateHostBuilder(args).Build();

            CreateDataBaseIfNotExists(host);

            host.Run();
        }

        private static void CreateDataBaseIfNotExists(IHost host) {
            using (var scope = host.Services.CreateScope()) {
                var services = scope.ServiceProvider;

                try {
                    var context = services.GetRequiredService<DataBaseContext>();
                    DataBaseInitializer.Initialize(context);
                } catch(Exception error) {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(error, "Não foi possível criar o Banco de dados");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
