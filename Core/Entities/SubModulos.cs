using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;
public class SubModulos : BaseEntity
{
    [Required]
    public string NombreSubModulo { get; set; }
    [Required]
    public DateOnly FechaCreacion { get; set; }
    [Required]
    public DateOnly FechaModificacion { get; set; }
    public ICollection<MaestrosvsSubmodulos> MaestrosvsSubmodulos { get; set; }
}