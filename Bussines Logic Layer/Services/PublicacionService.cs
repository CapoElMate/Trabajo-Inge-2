using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs.Publicacion;
using Bussines_Logic_Layer.Interfaces;
using Data_Access_Layer.Interfaces;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Services
{
    public class PublicacionService : IPublicacionService
    {
        private readonly IPublicacionRepository _repo;
        private readonly IMapper _mapper;

        public PublicacionService(IPublicacionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PublicacionDto>> GetAllAsync()
        {
            var publicaciones = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<PublicacionDto>>(publicaciones);
        }

        public async Task<PublicacionDto?> GetByIdAsync(int id)
        {
            var publicacion = await _repo.GetByIdAsync(id);
            return publicacion == null ? null : _mapper.Map<PublicacionDto>(publicacion);
        }

        public async Task<PublicacionDto> CreateAsync(CreatePublicacionDto dto)
        {
            var publicacion = _mapper.Map<Publicacion>(dto);
            return _mapper.Map<PublicacionDto>(await _repo.AddAsync(publicacion));
        }

        public async Task<bool> UpdateAsync(PublicacionDto dto)
        {
            var publicacion = await _repo.GetByIdAsync(dto.idPublicacion);
            if (publicacion == null)
                return false;

            _mapper.Map(dto, publicacion);
            await _repo.UpdateAsync(publicacion);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var publicacion = await _repo.GetByIdAsync(id);
            if (publicacion == null)
                return false;

            await _repo.DeleteAsync(publicacion);
            return true;
        }

        public async Task<bool> LogicDeleteAsync(int id)
        {
            var publicacion = await _repo.GetByIdAsync(id);
            if (publicacion == null)
                return false;

            publicacion.Status = "Eliminada";

            await _repo.UpdateAsync(publicacion);
            return true;
        }
    }
}
