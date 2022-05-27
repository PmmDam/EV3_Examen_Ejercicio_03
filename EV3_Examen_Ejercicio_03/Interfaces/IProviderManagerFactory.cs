using EV3_Examen_Ejercicio_03.Manager;
using MyLib.SQL;

namespace EV3_Examen_Ejercicio_03.Factories
{
    public interface IProviderManagerFactory
    {
        IProviderManager Create(SqlServer myDb);
    }
}