using EV3_Examen_Ejercicio_03.Manager;
using MyLib.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV3_Examen_Ejercicio_03.Factories
{
    public class ProviderManagerFactory : IProviderManagerFactory
    {
        public IProviderManager Create(SqlServer myDb)
        {
            return new ProviderManager(myDb);
        }
    }
}
