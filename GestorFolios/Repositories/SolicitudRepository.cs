using Gestor_Oficios.Data;
using GestorOficios.Models.Entities;
using GestorOficios.Models.DTOs.Oficios;
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
        }//fin de solicitud repository

        public async Task<IEnumerable<Solicitudes>> GetAllAsync()
        { 
            return await _context.Solicitudes
                // Usuario que realizó la solicitud
                .Include(s => s.Solicitante)

                //Departamento de donde Solicita
                .Include(s => s.Id_Departamento)

                //Justificacopm
                .Include(s=>s.Justificacion)

                // Acción solicitada
                .Include(s => s.Accion)

                // Prioridad
                .Include(s => s.Prioridad)

                // Estado actual
                .Include(s => s.Estado)

                .ToListAsync();

        }// fin de listado de solicitudes


        //Obtener por id

        // Buscar una solicitud específica
        public async Task<Solicitudes?> GetByIdAsync(int id)
        {
            return await _context.Solicitudes

                .Include(s => s.Solicitante)

                .Include(s=> s.Id_Departamento)//departamento solicitante procedencia del solicitante

                .Include(s => s.Accion)

                .Include(s=>s.Fecha_Solicitud)

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

        public async Task DeleteAsync(int Id)
        {

            var solicitud = await GetByIdAsync(Id);
            if (solicitud != null) { _context.Solicitudes.Remove(solicitud); await _context.SaveChangesAsync(); }
        
        }

        public Task<IEnumerable<Solicitudes>> GetSolicitudesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
