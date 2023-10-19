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
public class TipoNotificacionController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TipoNotificacionController(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TipoNotificacionDto>>> Get()
    {
        var tiponoti = await _unitOfWork.TipoNotificaciones.GetAllAsync();
        return _mapper.Map<List<TipoNotificacionDto>>(tiponoti);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TipoNotificacionDto>> Get(int id)
    {
        var tiponoti = await _unitOfWork.TipoNotificaciones.GetByIdAsync(id);
        if(tiponoti == null)
        {
            return NotFound();
        }
        return _mapper.Map<TipoNotificacionDto>(tiponoti);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TipoNotificacionDto>> Post([FromBody] TipoNotificacionDto tiponotiDto)
    {
        var tiponoti = _mapper.Map<TipoNotificacion>(tiponotiDto);

        if(tiponoti == null)
            return BadRequest();
        if (tiponotiDto.FechaCreacion == DateOnly.MinValue)
        {
            tiponotiDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            tiponoti.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (tiponoti.FechaModificacion == DateOnly.MinValue)
        {
            tiponotiDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            tiponoti.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.TipoNotificaciones.Add(tiponoti);
        await _unitOfWork.SaveAsync();
        tiponotiDto.Id = tiponoti.Id;
        return CreatedAtAction(nameof(Post),new {id = tiponoti.Id},tiponotiDto);
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TipoNotificacionDto>> Put(int id, [FromBody] TipoNotificacionDto tiponotiDto)
    {
        if(tiponotiDto.Id == 0)
        {
            tiponotiDto.Id = id;
        }
        if(tiponotiDto.Id != id)
        {
            return NotFound();
        }
        var tiponoti = _mapper.Map<TipoNotificacion>(tiponotiDto);
        if(tiponoti==null)
            return BadRequest();
        if(tiponoti.FechaCreacion == DateOnly.MinValue)
        {
            tiponotiDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            tiponoti.FechaCreacion = DateOnly.FromDateTime(DateTime.Now); 
        }
        if(tiponotiDto.FechaModificacion == DateOnly.MinValue)
        {
            tiponotiDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            tiponoti.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.TipoNotificaciones.Update(tiponoti);
        await _unitOfWork.SaveAsync();
        return tiponotiDto;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var tiponoti = await _unitOfWork.TipoNotificaciones.GetByIdAsync(id);
        if(tiponoti == null)
            return NotFound();
        _unitOfWork.TipoNotificaciones.Remove(tiponoti);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

}
