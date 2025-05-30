using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer.Interfaces;
using Domain_Layer.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Repositorios.SQL
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            return await _context.Clientes
                .Include(u => u.UsuarioRegistrado.PermisosEspeciales)
                .ToListAsync();
        }

        public async Task<Cliente?> GetByDNIAsync(string dni)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(u => u.DNI == dni);
        }

        public async Task<Cliente?> GetByEmailAsync(string email)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(u => u.UsuarioRegistrado.Email == email);
        }

        public async Task AddAsync(Cliente usuario)
        {
            await _context.Clientes.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cliente usuario)
        {
            _context.Clientes.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Cliente usuario)
        {
            _context.Clientes.Remove(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string dni)
        {
            return await _context.Clientes.AnyAsync(u => u.DNI == dni);
        }
    }
}
