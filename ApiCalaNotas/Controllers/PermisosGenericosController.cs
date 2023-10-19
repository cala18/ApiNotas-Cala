using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class PermisosGenericosController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PermisosGenericosController(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PermisosGenericosDto>>> Get()
    {
        var permisosgen = await _unitOfWork.PermisoGenericos.GetAllAsync();
        return _mapper.Map<List<PermisosGenericosDto>>(permisosgen);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PermisosGenericosDto>> Get(int id)
    {
        var permisosgen = await _unitOfWork.PermisoGenericos.GetByIdAsync(id);
        if(permisosgen == null)
        {
            return NotFound();
        }
        return _mapper.Map<PermisosGenericosDto>(permisosgen);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PermisosGenericosDto>> Post([FromBody] PermisosGenericosDto permisosgenDto)
    {
        var permisosgen = _mapper.Map<PermisosGenericos>(permisosgenDto);

        if(permisosgen == null)
            return BadRequest();
        if (permisosgenDto.FechaCreacion == DateOnly.MinValue)
        {
            permisosgenDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            permisosgen.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (permisosgen.FechaModificacion == DateOnly.MinValue)
        {
            permisosgenDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            permisosgen.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.PermisoGenericos.Add(permisosgen);
        await _unitOfWork.SaveAsync();
        permisosgenDto.Id = permisosgen.Id;
        return CreatedAtAction(nameof(Post),new {id = permisosgen.Id},permisosgenDto);
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PermisosGenericosDto>> Put(int id, [FromBody] PermisosGenericosDto permisosgenDto)
    {
        if(permisosgenDto.Id == 0)
        {
            permisosgenDto.Id = id;
        }
        if(permisosgenDto.Id != id)
        {
            return NotFound();
        }
        var permisosgen = _mapper.Map<PermisosGenericos>(permisosgenDto);
        if(permisosgen==null)
            return BadRequest();
        if(permisosgen.FechaCreacion == DateOnly.MinValue)
        {
            permisosgenDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            permisosgen.FechaCreacion = DateOnly.FromDateTime(DateTime.Now); 
        }
        if(permisosgenDto.FechaModificacion == DateOnly.MinValue)
        {
            permisosgenDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            permisosgen.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.PermisoGenericos.Update(permisosgen);
        await _unitOfWork.SaveAsync();
        return permisosgenDto;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var permisosgen = await _unitOfWork.PermisoGenericos.GetByIdAsync(id);
        if(permisosgen == null)
            return NotFound();
        _unitOfWork.PermisoGenericos.Remove(permisosgen);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

}
