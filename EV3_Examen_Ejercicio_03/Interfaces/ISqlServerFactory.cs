using MyLib.SQL;

namespace EV3_Examen_Ejercicio_03.Factories
{
    public interface ISqlServerFactory
    {
        SqlServer Create(string ConnectionString);
    }
}