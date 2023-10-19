using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;
public class HiloRespuestaNotificacionDto
{
    public int Id { get; set; }
    public string NombreHiloRespuesta { get; set; }
    public DateOnly FechaCreacion { get; set; }
    public DateOnly FechaModificacion { get; set; }
}
