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
    public class TipoEntregaService : ITipoEntregaService
    {
        private readonly ITipoEntregaRepository _repo;
        private readonly IMapper _mapper;

        public TipoEntregaService(ITipoEntregaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TipoEntregaDto>> GetAllAsync()
        {
            var tipoEntrega = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<TipoEntregaDto>>(tipoEntrega);
        }

        public async Task<TipoEntregaDto?> GetByIdAsync(string tipoEntregaName)
        {
            var tipoEntrega = await _repo.GetByIdAsync(tipoEntregaName);
            return tipoEntrega == null ? null : _mapper.Map<TipoEntregaDto>(tipoEntrega);
        }

        public async Task<TipoEntregaDto> CreateAsync(TipoEntregaDto dto)
        {
            var tipoEntrega = _mapper.Map<TipoEntrega>(dto);
            await _repo.AddAsync(tipoEntrega);
            return _mapper.Map<TipoEntregaDto>(tipoEntrega);
        }

        public async Task<bool> UpdateAsync(TipoEntregaDto dto)
        {
            var tipoEntrega = await _repo.GetByIdAsync(dto.Entrega);
            if (tipoEntrega == null)
                return false;

            _mapper.Map(dto, tipoEntrega);
            await _repo.UpdateAsync(tipoEntrega);
            return true;
        }

        public async Task<bool> DeleteAsync(string tipoEntregaName)
        {
            var tipoEntrega = await _repo.GetByIdAsync(tipoEntregaName);
            if (tipoEntrega == null)
                return false;

            await _repo.DeleteAsync(tipoEntrega);
            return true;
        }
    }
}
