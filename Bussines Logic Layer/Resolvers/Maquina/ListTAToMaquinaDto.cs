using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Maquina;
using Data_Access_Layer;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Resolvers
{
    public class ListTAToMaquinaDto : IValueResolver<Domain_Layer.Entidades.Maquina, MaquinaDto, ICollection<TagMaquinaDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ListTAToMaquinaDto(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICollection<TagMaquinaDto> Resolve(Domain_Layer.Entidades.Maquina source, MaquinaDto destination, ICollection<TagMaquinaDto> destMember, ResolutionContext context)
        {
            var tagsMaquinaria = _context.TagsMaquina
                .Where(t => source.TagsMaquina.Select(st => st.Tag).Contains(t.Tag))
                .ToList();

            if (tagsMaquinaria == null || tagsMaquinaria.Count == 0)
            {
                //return new List<TagMaquinaDto>();
                throw new Exception("No existen tags de maquinaria para los valores proporcionados");
            }

            //return tagsMaquinaria
            //    .Select(t => new TagMaquinaDto { Tag = t.Tag })
            //    .ToList();
            return _mapper.Map<ICollection<TagMaquinaDto>>(tagsMaquinaria);
        }
    }
}
