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
    public class ClienteSQL : IClienteRepository
    {
        private readonly DbContext _context;

        public ClienteSQL(DbContext context)
        {
            _context = context;
        }

        public Cliente get(string dni = "", string email = "")
        {
            var cliente = _context.Set<Cliente>().FirstOrDefault(cli => cli.DNI == dni || cli.UsuarioRegistrado.Email == email);
            if (cliente != null)
                return cliente;
            else
                throw new InvalidOperationException("Usuario no encontrado.");
        }

        public void update(Cliente cliente)
        {
            var existingCliente = _context.Set<Cliente>().FirstOrDefault(existingCli => existingCli.DNI == cliente.DNI);
            if (existingCliente != null)
            {
                existingCliente.UsuarioRegistrado = cliente.UsuarioRegistrado;
                existingCliente.Empleado = cliente.Empleado;

                _context.SaveChanges();
            }
        }

        public void delete(string dni = "", string email = "")
        {
            var cliente = _context.Set<Cliente>().FirstOrDefault(cli => cli.DNI == dni || cli.UsuarioRegistrado.Email == email);
            if (cliente != null)
            {
                _context.Set<Cliente>().Remove(cliente);
                _context.SaveChanges();
            }
        }

        public void create(Cliente cliente)
        {
            _context.Set<Cliente>().Add(cliente);
            _context.SaveChanges();
        }
    }
}
