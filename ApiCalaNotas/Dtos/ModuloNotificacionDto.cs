using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;
public class ModuloNotificacionDto
{
    public int Id { get; set; }
    public string AsuntoNotificacion { get; set; }
    public string TextoNotificacion { get; set; }
    public DateOnly FechaCreacion { get; set; }
    public DateOnly FechaModificacion { get; set; }
    public int IdRadicadosFk { get; set; }
    public int IdFormatosFk { get; set; }
    public int IdTipoRequerimientoFk { get; set; }
    public int IdEstadoNotificacionFk { get; set; }
    public int IdTipoNotificacionesFk { get; set; }
    public int IdHiloRespuestaFk { get; set; }
    
}
