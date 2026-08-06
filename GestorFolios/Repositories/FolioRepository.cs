using Gestor_Oficios.Data;
using GestorOficios.Models.DTOs.Oficios;
using GestorOficios.Models.Entities;
using GestorOficios.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestorOficios.Repositories
{
    public class FolioRepository : IFolioRepository
    {
        private readonly ApplicationDbContext _context;

        public FolioRepository(ApplicationDbContext contex) 
        {
            _context = contex;
        }// fin folio repository


        //Listado
        public async Task<IEnumerable<Control_Oficios>> GetAllAsync()
        {
            return await _context.Control_Oficios.ToListAsync();
        }//fin async

        // BUSCAR POR ID
        public async Task<Control_Oficios?> GetByIdAsync(int id)
        {
            return await _context.Control_Oficios
                .Include(f => f.Id_Oficio)
                .Include(f => f.AccesosTemporales)
                .Include(f => f.Elaborado_Por)
                .FirstOrDefaultAsync(f => f.Id_Archivo == id);
        }



        // CREAR OFICIO
        public async Task AddAsync(Control_Oficios oficio)
        {
            await _context.Control_Oficios.AddAsync(oficio);

            await _context.SaveChangesAsync();
        }
    }
}
