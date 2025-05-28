using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.InfoAsentada;
using Bussines_Logic_Layer.Interfaces;
using Data_Access_Layer.Interfaces;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Services
{
    public class InfoAsentadaService : IInfoAsentadaService
    {
        private readonly IInfoAsentadaRepository _repo;
        private readonly IMapper _mapper;

        public InfoAsentadaService(IInfoAsentadaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InfoAsentadaDto>> GetAllAsync()
        {
            var infoAsentadas = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<InfoAsentadaDto>>(infoAsentadas);
        }

        public async Task<InfoAsentadaDto?> GetByIdAsync(int id)
        {
            var infoAsentada = await _repo.GetByIdAsync(id);
            return infoAsentada == null ? null : _mapper.Map<InfoAsentadaDto>(infoAsentada);
        }

        public async Task<InfoAsentadaDto> CreateAsync(CreateInfoAsentadaDto dto)
        {
            var infoAsentada = _mapper.Map<InfoAsentada>(dto);
            await _repo.AddAsync(infoAsentada);
            return _mapper.Map<InfoAsentadaDto>(infoAsentada);
        }

        public async Task<bool> UpdateAsync(InfoAsentadaDto dto)
        {
            var infoAsentada = await _repo.GetByIdAsync(dto.idInfo);
            if (infoAsentada == null)
                return false;

            _mapper.Map(dto, infoAsentada);
            await _repo.UpdateAsync(infoAsentada);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var infoAsentada = await _repo.GetByIdAsync(id);
            if (infoAsentada == null)
                return false;

            await _repo.DeleteAsync(infoAsentada);
            return true;
        }
    }
}
