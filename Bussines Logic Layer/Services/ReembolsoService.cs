using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Reembolso;
using Bussines_Logic_Layer.Interfaces;
using Data_Access_Layer.Interfaces;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Services
{
    public class ReembolsoService : IReembolsoService
    {
        private readonly IReembolsoRepository _repo;
        private readonly IMapper _mapper;

        public ReembolsoService(IReembolsoRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReembolsoDto>> GetAllAsync()
        {
            var reembolsos = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ReembolsoDto>>(reembolsos);
        }

        public async Task<ReembolsoDto?> GetByIdAsync(int id)
        {
            var reembolso = await _repo.GetByIdAsync(id);
            return reembolso == null ? null : _mapper.Map<ReembolsoDto>(reembolso);
        }

        public async Task<ReembolsoDto> CreateAsync(CreateReembolsoDto dto)
        {
            var reembolso = _mapper.Map<Reembolso>(dto);
            await _repo.AddAsync(reembolso);
            return _mapper.Map<ReembolsoDto>(reembolso);
        }

        public async Task<bool> UpdateAsync(ReembolsoDto dto)
        {
            var reembolso = await _repo.GetByIdAsync(dto.idReembolso);
            if (reembolso == null)
                return false;

            _mapper.Map(dto, reembolso);
            await _repo.UpdateAsync(reembolso);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var reembolso = await _repo.GetByIdAsync(id);
            if (reembolso == null)
                return false;

            await _repo.DeleteAsync(reembolso);
            return true;
        }
    }
}
