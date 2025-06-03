using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Data_Access_Layer;
using global::Bussines_Logic_Layer.DTOs;
using global::Bussines_Logic_Layer.DTOs.Maquina;
using System;
using System.Linq;
using Bussines_Logic_Layer.DTOs.Publicacion;

namespace Bussines_Logic_Layer.Resolvers.Publicacion.Create
{

    public class CreateMaquinaDtoToPublicacion : IValueResolver<CreatePublicacionDto, Domain_Layer.Entidades.Publicacion, Domain_Layer.Entidades.Maquina>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateMaquinaDtoToPublicacion(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Domain_Layer.Entidades.Maquina Resolve(CreatePublicacionDto source, Domain_Layer.Entidades.Publicacion destination, Domain_Layer.Entidades.Maquina destMember, ResolutionContext context)
        {
            var publicacionExistente = _context.Maquinas.FirstOrDefault(m => m.idMaquina.Equals(source.Maquina.IdMaquina));

            if (publicacionExistente == null)
            {
                throw new Exception("La maquina no existe");
            }

            //_mapper.Map(source.Marca, publicacionExistente);
            return publicacionExistente;
        }
    }
}
