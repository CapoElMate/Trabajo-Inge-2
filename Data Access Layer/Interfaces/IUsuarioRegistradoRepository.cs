using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;

namespace Data_Access_Layer.Interfaces
{
    public interface IUsuarioRegistradoRepository
    {
        public UsuarioRegistrado get();
        public void update(UsuarioRegistrado usuarioRegistrado);
        public void delete(int dni, string email);
        public void create(UsuarioRegistrado usuarioRegistrado);
    }
}
