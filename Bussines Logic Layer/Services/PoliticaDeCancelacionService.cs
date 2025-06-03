using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Maquina;
using Bussines_Logic_Layer.Interfaces;
using Data_Access_Layer.Interfaces;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Services
{
    public class PoliticaDeCancelacionService : IPoliticaDeCancelacionService
    {
        private readonly IPoliticaDeCancelacionRepository _repo;
        private readonly IMapper _mapper;

        public PoliticaDeCancelacionService(IPoliticaDeCancelacionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PoliticaDeCancelacionDto>> GetAllAsync()
        {
            var politicaDeCancelaciones = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<PoliticaDeCancelacionDto>>(politicaDeCancelaciones);
        }

        public async Task<PoliticaDeCancelacionDto?> GetByNameAsync(string politica)
        {
            var politicaDeCancelacion = await _repo.GetByNameAsync(politica);
            return politicaDeCancelacion == null ? null : _mapper.Map<PoliticaDeCancelacionDto>(politicaDeCancelacion);
        }

        public async Task<PoliticaDeCancelacionDto> CreateAsync(PoliticaDeCancelacionDto dto)
        {
            var politicaDeCancelacion = _mapper.Map<PoliticaDeCancelacion>(dto);
            await _repo.AddAsync(politicaDeCancelacion);
            return _mapper.Map<PoliticaDeCancelacionDto>(politicaDeCancelacion);
        }

        public async Task<bool> UpdateAsync(string politica, PoliticaDeCancelacionDto dto)
        {
            var politicaDeCancelacion = await _repo.GetByNameAsync(politica);
            if (politicaDeCancelacion == null)
                return false;

            _mapper.Map(dto, politicaDeCancelacion);
            await _repo.UpdateAsync(politicaDeCancelacion);
            return true;
        }

        public async Task<bool> DeleteAsync(string politica)
        {
            var politicaDeCancelacion = await _repo.GetByNameAsync(politica);
            if (politicaDeCancelacion == null)
                return false;

            await _repo.DeleteAsync(politicaDeCancelacion);
            return true;
        }
        
    }
}
