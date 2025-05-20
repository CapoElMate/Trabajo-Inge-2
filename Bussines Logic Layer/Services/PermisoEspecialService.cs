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
    public class PermisoEspecialServie : IPermisoEspecialService
    {
        private readonly IPermisoEspecialRepository _repo;
        private readonly IMapper _mapper;

        public PermisoEspecialServie(IPermisoEspecialRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PermisoEspecialDto>> GetAllAsync()
        {
            var permiso = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<PermisoEspecialDto>>(permiso);
        }

        public async Task<PermisoEspecialDto?> GetByNameAsync(string permisoEspecial)
        {
            var permiso = await _repo.GetByNameAsync(permisoEspecial);
            return permiso == null ? null : _mapper.Map<PermisoEspecialDto>(permiso);
        }

        public async Task<PermisoEspecialDto> CreateAsync(PermisoEspecialDto dto)
        {
            var permiso = _mapper.Map<PermisoEspecial>(dto);
            await _repo.AddAsync(permiso);
            return _mapper.Map<PermisoEspecialDto>(permiso);
        }

        public async Task<bool> UpdateAsync(PermisoEspecialDto dto)
        {
            var permiso = await _repo.GetByNameAsync(dto.Permiso);
            if (permiso == null)
                return false;

            _mapper.Map(dto, permiso);
            await _repo.UpdateAsync(permiso);
            return true;
        }

        public async Task<bool> DeleteAsync(string permisoEspecial)
        {
            var permiso = await _repo.GetByNameAsync(permisoEspecial);
            if (permiso == null)
                return false;

            await _repo.DeleteAsync(permiso);
            return true;
        }
    }
}
