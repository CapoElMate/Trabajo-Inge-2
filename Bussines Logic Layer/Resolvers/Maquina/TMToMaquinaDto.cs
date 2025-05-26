using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Maquina;
using Data_Access_Layer;
using Domain_Layer.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Bussines_Logic_Layer.Resolvers.Maquina
{
    public class TMToMaquinaDto : IValueResolver<Domain_Layer.Entidades.Maquina, MaquinaDto, TipoMaquinaDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TMToMaquinaDto(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public TipoMaquinaDto Resolve(Domain_Layer.Entidades.Maquina source, MaquinaDto destination, TipoMaquinaDto destMember, ResolutionContext context)
        {
            var tipoExistente = _context.TiposMaquina.FirstOrDefault(t => t.Tipo.Equals(source.TipoMaquina.Tipo));

            if (tipoExistente == null)
            {
                throw new Exception("El tipo no existe");
            }
            //return new TipoMaquinaDto
            //{
            //    Tipo = tipoExistente.Tipo
            //};
            return _mapper.Map<TipoMaquinaDto>(tipoExistente);
        }
    }
}
