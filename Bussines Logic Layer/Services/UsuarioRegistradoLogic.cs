using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines_Logic_Layer.Interfaces;
using Data_Access_Layer.Interfaces;

namespace Bussines_Logic_Layer.Services
{
    public class UsuarioRegistradoLogic: IServiceUsuarioRegistrado
    {
        private IUsuarioRegistradoRepository _usuarioRegistradoRepository;
        public void UsuarioRegistradoService(IUsuarioRegistradoRepository repository)
        {
            _usuarioRegistradoRepository = repository;
        }
    }
}
