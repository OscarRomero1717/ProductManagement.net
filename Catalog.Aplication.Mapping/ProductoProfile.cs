using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.EventHadlers;
using Catalog.EventHadlers._01.Commands;
using Catalog.QueriesService;
using ProductManagement.Domain;

namespace Catalog.Aplication.Mapping
{

    public class ProductoProfile : Profile
    {
        public ProductoProfile()
        {
            CreateMap<Producto, ProductoDto>()
               .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.or_nombre))
               .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.or_codigo))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.or_id))
                 .ForMember(dest => dest.UnidadMedida, opt => opt.MapFrom(src => src.or_unidad_medida))
                  .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.or_fecha_creacion))
                   .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.or_descripcion))
                    .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.or_estado))
                    .ForMember(dest => dest.PrecioUnitario, opt => opt.MapFrom(src => src.or_precio_unitario))
                    .ForMember(dest => dest.RefInterna, opt => opt.MapFrom(src => src.or_referencia_interna))
               .ReverseMap();


            CreateMap<Producto, ProductoCreateCommand>()
               .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.or_nombre))
               .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.or_codigo))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.or_id))
                 .ForMember(dest => dest.UnidadMedida, opt => opt.MapFrom(src => src.or_unidad_medida))
                  .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.or_fecha_creacion))
                   .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.or_descripcion))
                    .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.or_estado))
                    .ForMember(dest => dest.PrecioUnitario, opt => opt.MapFrom(src => src.or_precio_unitario))
                    .ForMember(dest => dest.RefInterna, opt => opt.MapFrom(src => src.or_referencia_interna))
               .ReverseMap();


            CreateMap<Producto, ProductoUpdateCommand>()
               .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.or_nombre))
               .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.or_codigo))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.or_id))
                 .ForMember(dest => dest.UnidadMedida, opt => opt.MapFrom(src => src.or_unidad_medida))
                  .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.or_fecha_creacion))
                   .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.or_descripcion))
                    .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.or_estado))
                    .ForMember(dest => dest.PrecioUnitario, opt => opt.MapFrom(src => src.or_precio_unitario))
                    .ForMember(dest => dest.RefInterna, opt => opt.MapFrom(src => src.or_referencia_interna))
               .ReverseMap();

        }
    }

}
