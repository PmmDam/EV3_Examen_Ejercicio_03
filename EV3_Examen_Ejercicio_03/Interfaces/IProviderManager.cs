using EV3_Examen_Ejercicio_03.Model;

namespace EV3_Examen_Ejercicio_03.Manager
{
    public interface IProviderManager
    {
        List<ProviderModel> ProviderList { get; set; }

        Task GetAllProvidersFromProductAsync(string Name);
    }
}