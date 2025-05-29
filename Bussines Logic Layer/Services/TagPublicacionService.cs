using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.Interfaces;
using Data_Access_Layer.Interfaces;
using Domain_Layer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines_Logic_Layer.Services
{
    public class TagPublicacionService : ITagPublicacionService
    {
        private readonly ITagPublicacionRepository _repo;
        private readonly IMapper _mapper;

        public TagPublicacionService(ITagPublicacionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TagPublicacionDto>> GetAllAsync()
        {
            var tag = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<TagPublicacionDto>>(tag);
        }

        public async Task<TagPublicacionDto?> GetByNameAsync(string tagPublicacion)
        {
            var tag = await _repo.GetByNameAsync(tagPublicacion);
            return tag == null ? null : _mapper.Map<TagPublicacionDto>(tag);
        }

        public async Task<TagPublicacionDto> CreateAsync(TagPublicacionDto dto)
        {
            var tag = _mapper.Map<TagPublicacion>(dto);
            await _repo.AddAsync(tag);
            return _mapper.Map<TagPublicacionDto>(tag);
        }

        public async Task<bool> UpdateAsync(TagPublicacionDto dto)
        {
            var tag = await _repo.GetByNameAsync(dto.Tag);
            if (tag == null)
                return false;

            _mapper.Map(dto, tag);
            await _repo.UpdateAsync(tag);
            return true;
        }

        public async Task<bool> DeleteAsync(string tagPublicacion)
        {
            var tag = await _repo.GetByNameAsync(tagPublicacion);
            if (tag == null)
                return false;

            await _repo.DeleteAsync(tag);
            return true;
        }
    }
}
