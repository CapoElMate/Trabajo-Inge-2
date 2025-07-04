﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines_Logic_Layer.DTOs.Reserva;

namespace Bussines_Logic_Layer.Interfaces
{
    public interface IReservaService
    {
        Task<IEnumerable<ReservaDto>> GetAllAsync();
        Task<ReservaDto?> GetByIdAsync(int id);
        Task<IEnumerable<ReservaDto?>>? GetByDNIAsync(string DNI);
        Task<bool> UpdatePayment(int idReserva, long idPago);

        Task<ReservaDto> CreateAsync(CreateReservaDto dto);
        Task<bool> UpdateAsync(int id, ReservaDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
