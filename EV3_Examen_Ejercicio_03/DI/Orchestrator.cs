using EV3_Examen_Ejercicio_03.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLib.SQL;
using EV3_Examen_Ejercicio_03.Factories;
using EV3_Examen_Ejercicio_03.Manager;

namespace EV3_Examen_Ejercicio_03.DI
{
    public class Orchestrator : IOrchestrator
    {

        // Configuración
        private IOptionsMonitor<ConfigModel> _options { get; set; }
        private ConfigModel _config { get; set; }
        private IDisposable _optionsDisposable { get; set; }



        //Dependencias del constructos

        private IProviderManagerFactory _productManagerFactory { get; set; }
        private ISqlServerFactory _myDbFactory { get; set; }





        public Orchestrator(IOptionsMonitor<ConfigModel> options, IProviderManagerFactory productManagerFactory, ISqlServerFactory myDbFactory)
        {

            //Configuración
            this._options = options;
            this._config = this._options.CurrentValue;
            this._optionsDisposable = options.OnChange<ConfigModel>(ReadNewConfig);

            // Dependencias
            this._productManagerFactory = productManagerFactory;
            this._myDbFactory = myDbFactory;
        }


        private void ReadNewConfig(ConfigModel newConfig)
        {
            if (this._optionsDisposable != null)
            {
                this._optionsDisposable.Dispose();
                this._optionsDisposable = null;
            }
            this._config = newConfig;
            this._optionsDisposable = this._options.OnChange<ConfigModel>(ReadNewConfig);
        }


        public async Task Play()
        {

            SqlServer myDb = this._myDbFactory.Create(this._config.connectionString);
            IProviderManager providerManager = this._productManagerFactory.Create(myDb);

            Console.WriteLine(this._config.connectionString);

             
            string nameProduct = "Product1";

            // Esto rellena la lista ProviderList que está en el ProviderManager
            await providerManager.GetAllProvidersFromProductAsync(nameProduct);

            await PrintProviders(providerManager.ProviderList);

        }


        private async Task PrintProviders(List<ProviderModel> providersList)
        {
            if (providersList.Count !=null) 
            {
                for (int i = 0; i < providersList.Count; i++)
                {
                    Console.WriteLine(providersList[i].IdProvider);
                    Console.WriteLine(providersList[i].Name);
                    Console.WriteLine(providersList[i].Adress);
                    Console.WriteLine(providersList[i].Phone);
                }
            }
            
        } 
    }
}
