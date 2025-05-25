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
    public class ModeloMaquinaToMaquinaResolver : IValueResolver<MaquinaDto, Domain_Layer.Entidades.Maquina, Modelo>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ModeloMaquinaToMaquinaResolver(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Modelo Resolve(MaquinaDto source, Domain_Layer.Entidades.Maquina destination, Modelo destMember, ResolutionContext context)
        {
            var modeloExistente = _context.Modelos.FirstOrDefault(m => m.ModeloName.Equals(source.Modelo.Modelo));

            if (modeloExistente == null)
            {
                throw new Exception("La marca no existe");
            }

            return modeloExistente;
        }
    }
}