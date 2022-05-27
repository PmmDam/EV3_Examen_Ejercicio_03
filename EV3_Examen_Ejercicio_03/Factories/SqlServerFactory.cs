using MyLib.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV3_Examen_Ejercicio_03.Factories
{
    public class SqlServerFactory : ISqlServerFactory
    {
        public SqlServer Create(string ConnectionString)
        {
            return new SqlServer(ConnectionString);
        }
    }
}
