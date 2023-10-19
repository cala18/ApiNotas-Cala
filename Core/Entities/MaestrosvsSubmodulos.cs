using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;
public class MaestrosvsSubmodulos : BaseEntity
{   
    [Required]
    public DateOnly FechaCreacion { get; set; }
    [Required]
    public DateOnly FechaModificacion { get; set; }
    public int IdModulosMaestroFk { get; set; }
    public ModulosMaestro ModulosMaestros { get; set; }
    public int IdSubModulosFk { get; set; }
    public SubModulos SubModulos { get; set; }
    public ICollection<GenericovsSubmodulos> GenericovsSubmodulos { get; set; }
}
