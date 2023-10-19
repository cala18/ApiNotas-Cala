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
public class ModuloNotificacionController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ModuloNotificacionController(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ModuloNotificacionDto>>> Get()
    {
        var modulonoti = await _unitOfWork.ModuloNotificaciones.GetAllAsync();
        return _mapper.Map<List<ModuloNotificacionDto>>(modulonoti);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ModuloNotificacionDto>> Get(int id)
    {
        var modulonoti = await _unitOfWork.ModuloNotificaciones.GetByIdAsync(id);
        if(modulonoti == null)
        {
            return NotFound();
        }
        return _mapper.Map<ModuloNotificacionDto>(modulonoti);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ModuloNotificacionDto>> Post([FromBody] ModuloNotificacionDto modulonotiDto)
    {
        var modulonoti = _mapper.Map<ModuloNotificacion>(modulonotiDto);

        if(modulonoti == null)
            return BadRequest();
        if (modulonotiDto.FechaCreacion == DateOnly.MinValue)
        {
            modulonotiDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            modulonoti.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (modulonoti.FechaModificacion == DateOnly.MinValue)
        {
            modulonotiDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            modulonoti.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.ModuloNotificaciones.Add(modulonoti);
        await _unitOfWork.SaveAsync();
        modulonotiDto.Id = modulonoti.Id;
        return CreatedAtAction(nameof(Post),new {id = modulonoti.Id},modulonotiDto);
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ModuloNotificacionDto>> Put(int id, [FromBody] ModuloNotificacionDto modulonotiDto)
    {
        if(modulonotiDto.Id == 0)
        {
            modulonotiDto.Id = id;
        }
        if(modulonotiDto.Id != id)
        {
            return NotFound();
        }
        var modulonoti = _mapper.Map<ModuloNotificacion>(modulonotiDto);
        if(modulonoti==null)
            return BadRequest();
        if(modulonoti.FechaCreacion == DateOnly.MinValue)
        {
            modulonotiDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            modulonoti.FechaCreacion = DateOnly.FromDateTime(DateTime.Now); 
        }
        if(modulonotiDto.FechaModificacion == DateOnly.MinValue)
        {
            modulonotiDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            modulonoti.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.ModuloNotificaciones.Update(modulonoti);
        await _unitOfWork.SaveAsync();
        return modulonotiDto;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var modulonoti = await _unitOfWork.ModuloNotificaciones.GetByIdAsync(id);
        if(modulonoti == null)
            return NotFound();
        _unitOfWork.ModuloNotificaciones.Remove(modulonoti);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

}
