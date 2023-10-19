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
public class FormatoController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FormatoController(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<FormatoDto>>> Get()
    {
        var formato = await _unitOfWork.Formatos.GetAllAsync();
        return _mapper.Map<List<FormatoDto>>(formato);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FormatoDto>> Get(int id)
    {
        var formato = await _unitOfWork.Formatos.GetByIdAsync(id);
        if(formato == null)
        {
            return NotFound();
        }
        return _mapper.Map<FormatoDto>(formato);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FormatoDto>> Post([FromBody] FormatoDto formatoDto)
    {
        var formato = _mapper.Map<Formato>(formatoDto);

        if(formato == null)
            return BadRequest();
        if (formatoDto.FechaCreacion == DateOnly.MinValue)
        {
            formatoDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            formato.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (formato.FechaModificacion == DateOnly.MinValue)
        {
            formatoDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            formato.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.Formatos.Add(formato);
        await _unitOfWork.SaveAsync();
        formatoDto.Id = formato.Id;
        return CreatedAtAction(nameof(Post),new {id = formato.Id},formatoDto);
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FormatoDto>> Put(int id, [FromBody] FormatoDto formatoDto)
    {
        if(formatoDto.Id == 0)
        {
            formatoDto.Id = id;
        }
        if(formatoDto.Id != id)
        {
            return NotFound();
        }
        var formato = _mapper.Map<Formato>(formatoDto);
        if(formato==null)
            return BadRequest();
        if(formato.FechaCreacion == DateOnly.MinValue)
        {
            formatoDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            formato.FechaCreacion = DateOnly.FromDateTime(DateTime.Now); 
        }
        if(formatoDto.FechaModificacion == DateOnly.MinValue)
        {
            formatoDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            formato.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.Formatos.Update(formato);
        await _unitOfWork.SaveAsync();
        return formatoDto;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var formato = await _unitOfWork.Formatos.GetByIdAsync(id);
        if(formato == null)
            return NotFound();
        _unitOfWork.Formatos.Remove(formato);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

}
