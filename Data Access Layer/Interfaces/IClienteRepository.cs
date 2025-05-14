using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;

namespace Data_Access_Layer.Interfaces
{
    public interface IClienteRepository
    {
        public Cliente get(string dni = "", string email = "");
        public void update(Cliente cliente);
        public void delete(string dni = "", string email = "");
        public void create(Cliente cliente);
    }
}
