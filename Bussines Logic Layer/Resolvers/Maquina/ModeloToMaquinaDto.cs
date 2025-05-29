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
    public class ModeloToMaquinaDto : IValueResolver<Domain_Layer.Entidades.Maquina, MaquinaDto, ModeloDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ModeloToMaquinaDto(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ModeloDto Resolve(Domain_Layer.Entidades.Maquina source, MaquinaDto destination, ModeloDto destMember, ResolutionContext context)
        {
            var modeloExistente = _context.Modelos.FirstOrDefault(m => m.ModeloName.Equals(source.Modelo.ModeloName));

            if (modeloExistente == null)
            {
                throw new Exception("El tipo no existe");
            }
            return _mapper.Map<ModeloDto>(modeloExistente);
        }
    }
}
