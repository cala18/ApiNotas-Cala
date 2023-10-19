using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;
public class TipoNotificacion : BaseEntity
{
    [Required]
    public string NombreTipoNotificacion { get; set; }
    [Required]
    public DateOnly FechaCreacion { get; set; }
    [Required]
    public DateOnly FechaModificacion { get; set; }
    public ICollection<ModuloNotificacion> ModuloNotificaciones { get; set; }
    public ICollection<Blockchain> Blockchains { get; set; }
}
