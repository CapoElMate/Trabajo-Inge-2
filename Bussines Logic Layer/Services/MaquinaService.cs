using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs.Maquina;
using Bussines_Logic_Layer.Interfaces;
using Data_Access_Layer.Interfaces;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Services
{
    public class MaquinaService : IMaquinaService
    {
        private readonly IMaquinaRepository _repo;
        private readonly IMapper _mapper;

        public MaquinaService(IMaquinaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MaquinaDto>> GetAllAsync()
        {
            var maquinas = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<MaquinaDto>>(maquinas);
        }

        public async Task<MaquinaDto?> GetByIdAsync(int id)
        {
            var maquina = await _repo.GetByIdAsync(id);
            return maquina == null ? null : _mapper.Map<MaquinaDto>(maquina);
        }

        public async Task<MaquinaDto> CreateAsync(CreateMaquinaDto dto)
        {
            var maquina = _mapper.Map<Maquina>(dto);
            await _repo.AddAsync(maquina);
            return _mapper.Map<MaquinaDto>(maquina);
        }

        public async Task<bool> UpdateAsync(int id, MaquinaDto dto)
        {
            var maquina = await _repo.GetByIdAsync(id);
            if (maquina == null)
                return false;

            _mapper.Map(dto, maquina);
            await _repo.UpdateAsync(maquina);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var maquina = await _repo.GetByIdAsync(id);
            if (maquina == null)
                return false;

            await _repo.DeleteAsync(maquina);
            return true;
        }
    }
}
