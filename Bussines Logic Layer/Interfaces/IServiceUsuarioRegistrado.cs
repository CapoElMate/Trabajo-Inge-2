using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer.Interfaces;

namespace Bussines_Logic_Layer.Interfaces
{
    public interface IServiceUsuarioRegistrado
    {
        public void UsuarioRegistradoService(IUsuarioRegistradoRepository repository);
    }
}
