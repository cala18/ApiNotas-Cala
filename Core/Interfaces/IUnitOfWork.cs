using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces;
public interface IUnitOfWork 
{
    IAuditoria Auditorias {get;}
    IBlockchain Blockchains {get;}
    IEstadoNotificacion EstadoNotificaciones {get;}
    IFormato Formatos {get;}
    IGenericovsSubmodulos GenericovsSubmodulos {get;}
    IHiloRespuestaNotificacion HiloRespuestaNotificaciones {get;}
    IMaestrovsSubmodulos MaestrovsSubmodulos {get;}
    IModuloNotificacion ModuloNotificaciones {get;}
    IModulosMaestro ModulosMaestros {get;}
    IPermisoGenericos PermisoGenericos {get;}
    IRadicados Radicados {get;}
    IRol Rols {get;}
    IRolvsMaestro RolvsMaestros {get;}
    ISubModulos SubModulos {get;}
    ITipoNotificacion TipoNotificaciones {get;}
    ITipoRequerimientos TipoRequerimientos {get;}
    Task<int> SaveAsync();
}