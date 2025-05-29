using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Bussines_Logic_Layer.Mapping
{
    public class ArchivoMap: Profile
    {
        public ArchivoMap()
        {
            CreateMap<Domain_Layer.Entidades.Archivo, Bussines_Logic_Layer.DTOs.Archivo.ArchivoDtoReponse>()
                .ForMember(dest => dest.idArchivo, opt => opt.MapFrom(src => src.idArchivo))
                .ForMember(dest => dest.EntidadID, opt => opt.MapFrom(src => src.EntidadID))
                .ForMember(dest => dest.TipoEntidad, opt => opt.MapFrom(src => src.TipoEntidad))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion));
            CreateMap<Domain_Layer.Entidades.Archivo, Bussines_Logic_Layer.DTOs.Archivo.ArchivoDtoUpload>()
                .ForMember(dest => dest.EntidadID, opt => opt.MapFrom(src => src.EntidadID))
                .ForMember(dest => dest.TipoEntidad, opt => opt.MapFrom(src => src.TipoEntidad))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion));
            CreateMap<Bussines_Logic_Layer.DTOs.Archivo.ArchivoDtoUpload, Domain_Layer.Entidades.Archivo>()
                .ForMember(dest => dest.EntidadID, opt => opt.MapFrom(src => src.EntidadID))
                .ForMember(dest => dest.TipoEntidad, opt => opt.MapFrom(src => src.TipoEntidad))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(dest => dest.Ruta, opt => opt.Ignore())
                .ForMember(dest => dest.TipoContenido, opt => opt.Ignore());
        }
    }
}
