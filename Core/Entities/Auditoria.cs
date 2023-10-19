using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;
public class Auditoria : BaseEntity
{
    [Required]
    public string NombreUsuario { get; set; }
    [Required]
    public int DescAccion { get; set; }
    [Required]
    public DateOnly FechaCreacion { get; set; }
    [Required]
    public DateOnly FechaModificacion { get; set; }
    public ICollection<Blockchain> Blockchains { get; set; }
    
}