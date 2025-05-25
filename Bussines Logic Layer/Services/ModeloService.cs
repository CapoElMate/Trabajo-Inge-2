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
using Microsoft.EntityFrameworkCore;

namespace Bussines_Logic_Layer.Services
{
    public class ModeloService : IModeloService
    {
        private readonly IModeloRepository _repo;
        private readonly IMapper _mapper;

        public ModeloService(IModeloRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ModeloDto>> GetAllAsync()
        {
            var modelo = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ModeloDto>>(modelo);
        }

        public async Task<ModeloDto?> GetByNameAsync(string modeloName)
        {
            var modelo = await _repo.GetByNameAsync(modeloName);
            return modelo == null ? null : _mapper.Map<ModeloDto>(modelo);
        }

        public async Task<ModeloDto> CreateAsync(ModeloDto dto)
        {
            var modelo = _mapper.Map<Modelo>(dto);
            await _repo.AddAsync(modelo);
            return _mapper.Map<ModeloDto>(modelo);
        }

        public async Task<bool> UpdateAsync(string modeloName, ModeloDto dto)
        {
            var modelo = await _repo.GetByNameAsync(modeloName);
            if (modelo == null)
                return false;

            _mapper.Map(dto, modelo);
            await _repo.UpdateAsync(modelo);
            return true;
        }

        public async Task<bool> DeleteAsync(string modeloName)
        {
            var modelo = await _repo.GetByNameAsync(modeloName);
            if (modelo == null)
                return false;

            await _repo.DeleteAsync(modelo);
            return true;
        }
    }
}
