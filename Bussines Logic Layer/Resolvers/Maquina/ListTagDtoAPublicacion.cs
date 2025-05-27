using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Data_Access_Layer;
using Domain_Layer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines_Logic_Layer.Resolvers.Publicacion
{
    public class ListTagDtoAPublicacion : IValueResolver<PublicacionDto, Domain_Layer.Entidades.Publicacion, ICollection<TagPublicacion>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ListTagDtoAPublicacion(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICollection<TagPublicacion> Resolve(PublicacionDto source, Domain_Layer.Entidades.Publicacion destination, ICollection<TagPublicacion> destMember, ResolutionContext context)
        {
            // Si no se proporcionan tags de publicacion en el DTO, retornar una lista vacía
            if (source.TagsPublicacion == null || !source.TagsPublicacion.Any())
            {
                return new List<TagPublicacion>();
            }

            var tagsPublicacion = _context.TagsPublicacion
                .Where(t => source.TagsPublicacion.Select(st => st.Tag).Contains(t.Tag))
                .ToList();

            if (tagsPublicacion == null || !tagsPublicacion.Any())
            {
                throw new Exception("No existen tags de publicacionria para los valores proporcionados");
            }

            return tagsPublicacion;
        }
    }
}
