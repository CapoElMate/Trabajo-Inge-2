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
    public class TipoMaquinaService : ITipoMaquinaService
    {
        private readonly ITipoMaquinaRepository _repo;
        private readonly IMapper _mapper;

        public TipoMaquinaService(ITipoMaquinaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TipoMaquinaDto>> GetAllAsync()
        {
            var tag = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<TipoMaquinaDto>>(tag);
        }

        public async Task<TipoMaquinaDto?> GetByNameAsync(string tipoMaquina)
        {
            var tipo = await _repo.GetByNameAsync(tipoMaquina);
            return tipo == null ? null : _mapper.Map<TipoMaquinaDto>(tipo);
        }

        public async Task<TipoMaquinaDto> CreateAsync(TipoMaquinaDto dto)
        {
            var tipo = _mapper.Map<TipoMaquina>(dto);
            await _repo.AddAsync(tipo);
            return _mapper.Map<TipoMaquinaDto>(tipo);
        }

        public async Task<bool> UpdateAsync(TipoMaquinaDto dto)
        {
            var tipo = await _repo.GetByNameAsync(dto.Tipo);
            if (tipo == null)
                return false;

            _mapper.Map(dto, tipo);
            await _repo.UpdateAsync(tipo);
            return true;
        }

        public async Task<bool> DeleteAsync(string tipoMaquina)
        {
            var tipo = await _repo.GetByNameAsync(tipoMaquina);
            if (tipo == null)
                return false;

            await _repo.DeleteAsync(tipo);
            return true;
        }
    }
}
