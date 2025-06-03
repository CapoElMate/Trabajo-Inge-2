using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Alquiler;
using Data_Access_Layer;

namespace Bussines_Logic_Layer.Resolvers.Alquiler
{
    public class CreateEmpleadoDtoToAlquiler : IValueResolver<CreateAlquilerDto, Domain_Layer.Entidades.Alquiler, Domain_Layer.Entidades.Empleado>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateEmpleadoDtoToAlquiler(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public Domain_Layer.Entidades.Empleado Resolve(CreateAlquilerDto source, Domain_Layer.Entidades.Alquiler destination
                                           , Domain_Layer.Entidades.Empleado destMember, ResolutionContext context)
        {

            var reservaExistente = _context.Empleados.FirstOrDefault(m => m.DNI.Equals(source.DNIEmpleadoEfectivizo));

            if (reservaExistente == null)
            {
                throw new Exception("No existe un empleado para los valores proporcionados");
            }

            return _mapper.Map<Domain_Layer.Entidades.Empleado>(reservaExistente);
        }

    }
}
