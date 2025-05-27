using AutoMapper;
using Bussines_Logic_Layer.DTOs.Maquina;
using Bussines_Logic_Layer.DTOs;
using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Resolvers.Publicacion
{
    public class ListTagAPublicacionDto : IValueResolver<Domain_Layer.Entidades.Publicacion, PublicacionDto, ICollection<TagPublicacionDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ListTagAPublicacionDto(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public ICollection<TagPublicacionDto> Resolve(Domain_Layer.Entidades.Publicacion source, PublicacionDto destination
                                                        ,ICollection<TagPublicacionDto> destMember, ResolutionContext context)
        {
            // Si no se proporcionan tags de publicacion en el DTO, retornar una lista vacía
            if (source.TagsPublicacion == null || !source.TagsPublicacion.Any())
            {
                return new List<TagPublicacionDto>();
            }

            var tagsPublicacion = _context.TagsPublicacion
                .Where(t => source.TagsPublicacion.Select(st => st.Tag).Contains(t.Tag))
                .ToList();

            if (tagsPublicacion == null || !tagsPublicacion.Any())
            {
                throw new Exception("No existen tags de publicacion para los valores proporcionados");
            }

            return _mapper.Map<ICollection<TagPublicacionDto>>(tagsPublicacion);
        }
    }
    
}
