using Data_Access_Layer.Interfaces;
using Domain_Layer.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositorios.SQL
{
    public class AlquilerRepository : IAlquilerRepository
    {
        private readonly ApplicationDbContext _context;

        public AlquilerRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Alquiler>> GetAllAsync()
        {
            return await _context.Alquileres
                .Include(p => p.InfoAsentada)
                .ToListAsync();
        }
        public async Task<IEnumerable<Alquiler>> GetByDNIAsync(string dni)
        {
            return await _context.Alquileres
                .Include(p => p.InfoAsentada)
                .Where(a => a.DNICliente.Equals(dni))
                .ToListAsync();
        }
        public async Task AddAsync(Alquiler Alquiler)
        {
            await _context.Alquileres.AddAsync(Alquiler);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(Alquiler Alquiler)
        {
            _context.Alquileres.Remove(Alquiler);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Alquileres.AnyAsync(p => p.idAlquiler == id);
        }

        
        public async Task<Alquiler?> GetByIdAsync(int id)
        {
            return await _context.Alquileres
                .Include(p => p.InfoAsentada)
                .FirstOrDefaultAsync(p => p.idAlquiler == id);
        }

        public async Task UpdateAsync(Alquiler Alquiler)
        {
            _context.Alquileres.Update(Alquiler);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Efectivizar(Alquiler alquiler, int idReserva)
        {
            var reserva = await _context.Reservas
                .FirstOrDefaultAsync(r => r.idReserva == idReserva);

            if (reserva == null)
                return false;

            reserva.Alquiler = alquiler;
            reserva.idAlquiler = alquiler.idAlquiler;
            _context.Reservas.Update(reserva);
            return true;
        }
    }
}
