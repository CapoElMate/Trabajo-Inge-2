using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Maquina;
using Data_Access_Layer;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Resolvers.Maquina.Create
{
    public class ListTAMaquinaToMaquina : IValueResolver<MaquinaDto, Domain_Layer.Entidades.Maquina, ICollection<TagMaquina>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ListTAMaquinaToMaquina(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICollection<TagMaquina> Resolve(MaquinaDto source, Domain_Layer.Entidades.Maquina destination, ICollection<TagMaquina> destMember, ResolutionContext context)
        {
            if (source.TagsMaquina == null || !source.TagsMaquina.Any())
            {
                return new List<TagMaquina>();
            }

            var tagsMaquinaria = _context.TagsMaquina
                .Where(t => source.TagsMaquina.Select(st => st.Tag).Contains(t.Tag))
                .ToList();

            if (tagsMaquinaria == null || !tagsMaquinaria.Any())
            {
                throw new Exception("No existen tags de maquinaria para los valores proporcionados");
            }

            return tagsMaquinaria;
        }
    }
}
