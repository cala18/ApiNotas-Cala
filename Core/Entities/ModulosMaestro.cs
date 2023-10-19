using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;
public class ModulosMaestro : BaseEntity
{
    [Required]
    public string NombreModuloMaestro { get; set; }
    [Required]
    public DateOnly FechaCreacion { get; set; }
    [Required]
    public DateOnly FechaModificacion { get; set; }
    public ICollection<RolvsMaestro> RolvsMaestros { get; set; }
    public ICollection<MaestrosvsSubmodulos> MaestrosvsSubmodulos { get; set; }

}
