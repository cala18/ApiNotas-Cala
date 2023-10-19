using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;
public class Rol : BaseEntity
{
    [Required]
    public string NombreRol { get; set; }
    [Required]
    public DateOnly FechaCreacion { get; set; }
    [Required]
    public DateOnly FechaModificacion { get; set; }
    public ICollection<RolvsMaestro> RolvsMaestros { get; set; }
    public ICollection<GenericovsSubmodulos> GenericovsSubmodulos { get; set; }
    
}