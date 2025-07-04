﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;

namespace Data_Access_Layer.Interfaces
{
    public interface ITipoMaquinaRepository
    {
        Task<IEnumerable<TipoMaquina>> GetAllAsync();
        Task<TipoMaquina?> GetByNameAsync(string tipoMaquina);
        Task AddAsync(TipoMaquina tipoMaquina);
        Task UpdateAsync(TipoMaquina tipoMaquina);
        Task DeleteAsync(TipoMaquina tipoMaquina);
        Task<bool> ExistsAsync(string tipoMaquina);
    }
}
