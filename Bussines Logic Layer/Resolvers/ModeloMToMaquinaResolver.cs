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
    public class ModeloMToMaquinaResolver : IValueResolver<CreateMaquinaDto, Maquina, Modelo>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ModeloMToMaquinaResolver(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Modelo Resolve(CreateMaquinaDto source, Maquina destination, Modelo destMember, ResolutionContext context)
        {
            var modeloExistente = _context.Modelos.FirstOrDefault(m => m.ModeloName.Equals(source.Modelo.Modelo));

            if (modeloExistente == null)
            {
                throw new Exception("La marca no existe");
            }

            _mapper.Map(source.Modelo, modeloExistente);
            return modeloExistente;
        }
    }
}