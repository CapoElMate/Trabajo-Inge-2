using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Alquiler;

namespace Bussines_Logic_Layer.Interfaces
{
    public interface IAlquilerService
    {
        Task<IEnumerable<AlquilerDto>> GetAllAsync();
        Task<AlquilerDto?> GetByIdAsync(int id);
        Task<IEnumerable<AlquilerDto?>> GetByDNIAsync(string dni);
        Task<AlquilerDto> CreateAsync(CreateAlquilerDto dto);
        Task<bool> UpdateAsync(AlquilerDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> LogicDeleteAsync(int id);
        Task<bool> ReservarAlquiler(int idAlquiler, int idReserva);
        
    }
}
