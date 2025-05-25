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
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly ApplicationDbContext _context;

        public EmpleadoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empleado>> GetAllAsync()
        {
            return await _context.Empleados
                .Include(u => u.Cliente)
                .ToListAsync();
        }

        public async Task<Empleado?> GetByDNIAsync(string dni)
        {
            return await _context.Empleados
                .FirstOrDefaultAsync(u => u.DNI == dni);
        }

        public async Task<Empleado?> GetByNroEmpleadoAsync(int nroEmpleado)
        {
            return await _context.Empleados
                .FirstOrDefaultAsync(u => u.nroEmpleado == nroEmpleado);
        }

        public async Task<Empleado?> GetByEmailAsync(string email)
        {
            return await _context.Empleados
                .FirstOrDefaultAsync(u => u.Cliente.UsuarioRegistrado.Email == email);
        }

        public async Task AddAsync(Empleado usuario)
        {
            await _context.Empleados.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Empleado usuario)
        {
            _context.Empleados.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Empleado usuario)
        {
            _context.Empleados.Remove(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string dni)
        {
            return await _context.Empleados.AnyAsync(u => u.DNI == dni);
        }
    }
}
