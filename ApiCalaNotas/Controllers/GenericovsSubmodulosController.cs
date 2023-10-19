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
public class GenericovsSubmodulosController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GenericovsSubmodulosController(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<GenericovsSubmodulosDto>>> Get()
    {
        var genericovsmodulos = await _unitOfWork.GenericovsSubmodulos.GetAllAsync();
        return _mapper.Map<List<GenericovsSubmodulosDto>>(genericovsmodulos);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GenericovsSubmodulosDto>> Get(int id)
    {
        var genericovsmodulos = await _unitOfWork.GenericovsSubmodulos.GetByIdAsync(id);
        if(genericovsmodulos == null)
        {
            return NotFound();
        }
        return _mapper.Map<GenericovsSubmodulosDto>(genericovsmodulos);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GenericovsSubmodulosDto>> Post([FromBody] GenericovsSubmodulosDto genericovsSubmodulosDto)
    {
        var genericovsmodulos = _mapper.Map<GenericovsSubmodulos>(genericovsSubmodulosDto);

        if(genericovsmodulos == null)
            return BadRequest();
        if (genericovsSubmodulosDto.FechaCreacion == DateOnly.MinValue)
        {
            genericovsSubmodulosDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            genericovsmodulos.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (genericovsmodulos.FechaModificacion == DateOnly.MinValue)
        {
            genericovsSubmodulosDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            genericovsmodulos.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.GenericovsSubmodulos.Add(genericovsmodulos);
        await _unitOfWork.SaveAsync();
        genericovsSubmodulosDto.Id = genericovsmodulos.Id;
        return CreatedAtAction(nameof(Post),new {id = genericovsmodulos.Id},genericovsSubmodulosDto);
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GenericovsSubmodulosDto>> Put(int id, [FromBody] GenericovsSubmodulosDto genericovsSubmodulosDto)
    {
        if(genericovsSubmodulosDto.Id == 0)
        {
            genericovsSubmodulosDto.Id = id;
        }
        if(genericovsSubmodulosDto.Id != id)
        {
            return NotFound();
        }
        var genericovssubmodulos = _mapper.Map<GenericovsSubmodulos>(genericovsSubmodulosDto);
        if(genericovssubmodulos==null)
            return BadRequest();
        if(genericovssubmodulos.FechaCreacion == DateOnly.MinValue)
        {
            genericovsSubmodulosDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            genericovssubmodulos.FechaCreacion = DateOnly.FromDateTime(DateTime.Now); 
        }
        if(genericovsSubmodulosDto.FechaModificacion == DateOnly.MinValue)
        {
            genericovsSubmodulosDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            genericovssubmodulos.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.GenericovsSubmodulos.Update(genericovssubmodulos);
        await _unitOfWork.SaveAsync();
        return genericovsSubmodulosDto;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var genericovssubmodulos = await _unitOfWork.GenericovsSubmodulos.GetByIdAsync(id);
        if(genericovssubmodulos == null)
            return NotFound();
        _unitOfWork.GenericovsSubmodulos.Remove(genericovssubmodulos);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

}
