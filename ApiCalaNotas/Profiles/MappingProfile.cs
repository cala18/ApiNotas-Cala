using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API.Profiles;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Auditoria,AuditoriaDto>().ReverseMap();
        CreateMap<Blockchain,BlockchainDto>().ReverseMap();
        CreateMap<EstadoNotificacion,EstadoNotificacionDto>().ReverseMap();
        CreateMap<Formato,FormatoDto>().ReverseMap();
        CreateMap<GenericovsSubmodulos,GenericovsSubmodulosDto>().ReverseMap();
        CreateMap<HiloRespuestaNotificacion,HiloRespuestaNotificacionDto>().ReverseMap();
        CreateMap<MaestrosvsSubmodulos,MaestrosvsSubmodulosDto>().ReverseMap();
        CreateMap<ModuloNotificacion,ModuloNotificacionDto>().ReverseMap();
        CreateMap<ModulosMaestro,ModulosMaestroDto>().ReverseMap();
        CreateMap<PermisosGenericos,PermisosGenericosDto>().ReverseMap();
        CreateMap<Radicados,RadicadosDto>().ReverseMap();
        CreateMap<Rol,RolDto>().ReverseMap();
        CreateMap<RolvsMaestro,RolvsMaestroDto>().ReverseMap();
        CreateMap<SubModulos,SubmodulosDto>().ReverseMap();
        CreateMap<TipoNotificacion,TipoNotificacionDto>().ReverseMap();
        CreateMap<TipoRequerimiento,TipoRequerimientosDto>().ReverseMap();
    }
}
