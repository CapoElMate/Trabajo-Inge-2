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
    public class MarcaService : IMarcaService
    {
        private readonly IMarcaRepository _repo;
        private readonly IMapper _mapper;

        public MarcaService(IMarcaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MarcaDto>> GetAllAsync()
        {
            var marca = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<MarcaDto>>(marca);
        }

        public async Task<MarcaDto?> GetByNameAsync(string marcaName)
        {
            var marca = await _repo.GetByNameAsync(marcaName);
            return marca == null ? null : _mapper.Map<MarcaDto>(marca);
        }

        public async Task<MarcaDto> CreateAsync(MarcaDto dto)
        {
            var marca = _mapper.Map<Marca>(dto);
            await _repo.AddAsync(marca);
            return _mapper.Map<MarcaDto>(marca);
        }

        public async Task<bool> UpdateAsync(MarcaDto dto)
        {
            var marca = await _repo.GetByNameAsync(dto.Marca);
            if (marca == null)
                return false;

            _mapper.Map(dto, marca);
            await _repo.UpdateAsync(marca);
            return true;
        }

        public async Task<bool> DeleteAsync(string marcaName)
        {
            var marca = await _repo.GetByNameAsync(marcaName);
            if (marca == null)
                return false;

            await _repo.DeleteAsync(marca);
            return true;
        }
    }
}
