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
public class TipoRequerimientosController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TipoRequerimientosController(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TipoRequerimientosDto>>> Get()
    {
        var tiporequi = await _unitOfWork.TipoRequerimientos.GetAllAsync();
        return _mapper.Map<List<TipoRequerimientosDto>>(tiporequi);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TipoRequerimientosDto>> Get(int id)
    {
        var tiporequi = await _unitOfWork.TipoRequerimientos.GetByIdAsync(id);
        if(tiporequi == null)
        {
            return NotFound();
        }
        return _mapper.Map<TipoRequerimientosDto>(tiporequi);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TipoRequerimientosDto>> Post([FromBody] TipoRequerimientosDto tiporequiDto)
    {
        var tiporequi = _mapper.Map<TipoRequerimiento>(tiporequiDto);

        if(tiporequi == null)
            return BadRequest();
        if (tiporequiDto.FechaCreacion == DateOnly.MinValue)
        {
            tiporequiDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            tiporequi.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (tiporequi.FechaModificacion == DateOnly.MinValue)
        {
            tiporequiDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            tiporequi.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.TipoRequerimientos.Add(tiporequi);
        await _unitOfWork.SaveAsync();
        tiporequiDto.Id = tiporequi.Id;
        return CreatedAtAction(nameof(Post),new {id = tiporequi.Id},tiporequiDto);
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TipoRequerimientosDto>> Put(int id, [FromBody] TipoRequerimientosDto tiporequiDto)
    {
        if(tiporequiDto.Id == 0)
        {
            tiporequiDto.Id = id;
        }
        if(tiporequiDto.Id != id)
        {
            return NotFound();
        }
        var tiporequi = _mapper.Map<TipoRequerimiento>(tiporequiDto);
        if(tiporequi==null)
            return BadRequest();
        if(tiporequi.FechaCreacion == DateOnly.MinValue)
        {
            tiporequiDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            tiporequi.FechaCreacion = DateOnly.FromDateTime(DateTime.Now); 
        }
        if(tiporequiDto.FechaModificacion == DateOnly.MinValue)
        {
            tiporequiDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            tiporequi.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.TipoRequerimientos.Update(tiporequi);
        await _unitOfWork.SaveAsync();
        return tiporequiDto;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var tiporequi = await _unitOfWork.TipoRequerimientos.GetByIdAsync(id);
        if(tiporequi == null)
            return NotFound();
        _unitOfWork.TipoRequerimientos.Remove(tiporequi);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

}
