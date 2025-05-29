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
    public class TagMaquinaService : ITagMaquinaService
    {
        private readonly ITagMaquinaRepository _repo;
        private readonly IMapper _mapper;

        public TagMaquinaService(ITagMaquinaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TagMaquinaDto>> GetAllAsync()
        {
            var tag = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<TagMaquinaDto>>(tag);
        }

        public async Task<TagMaquinaDto?> GetByNameAsync(string tagMaquina)
        {
            var tag = await _repo.GetByNameAsync(tagMaquina);
            return tag == null ? null : _mapper.Map<TagMaquinaDto>(tag);
        }

        public async Task<TagMaquinaDto> CreateAsync(TagMaquinaDto dto)
        {
            var tag = _mapper.Map<TagMaquina>(dto);
            await _repo.AddAsync(tag);
            return _mapper.Map<TagMaquinaDto>(tag);
        }

        public async Task<bool> UpdateAsync(TagMaquinaDto dto)
        {
            var tag = await _repo.GetByNameAsync(dto.Tag);
            if (tag == null)
                return false;

            _mapper.Map(dto, tag);
            await _repo.UpdateAsync(tag);
            return true;
        }

        public async Task<bool> DeleteAsync(string tagMaquina)
        {
            var tag = await _repo.GetByNameAsync(tagMaquina);
            if (tag == null)
                return false;

            await _repo.DeleteAsync(tag);
            return true;
        }
    }
}
