using Gestor_Oficios.Data;
using Gestor0ficios.Models.Entities;
using GestorOficios.Models.Entities;
using GestorOficios.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestorOficios.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context) { _context = context; }

        public async Task<IEnumerable<Usuarios>> GetAllAsync()
        { 

            return await _context.Usuarios
                .Include(u=> u.Nombre_Completo)
                .Include(u=>u.Codigo_Departamento)
                .Include(u=>u.Cargo)
                .ToListAsync();
        
        }//fin todos

        public async Task<Usuarios?> GetByIdAsync(int id)
        {
            return await _context.Usuarios
                .Include(u=>u.Id_Usuario)
                .Include(u => u.Nombre_Completo)
                .Include(u => u.Codigo_Departamento)
                .Include(u => u.Cargo)
                .FirstOrDefaultAsync(u => u.Id_Usuario == id);
        }

        public async Task<Usuarios?> GetByNombreAsync(string Nombre)
        {
            return await _context.Usuarios
                .Include(u => u.Id_Usuario)
                .Include(u => u.Nombre_Completo)
                .Include(u => u.Codigo_Departamento)
                .Include(u => u.Cargo)
                .FirstOrDefaultAsync(u => u.Nombre_Completo == Nombre);
        }

        public async Task AddAsync(Usuarios usuarios)
        {
            await _context.Usuarios.AddAsync(usuarios);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Usuarios usuarios)
        {

            _context.Usuarios.Update(usuarios);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id_Usuario == id);


            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);

                await _context.SaveChangesAsync();
            }
        }

        public Task DeleteAsync(Usuarios usuario)
        {

            throw new NotImplementedException();
        }
    }
}
