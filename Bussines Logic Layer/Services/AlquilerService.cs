using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Alquiler;
using Bussines_Logic_Layer.Interfaces;
using Data_Access_Layer.Interfaces;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Services
{
    public class AlquilerService : IAlquilerService
    {
        private readonly IAlquilerRepository _repo;
        private readonly IMapper _mapper;

        public AlquilerService(IAlquilerRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AlquilerDto>> GetAllAsync()
        {
            var alquileres = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<AlquilerDto>>(alquileres);
        }

        public async Task<AlquilerDto?> GetByIdAsync(int id)
        {
            var alquiler = await _repo.GetByIdAsync(id);
            return alquiler == null ? null : _mapper.Map<AlquilerDto>(alquiler);
        }

        public async Task<AlquilerDto> CreateAsync(CreateAlquilerDto dto)
        {
            var alquiler = _mapper.Map<Alquiler>(dto);
            await _repo.AddAsync(alquiler);
            return _mapper.Map<AlquilerDto>(alquiler);
        }

        public async Task<bool> UpdateAsync(AlquilerDto dto)
        {
            var alquiler = await _repo.GetByIdAsync(dto.idAlquiler);
            if (alquiler == null)
                return false;

            _mapper.Map(dto, alquiler);
            await _repo.UpdateAsync(alquiler);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var alquiler = await _repo.GetByIdAsync(id);
            if (alquiler == null)
                return false;

            await _repo.DeleteAsync(alquiler);
            return true;
        }

        public async Task<bool> LogicDeleteAsync(int id)
        {
            var alquiler = await _repo.GetByIdAsync(id);
            if (alquiler == null)
                return false;

            alquiler.isDeleted = true;
            await _repo.UpdateAsync(alquiler);
            return true;
        }
    }
}
