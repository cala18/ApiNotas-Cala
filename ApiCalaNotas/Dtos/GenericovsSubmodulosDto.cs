using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;
public class GenericovsSubmodulosDto
{
    public int Id { get; set; }
    public DateOnly FechaCreacion { get; set; }
    public DateOnly FechaModificacion { get; set; }
    public int IdPermisoGenericoFk { get; set; }
    public int IdRolFk { get; set; }
    public int IdMaestrovsSubmodulosFk { get; set; }
}
