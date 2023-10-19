using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace API.Dtos;
public class BlockchainDto
{
    public int Id { get; set; }
    public string HashGenerado { get; set; }
    public DateOnly FechaCreacion { get; set; }
    public DateOnly FechaModificacion { get; set; }
    public int IdAuditoriaFk { get; set; }
    public int IdTipoNotificacionesFk { get; set; }
    public int IdHiloRespuestaFk { get; set; }

}
