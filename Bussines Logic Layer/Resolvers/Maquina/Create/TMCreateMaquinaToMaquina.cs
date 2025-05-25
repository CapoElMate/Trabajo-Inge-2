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

namespace Bussines_Logic_Layer.Resolvers.Maquina.Create
{
    public class TMCreateMaquinaToMaquina : IValueResolver<CreateMaquinaDto, Domain_Layer.Entidades.Maquina, TipoMaquina>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TMCreateMaquinaToMaquina(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public TipoMaquina Resolve(CreateMaquinaDto source, Domain_Layer.Entidades.Maquina destination, TipoMaquina destMember, ResolutionContext context)
        {
            var tipoExistente = _context.TiposMaquina.FirstOrDefault(t => t.Tipo.Equals(source.TipoMaquina.Tipo));

            if (tipoExistente == null)
            {
                throw new Exception("El tipo no existe");
            }
            return tipoExistente;
        }
    }
}
