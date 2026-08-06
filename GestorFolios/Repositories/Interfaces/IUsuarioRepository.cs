using Gestor0ficios.Models.Entities;

namespace GestorOficios.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuarios?> GetByIdAsync(int id);

        Task<Usuarios?> GetByNombreAsync(string nombre);

        Task<IEnumerable<Usuarios>> GetAllAsync();

        Task AddAsync(Usuarios usuario);

        Task UpdateAsync(Usuarios usuario);

        Task DeleteAsync(Usuarios usuario );
    }
}
