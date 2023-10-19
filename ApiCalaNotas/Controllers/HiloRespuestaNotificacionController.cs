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
public class HiloRespuestaNotificacionController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public HiloRespuestaNotificacionController(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<HiloRespuestaNotificacionDto>>> Get()
    {
        var hilorespuesta = await _unitOfWork.HiloRespuestaNotificaciones.GetAllAsync();
        return _mapper.Map<List<HiloRespuestaNotificacionDto>>(hilorespuesta);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HiloRespuestaNotificacionDto>> Get(int id)
    {
        var hilorespuesta = await _unitOfWork.HiloRespuestaNotificaciones.GetByIdAsync(id);
        if(hilorespuesta == null)
        {
            return NotFound();
        }
        return _mapper.Map<HiloRespuestaNotificacionDto>(hilorespuesta);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HiloRespuestaNotificacionDto>> Post([FromBody] HiloRespuestaNotificacionDto hilorespuestaDto)
    {
        var hilorespuesta = _mapper.Map<HiloRespuestaNotificacion>(hilorespuestaDto);

        if(hilorespuesta == null)
            return BadRequest();
        if (hilorespuestaDto.FechaCreacion == DateOnly.MinValue)
        {
            hilorespuestaDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            hilorespuesta.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (hilorespuesta.FechaModificacion == DateOnly.MinValue)
        {
            hilorespuestaDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            hilorespuesta.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.HiloRespuestaNotificaciones.Add(hilorespuesta);
        await _unitOfWork.SaveAsync();
        hilorespuestaDto.Id = hilorespuesta.Id;
        return CreatedAtAction(nameof(Post),new {id = hilorespuesta.Id},hilorespuestaDto);
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HiloRespuestaNotificacionDto>> Put(int id, [FromBody] HiloRespuestaNotificacionDto hiloRespuestaDto)
    {
        if(hiloRespuestaDto.Id == 0)
        {
            hiloRespuestaDto.Id = id;
        }
        if(hiloRespuestaDto.Id != id)
        {
            return NotFound();
        }
        var hiloRespuesta = _mapper.Map<HiloRespuestaNotificacion>(hiloRespuestaDto);
        if(hiloRespuesta==null)
            return BadRequest();
        if(hiloRespuesta.FechaCreacion == DateOnly.MinValue)
        {
            hiloRespuestaDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            hiloRespuesta.FechaCreacion = DateOnly.FromDateTime(DateTime.Now); 
        }
        if(hiloRespuestaDto.FechaModificacion == DateOnly.MinValue)
        {
            hiloRespuestaDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            hiloRespuesta.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.HiloRespuestaNotificaciones.Update(hiloRespuesta);
        await _unitOfWork.SaveAsync();
        return hiloRespuestaDto;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var hilorespuesta = await _unitOfWork.HiloRespuestaNotificaciones.GetByIdAsync(id);
        if(hilorespuesta == null)
            return NotFound();
        _unitOfWork.HiloRespuestaNotificaciones.Remove(hilorespuesta);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

}
