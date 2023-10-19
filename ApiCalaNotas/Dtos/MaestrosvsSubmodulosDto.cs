using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;
public class MaestrosvsSubmodulosDto
{
    public int Id { get; set; }
    public DateOnly FechaCreacion { get; set; }
    public DateOnly FechaModificacion { get; set; }
    public int IdModulosMaestroFk { get; set; }
    public int IdSubModulosFk { get; set; }
}
