using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;
public class GenericovsSubmodulos : BaseEntity
{
    [Required]
    public DateOnly FechaCreacion { get; set; }
    [Required]
    public DateOnly FechaModificacion { get; set; }
    public int IdPermisoGenericoFk { get; set; }
    public PermisosGenericos PermisosGenericos { get; set; }
    public int IdRolFk { get; set; }
    public Rol Rols { get; set; }
    public int IdMaestrovsSubmodulosFk { get; set; }
    public MaestrosvsSubmodulos MaestrosvsSubmodulos { get; set; }

}
