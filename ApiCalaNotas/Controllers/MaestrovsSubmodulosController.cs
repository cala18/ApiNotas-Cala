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
public class MaestrovsSubmodulosController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MaestrovsSubmodulosController(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MaestrosvsSubmodulosDto>>> Get()
    {
        var maestrovssubmodulos = await _unitOfWork.MaestrovsSubmodulos.GetAllAsync();
        return _mapper.Map<List<MaestrosvsSubmodulosDto>>(maestrovssubmodulos);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MaestrosvsSubmodulosDto>> Get(int id)
    {
        var maestrovssub = await _unitOfWork.MaestrovsSubmodulos.GetByIdAsync(id);
        if(maestrovssub == null)
        {
            return NotFound();
        }
        return _mapper.Map<MaestrosvsSubmodulosDto>(maestrovssub);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MaestrosvsSubmodulosDto>> Post([FromBody] MaestrosvsSubmodulosDto maestrovssubDto)
    {
        var maestrovssub = _mapper.Map<MaestrosvsSubmodulos>(maestrovssubDto);

        if(maestrovssub == null)
            return BadRequest();
        if (maestrovssubDto.FechaCreacion == DateOnly.MinValue)
        {
            maestrovssubDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            maestrovssub.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (maestrovssub.FechaModificacion == DateOnly.MinValue)
        {
            maestrovssubDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            maestrovssub.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.MaestrovsSubmodulos.Add(maestrovssub);
        await _unitOfWork.SaveAsync();
        maestrovssubDto.Id = maestrovssub.Id;
        return CreatedAtAction(nameof(Post),new {id = maestrovssub.Id},maestrovssubDto);
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MaestrosvsSubmodulosDto>> Put(int id, [FromBody] MaestrosvsSubmodulosDto maestrovssubDto)
    {
        if(maestrovssubDto.Id == 0)
        {
            maestrovssubDto.Id = id;
        }
        if(maestrovssubDto.Id != id)
        {
            return NotFound();
        }
        var maestrovssub = _mapper.Map<MaestrosvsSubmodulos>(maestrovssubDto);
        if(maestrovssub==null)
            return BadRequest();
        if(maestrovssub.FechaCreacion == DateOnly.MinValue)
        {
            maestrovssubDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            maestrovssub.FechaCreacion = DateOnly.FromDateTime(DateTime.Now); 
        }
        if(maestrovssubDto.FechaModificacion == DateOnly.MinValue)
        {
            maestrovssubDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            maestrovssub.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.MaestrovsSubmodulos.Update(maestrovssub);
        await _unitOfWork.SaveAsync();
        return maestrovssubDto;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var maestrovssub = await _unitOfWork.MaestrovsSubmodulos.GetByIdAsync(id);
        if(maestrovssub == null)
            return NotFound();
        _unitOfWork.MaestrovsSubmodulos.Remove(maestrovssub);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

}
