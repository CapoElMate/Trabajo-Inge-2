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
    public class ModeloDtoToModeloResolver : IValueResolver<ModeloDto, Modelo, Marca>
    {
        private readonly ApplicationDbContext _context;

        public ModeloDtoToModeloResolver(ApplicationDbContext context)
        {
            _context = context;
        }

        public Marca Resolve(ModeloDto source, Modelo destination, Marca destMember, ResolutionContext context)
        {
            var marcaExistente = _context.Marcas.FirstOrDefault(m => m.MarcaName.Equals(source.Marca.Marca));

            if (marcaExistente == null)
            {
                throw new Exception("La marca no existe");
            }
            return marcaExistente;
        }
    }
}
