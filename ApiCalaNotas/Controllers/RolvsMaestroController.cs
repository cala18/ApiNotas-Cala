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
public class RolvsMaestroController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RolvsMaestroController(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<RolvsMaestroDto>>> Get()
    {
        var rolvsmaestro = await _unitOfWork.RolvsMaestros.GetAllAsync();
        return _mapper.Map<List<RolvsMaestroDto>>(rolvsmaestro);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RolvsMaestroDto>> Get(int id)
    {
        var rolvsmaestro = await _unitOfWork.RolvsMaestros.GetByIdAsync(id);
        if(rolvsmaestro == null)
        {
            return NotFound();
        }
        return _mapper.Map<RolvsMaestroDto>(rolvsmaestro);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RolvsMaestroDto>> Post([FromBody] RolvsMaestroDto rolvsmaestroDto)
    {
        var rolvsmaestro = _mapper.Map<RolvsMaestro>(rolvsmaestroDto);

        if(rolvsmaestro == null)
            return BadRequest();
        if (rolvsmaestroDto.FechaCreacion == DateOnly.MinValue)
        {
            rolvsmaestroDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            rolvsmaestro.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (rolvsmaestro.FechaModificacion == DateOnly.MinValue)
        {
            rolvsmaestroDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            rolvsmaestro.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.RolvsMaestros.Add(rolvsmaestro);
        await _unitOfWork.SaveAsync();
        rolvsmaestroDto.Id = rolvsmaestro.Id;
        return CreatedAtAction(nameof(Post),new {id = rolvsmaestro.Id},rolvsmaestroDto);
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RolvsMaestroDto>> Put(int id, [FromBody] RolvsMaestroDto rolvsmaestroDto)
    {
        if(rolvsmaestroDto.Id == 0)
        {
            rolvsmaestroDto.Id = id;
        }
        if(rolvsmaestroDto.Id != id)
        {
            return NotFound();
        }
        var rolvsmaestro = _mapper.Map<RolvsMaestro>(rolvsmaestroDto);
        if(rolvsmaestro==null)
            return BadRequest();
        if(rolvsmaestro.FechaCreacion == DateOnly.MinValue)
        {
            rolvsmaestroDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            rolvsmaestro.FechaCreacion = DateOnly.FromDateTime(DateTime.Now); 
        }
        if(rolvsmaestroDto.FechaModificacion == DateOnly.MinValue)
        {
            rolvsmaestroDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            rolvsmaestro.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.RolvsMaestros.Update(rolvsmaestro);
        await _unitOfWork.SaveAsync();
        return rolvsmaestroDto;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var rolvsmaestro = await _unitOfWork.RolvsMaestros.GetByIdAsync(id);
        if(rolvsmaestro == null)
            return NotFound();
        _unitOfWork.RolvsMaestros.Remove(rolvsmaestro);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

}
