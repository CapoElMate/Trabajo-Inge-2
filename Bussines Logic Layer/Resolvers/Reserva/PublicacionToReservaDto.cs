using AutoMapper;
using Bussines_Logic_Layer.DTOs.Maquina;
using Bussines_Logic_Layer.DTOs;
using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;
using Bussines_Logic_Layer.DTOs.Reserva;
using Bussines_Logic_Layer.DTOs.Publicacion;

namespace Bussines_Logic_Layer.Resolvers.Reserva
{
    public class PublicacionToReservaDto : IValueResolver<Domain_Layer.Entidades.Reserva, ReservaDto, PublicacionDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PublicacionToReservaDto(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public PublicacionDto Resolve(Domain_Layer.Entidades.Reserva source, ReservaDto destination
                                    , PublicacionDto destMember, ResolutionContext context)
        {

            var publicacionExistente = _context.Publicaciones.FirstOrDefault(p => p.idPublicacion.Equals(source.Publicacion.idPublicacion));

            if (publicacionExistente == null)
            {
                throw new Exception("No existe un publicacion para los valores proporcionados");
            }

            return _mapper.Map<PublicacionDto>(publicacionExistente);
        }

    }
}
