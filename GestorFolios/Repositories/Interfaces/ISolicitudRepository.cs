using Gestor0ficios.Models.Entities;
using GestorOficios.Models.Entities;
namespace GestorOficios.Repositories.Interfaces
{
    public interface ISolicitudRepository
    {
        Task<IEnumerable<Solicitudes>> GetSolicitudesAsync();
        Task<Solicitudes?> GetByIdAsync(int id);
        Task AddAsync(Solicitudes solicitudes); 
        Task UpdateAsync(Solicitudes solicitudes);
        Task DeleteAsync(int id);


    }
}
