﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;

namespace Data_Access_Layer.Interfaces
{
    public interface IUsuarioRegistradoRepository
    {
        public UsuarioRegistrado get(string dni = "", string email = "");
        public void update(UsuarioRegistrado usuarioRegistrado);
        public void delete(string dni = "", string email = "");
        public void create(UsuarioRegistrado usuarioRegistrado);
    }
}
