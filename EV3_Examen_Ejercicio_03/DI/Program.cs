using EV3_Examen_Ejercicio_03.DI;
using EV3_Examen_Ejercicio_03.Factories;
using EV3_Examen_Ejercicio_03.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EV3_Examen_Ejercicio_03
{
    internal class Program
    {


        static async Task Main(string[]args)
        {


            // A partir del siguiente esquema de base de datos, escribe un programa de tipo consola
            // que solicite un nombre de producto y muestre todos los datos de los proveedores que
            // ofrecen un producto que coincide total o parcialmente con ese nombre.

            // La consulta debe realizarse a partir de un procedimiento almacenado. 
            // La cadena de conexión debe leerse desde un fichero de configuración.
            // Se debe utilizar inyección de dependencias para inyectar tanto la configuración como cualquier otro servicio requerido para que la aplicación funcione



            // Configuración

            string fileConfigJsnoPath = Path.Combine(Environment.CurrentDirectory, "Data", "AppConfig.json");

            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configBuilder.AddJsonFile(fileConfigJsnoPath,false,true);


            IConfiguration configTree = configBuilder.Build();


            ServiceCollection services = new ServiceCollection();
            services.AddOptions<ConfigModel>();
            services.Configure<ConfigModel>(configTree);
            services.AddSingleton<IProviderManagerFactory, ProviderManagerFactory>();
            services.AddSingleton<ISqlServerFactory, SqlServerFactory>();
            services.AddSingleton<IOrchestrator, Orchestrator>();


            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                IOrchestrator orchestrator = serviceProvider.GetService<IOrchestrator>();
                await orchestrator.Play();
            }





        }
    }
}