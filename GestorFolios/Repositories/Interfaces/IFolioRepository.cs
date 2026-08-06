using Gestor0ficios.Models.Entities;
using GestorOficios.Models.DTOs.Oficios;
using GestorOficios.Models.Entities;
namespace GestorOficios.Repositories.Interfaces
{
    public interface IFolioRepository
    {
        Task<IEnumerable<Control_Oficios>> GetAllAsync();

        Task<Control_Oficios?> GetByIdAsync(int id);

        Task AddAsync(Control_Oficios folio);


    }
}
