using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.Interfaces;
using Data_Access_Layer.Interfaces;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Services
{
    public class UbicacionService : IUbicacionService
    {
        private readonly IUbicacionRepository _repo;
        private readonly IMapper _mapper;

        public UbicacionService(IUbicacionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UbicacionDto>> GetAllAsync()
        {
            var ubicaciones = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<UbicacionDto>>(ubicaciones);
        }

        public async Task<UbicacionDto?> GetByIdAsync(string ubicacionName)
        {
            var ubicacion = await _repo.GetByIdAsync(ubicacionName);
            return ubicacion == null ? null : _mapper.Map<UbicacionDto>(ubicacion);
        }

        public async Task<UbicacionDto> CreateAsync(UbicacionDto dto)
        {
            var ubicacion = _mapper.Map<Ubicacion>(dto);
            await _repo.AddAsync(ubicacion);
            return _mapper.Map<UbicacionDto>(ubicacion);
        }

        public async Task<bool> UpdateAsync(UbicacionDto dto)
        {
            var ubicacion = await _repo.GetByIdAsync(dto.UbicacionName);
            if (ubicacion == null)
                return false;

            _mapper.Map(dto, ubicacion);
            await _repo.UpdateAsync(ubicacion);
            return true;
        }

        public async Task<bool> DeleteAsync(string ubicacionName)
        {
            var ubicacion = await _repo.GetByIdAsync(ubicacionName);
            if (ubicacion == null)
                return false;

            await _repo.DeleteAsync(ubicacion);
            return true;
        }
    }
}
