using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Data_Access_Layer;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Resolvers
{
    public class ListTAToMaquina : IValueResolver<CreateMaquinaDto, Maquina, ICollection<TagMaquina>>
    {
        private readonly ApplicationDbContext _context;

        public ListTAToMaquina(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<TagMaquina> Resolve(CreateMaquinaDto source, Maquina destination, ICollection<TagMaquina> destMember, ResolutionContext context)
        {
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
