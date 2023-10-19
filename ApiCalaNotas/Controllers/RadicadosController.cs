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
public class RadicadosController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RadicadosController(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<RadicadosDto>>> Get()
    {
        var radicado = await _unitOfWork.Radicados.GetAllAsync();
        return _mapper.Map<List<RadicadosDto>>(radicado);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RadicadosDto>> Get(int id)
    {
        var radicado = await _unitOfWork.Radicados.GetByIdAsync(id);
        if(radicado == null)
        {
            return NotFound();
        }
        return _mapper.Map<RadicadosDto>(radicado);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RadicadosDto>> Post([FromBody] RadicadosDto radicadoDto)
    {
        var radicado = _mapper.Map<Radicados>(radicadoDto);

        if(radicado == null)
            return BadRequest();
        if (radicadoDto.FechaCreacion == DateOnly.MinValue)
        {
            radicadoDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            radicado.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (radicado.FechaModificacion == DateOnly.MinValue)
        {
            radicadoDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            radicado.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.Radicados.Add(radicado);
        await _unitOfWork.SaveAsync();
        radicadoDto.Id = radicado.Id;
        return CreatedAtAction(nameof(Post),new {id = radicado.Id},radicadoDto);
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RadicadosDto>> Put(int id, [FromBody] RadicadosDto radicadoDto)
    {
        if(radicadoDto.Id == 0)
        {
            radicadoDto.Id = id;
        }
        if(radicadoDto.Id != id)
        {
            return NotFound();
        }
        var radicado = _mapper.Map<Radicados>(radicadoDto);
        if(radicado==null)
            return BadRequest();
        if(radicado.FechaCreacion == DateOnly.MinValue)
        {
            radicadoDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            radicado.FechaCreacion = DateOnly.FromDateTime(DateTime.Now); 
        }
        if(radicadoDto.FechaModificacion == DateOnly.MinValue)
        {
            radicadoDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            radicado.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.Radicados.Update(radicado);
        await _unitOfWork.SaveAsync();
        return radicadoDto;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var radicado = await _unitOfWork.Radicados.GetByIdAsync(id);
        if(radicado == null)
            return NotFound();
        _unitOfWork.Radicados.Remove(radicado);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

}
