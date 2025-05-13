using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer.Interfaces;

namespace Bussines_Logic_Layer.Services
{
    public class UsuarioRegistradoService
    {
        private IUsuarioRegistradoRepository _usuarioRegistradoRepository;
        public UsuarioRegistradoService(IUsuarioRegistradoRepository usuarioRegistradoRepository)
        {
            _usuarioRegistradoRepository = usuarioRegistradoRepository;
        }

        public string getName()
        {
            return "";
        }
    }
}
