using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Data_Access_Layer;
using Domain_Layer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines_Logic_Layer.DTOs.Maquina;




namespace Bussines_Logic_Layer.Resolvers.Publicacion
{
    public class MaquinaDtoToPublicacion : IValueResolver<PublicacionDto, Domain_Layer.Entidades.Publicacion, Domain_Layer.Entidades.Maquina>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MaquinaDtoToPublicacion(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Domain_Layer.Entidades.Maquina Resolve(PublicacionDto source, Domain_Layer.Entidades.Publicacion destination, Domain_Layer.Entidades.Maquina destMember, ResolutionContext context)
        {
            var publicacionExistente = _context.Maquinas.FirstOrDefault(m => m.idMaquina.Equals(source.Maquina.IdMaquina));

            if (publicacionExistente == null)
            {
                throw new Exception("La publicacion no existe");
            }

            //_mapper.Map(source.Marca, publicacionExistente);
            return publicacionExistente;
        }
    }    
}
