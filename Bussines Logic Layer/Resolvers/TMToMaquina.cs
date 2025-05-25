using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs.Maquina;
using Data_Access_Layer;
using Domain_Layer.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Bussines_Logic_Layer.Resolvers
{
    public class TMToMaquina : IValueResolver<CreateMaquinaDto, Maquina, TipoMaquina>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TMToMaquina(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public TipoMaquina Resolve(CreateMaquinaDto source, Maquina destination, TipoMaquina destMember, ResolutionContext context)
        {
            var tipoExistente = _context.TiposMaquina.FirstOrDefault(t => t.Tipo.Equals(source.TipoMaquina.Tipo));

            if (tipoExistente == null)
            {
                throw new Exception("El tipo no existe");
            }

            _mapper.Map(source.TipoMaquina, tipoExistente);
            return tipoExistente;
        }
    }
}
