using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.Controllers;
public class EstadoNotificacionController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EstadoNotificacionController(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EstadoNotificacionDto>>> Get()
    {
        var estadonotificacion = await _unitOfWork.EstadoNotificaciones.GetAllAsync();
        return _mapper.Map<List<EstadoNotificacionDto>>(estadonotificacion);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EstadoNotificacionDto>> Get(int id)
    {
        var estadonotificacion = await _unitOfWork.EstadoNotificaciones.GetByIdAsync(id);
        if(estadonotificacion == null)
            return NotFound();
        return _mapper.Map<EstadoNotificacionDto>(estadonotificacion);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EstadoNotificacionDto>> Post([FromBody] EstadoNotificacionDto estadoNotificacionDto)
    {
        var estadonotificacion = _mapper.Map<EstadoNotificacion>(estadoNotificacionDto);

        if(estadonotificacion == null)
            return BadRequest();
        if(estadoNotificacionDto.FechaCreacion == DateOnly.MinValue)
        {
            estadoNotificacionDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            estadonotificacion.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if(estadoNotificacionDto.FechaModificacion == DateOnly.MinValue)
        {
            estadoNotificacionDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            estadonotificacion.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.EstadoNotificaciones.Add(estadonotificacion);
        await _unitOfWork.SaveAsync();
        
        estadoNotificacionDto.Id = estadonotificacion.Id;
        return CreatedAtAction(nameof(Post),new{ id = estadonotificacion.Id},estadoNotificacionDto);
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EstadoNotificacionDto>> Put(int id,[FromBody]EstadoNotificacionDto estadoNotificacionDto)
    {
        if(estadoNotificacionDto == null)
            return BadRequest();
        if(estadoNotificacionDto.Id == 0)
        {
            estadoNotificacionDto.Id = id;
        }
        if(estadoNotificacionDto.Id != id)
        {
            return NotFound();
        }
        
        var estadonotificacion = _mapper.Map<EstadoNotificacion>(estadoNotificacionDto);

        if(estadoNotificacionDto.FechaCreacion == DateOnly.MinValue)
        {
            estadoNotificacionDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            estadonotificacion.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if(estadoNotificacionDto.FechaModificacion == DateOnly.MinValue)
        {
            estadoNotificacionDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            estadonotificacion.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }

        _unitOfWork.EstadoNotificaciones.Update(estadonotificacion);
        await _unitOfWork.SaveAsync();
        return estadoNotificacionDto;
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        var estadonotificacion = await _unitOfWork.EstadoNotificaciones.GetByIdAsync(id);
        if(estadonotificacion == null)
            return BadRequest();
        
        _unitOfWork.EstadoNotificaciones.Remove(estadonotificacion);
        await _unitOfWork.SaveAsync();
        return NotFound();

    }


}
