using Data_Access_Layer.Interfaces;
using Domain_Layer.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Repositorios.SQL
{

    public class ReservaRespository : IReservaRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservaRespository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reserva>> GetAllAsync()
        {
            return await _context.Reservas
                .Include(r => r.TipoEntrega)
                .Include(r => r.Pago)
                .Include(r => r.Alquiler)
                .Include(r => r.Cliente)
                .Include(r => r.Publicacion)
                .ToListAsync();
        }

        public async Task<Reserva?> GetByIdAsync(int id)
        {
            return await _context.Reservas
                .Include(r => r.TipoEntrega)
                .Include(r => r.Pago)
                .Include(r => r.Alquiler)
                .Include(r => r.Cliente)
                .Include(r => r.Publicacion)
                .FirstOrDefaultAsync(r => r.idReserva == id);
        }
        public async Task<IEnumerable<Reserva>> GetByDNIAsync(string DNI)
        {
            return await _context.Reservas
                .Include(r => r.TipoEntrega)
                .Include(r => r.Pago)
                .Include(r => r.Alquiler)
                .Include(r => r.Cliente)
                .Include(r => r.Publicacion)
                .Where(r => r.DNI == DNI)
                .ToListAsync();
        }
        public async Task AddAsync(Reserva reserva)
        {
            await _context.Reservas.AddAsync(reserva);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reserva reserva)
        {
            _context.Reservas.Update(reserva);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Reserva reserva)
        {
            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Reservas.AnyAsync(m => m.idReserva == id);
        }
    }
}
