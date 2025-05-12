using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer.Interfaces;
using Domain_Layer.Entidades;

namespace Data_Access_Layer.Repositorios.SQL
{
    public class UsuarioRegistradoSQL: IUsuarioRegistradoRepository
    {
        public UsuarioRegistrado get()
        {
            return null;
        }
        public void update(UsuarioRegistrado usuarioRegistrado)
        {

        }
        public void delete(int dni, string email)
        {

        }
        public void create(UsuarioRegistrado usuarioRegistrado)
        {

        }
    }
}
