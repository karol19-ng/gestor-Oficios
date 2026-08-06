using Gestor_Oficios.Data;
using GestorOficios.Models.Entities;
using GestorOficios.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestorOficios.Repositories
{
    public class SolicitudRepository : ISolicitudRepository
    {
        private readonly ApplicationDbContext _context;

        public SolicitudRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Solicitudes>> GetSolicitudesAsync()
        {
            return await _context.Solicitudes
                .Include(s => s.Solicitante)
                .Include(s => s.Oficio_Solicitado)
                .Include(s => s.Accion)
                .Include(s => s.Prioridad)
                .Include(s => s.Estado)
                .ToListAsync();
        }

        public async Task<Solicitudes?> GetByIdAsync(int id)
        {
            return await _context.Solicitudes
                .Include(s => s.Solicitante)
                .Include(s => s.Oficio_Solicitado)
                .Include(s => s.Accion)
                .Include(s => s.Prioridad)
                .Include(s => s.Estado)
                .FirstOrDefaultAsync(s => s.Id_Solicitud == id);
        }

        public async Task AddAsync(Solicitudes solicitudes)
        {
            await _context.Solicitudes.AddAsync(solicitudes);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Solicitudes solicitudes)
        {
            _context.Solicitudes.Update(solicitudes);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var solicitud = await GetByIdAsync(id);
            if (solicitud != null)
            {
                _context.Solicitudes.Remove(solicitud);
                await _context.SaveChangesAsync();
            }
        }
    }
}
