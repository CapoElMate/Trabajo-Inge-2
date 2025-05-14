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
    public class EmpleadoSQL : IEmpleadoRepository
    {
        private readonly DbContext _context;

        public EmpleadoSQL(DbContext context)
        {
            _context = context;
        }

        public Empleado get(string dni = "", string email = "", int nroEmpleado = -1)
        {
            var empleado = _context.Set<Empleado>().FirstOrDefault(emp=> emp.DNI == dni || emp.Cliente.UsuarioRegistrado.Email == email || (nroEmpleado != -1) && emp.nroEmpleado == nroEmpleado);
            if (empleado != null)
                return empleado;
            else
                throw new InvalidOperationException("Usuario no encontrado.");
        }

        public void update(Empleado empleado)
        {
            var existingUser = _context.Set<Empleado>().FirstOrDefault(existingEmp => existingEmp.DNI == empleado.DNI);
            if (existingUser != null)
            {
                existingUser.Cliente = empleado.Cliente;

                _context.SaveChanges();
            }
        }

        public void delete(string dni = "", string email = "", int nroEmpleado = -1)
        {
            var empleado = _context.Set<Empleado>().FirstOrDefault(emp => emp.DNI == dni || emp.Cliente.UsuarioRegistrado.Email == email || (nroEmpleado != -1) && emp.nroEmpleado == nroEmpleado);
            if (empleado != null)
            {
                _context.Set<Empleado>().Remove(empleado);
                _context.SaveChanges();
            }
        }

        public void create(Empleado empleado)
        {
            _context.Set<Empleado>().Add(empleado);
            _context.SaveChanges();
        }
    }
}
