using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Reserva;
using Bussines_Logic_Layer.Interfaces;
using Data_Access_Layer.Interfaces;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _repo;
        private readonly IMapper _mapper;

        public ReservaService(IReservaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReservaDto>> GetAllAsync()
        {
            var reservas = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ReservaDto>>(reservas);
        }

        public async Task<ReservaDto?> GetByIdAsync(int id)
        {
            var reserva = await _repo.GetByIdAsync(id);
            return reserva == null ? null : _mapper.Map<ReservaDto>(reserva);
        }

        public async Task<IEnumerable<ReservaDto>> GetByDNIAsync(string DNI)
        {
            var reservas = await _repo.GetByDNIAsync(DNI);
            return reservas == null ? null : _mapper.Map<IEnumerable<ReservaDto>>(reservas);
        }
        
        public async Task<ReservaDto> CreateAsync(CreateReservaDto dto)
        {
            Reserva reserva= _mapper.Map<Reserva>(dto);
            await _repo.AddAsync(reserva);
            return _mapper.Map<ReservaDto>(reserva);
        }

        public async Task<bool> UpdateAsync(int id, ReservaDto dto)
        {
            var reserva = await _repo.GetByIdAsync(id);
            if (reserva == null)
                return false;

            _mapper.Map(dto, reserva);
            await _repo.UpdateAsync(reserva);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var reserva = await _repo.GetByIdAsync(id);
            if (reserva == null)
                return false;

            await _repo.DeleteAsync(reserva);
            return true;
        }
    }
}
