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
public class SubmodulosController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SubmodulosController(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<SubmodulosDto>>> Get()
    {
        var submodulos = await _unitOfWork.SubModulos.GetAllAsync();
        return _mapper.Map<List<SubmodulosDto>>(submodulos);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SubmodulosDto>> Get(int id)
    {
        var submodulo = await _unitOfWork.SubModulos.GetByIdAsync(id);
        if(submodulo == null)
        {
            return NotFound();
        }
        return _mapper.Map<SubmodulosDto>(submodulo);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SubmodulosDto>> Post([FromBody] SubmodulosDto submoduloDto)
    {
        var submodulo = _mapper.Map<SubModulos>(submoduloDto);

        if(submodulo == null)
            return BadRequest();
        if (submoduloDto.FechaCreacion == DateOnly.MinValue)
        {
            submoduloDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            submodulo.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (submodulo.FechaModificacion == DateOnly.MinValue)
        {
            submoduloDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            submodulo.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.SubModulos.Add(submodulo);
        await _unitOfWork.SaveAsync();
        submoduloDto.Id = submodulo.Id;
        return CreatedAtAction(nameof(Post),new {id = submodulo.Id},submoduloDto);
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SubmodulosDto>> Put(int id, [FromBody] SubmodulosDto submoduloDto)
    {
        if(submoduloDto.Id == 0)
        {
            submoduloDto.Id = id;
        }
        if(submoduloDto.Id != id)
        {
            return NotFound();
        }
        var submodulo = _mapper.Map<SubModulos>(submoduloDto);
        if(submodulo==null)
            return BadRequest();
        if(submodulo.FechaCreacion == DateOnly.MinValue)
        {
            submoduloDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            submodulo.FechaCreacion = DateOnly.FromDateTime(DateTime.Now); 
        }
        if(submoduloDto.FechaModificacion == DateOnly.MinValue)
        {
            submoduloDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            submodulo.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.SubModulos.Update(submodulo);
        await _unitOfWork.SaveAsync();
        return submoduloDto;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var submodulo = await _unitOfWork.SubModulos.GetByIdAsync(id);
        if(submodulo == null)
            return NotFound();
        _unitOfWork.SubModulos.Remove(submodulo);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

}
