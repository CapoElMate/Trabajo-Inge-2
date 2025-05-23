using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Data_Access_Layer;
using Domain_Layer.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Bussines_Logic_Layer.Resolvers
{
    public class TMToMaquinaDto : IValueResolver<Maquina, MaquinaDto, TipoMaquinaDto>
    {
        private readonly ApplicationDbContext _context;

        public TMToMaquinaDto(ApplicationDbContext context)
        {
            _context = context;
        }

        public TipoMaquinaDto Resolve(Maquina source, MaquinaDto destination, TipoMaquinaDto destMember, ResolutionContext context)
        {
            var tipoExistente = _context.TiposMaquina.FirstOrDefault(t => t.Tipo.Equals(source.TipoMaquina.Tipo));

            if (tipoExistente == null)
            {
                throw new Exception("El tipo no existe");
            }
            return new TipoMaquinaDto
            {
                Tipo = tipoExistente.Tipo
            };
        }
    }
}
