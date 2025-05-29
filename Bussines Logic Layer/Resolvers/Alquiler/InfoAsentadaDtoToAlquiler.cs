using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Alquiler;
using Data_Access_Layer;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Resolvers.Alquiler
{
    public class InfoAsentadaDtoToAlquiler : IValueResolver<AlquilerDto, Domain_Layer.Entidades.Alquiler, ICollection<InfoAsentada>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public InfoAsentadaDtoToAlquiler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICollection<InfoAsentada> Resolve(AlquilerDto source, Domain_Layer.Entidades.Alquiler destination, ICollection<InfoAsentada> destMember, ResolutionContext context)
        {
            // Si no se proporcionan tags de Alquiler en el DTO, retornar una lista vacía
            if (source.InfoAsentada == null || !source.InfoAsentada.Any())
            {
                return new List<InfoAsentada>();
            }

            var infoAsentada = _context.InfoAsentada
                .Where(i => source.InfoAsentada.Select(si => si.idInfo).Contains(i.idInfo))
                .ToList();

            if (infoAsentada == null || !infoAsentada.Any())
            {
                throw new Exception("No existe informacion asentada para el alquiler proporcionado.");
            }

            return infoAsentada;
        }
    }
}
