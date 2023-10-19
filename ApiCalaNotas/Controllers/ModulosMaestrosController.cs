using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class ModulosMaestrosController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ModulosMaestrosController(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ModulosMaestroDto>>> Get()
    {
        var modulosmaestro = await _unitOfWork.ModulosMaestros.GetAllAsync();
        return _mapper.Map<List<ModulosMaestroDto>>(modulosmaestro);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ModulosMaestroDto>> Get(int id)
    {
        var modulosmaestro = await _unitOfWork.ModulosMaestros.GetByIdAsync(id);
        if(modulosmaestro == null)
        {
            return NotFound();
        }
        return _mapper.Map<ModulosMaestroDto>(modulosmaestro);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ModulosMaestroDto>> Post([FromBody] ModulosMaestroDto modulosmaestroDto)
    {
        var modulosmaestro = _mapper.Map<ModulosMaestro>(modulosmaestroDto);

        if(modulosmaestro == null)
            return BadRequest();
        if (modulosmaestroDto.FechaCreacion == DateOnly.MinValue)
        {
            modulosmaestroDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            modulosmaestro.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (modulosmaestro.FechaModificacion == DateOnly.MinValue)
        {
            modulosmaestroDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            modulosmaestro.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.ModulosMaestros.Add(modulosmaestro);
        await _unitOfWork.SaveAsync();
        modulosmaestroDto.Id = modulosmaestro.Id;
        return CreatedAtAction(nameof(Post),new {id = modulosmaestro.Id},modulosmaestroDto);
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ModulosMaestroDto>> Put(int id, [FromBody] ModulosMaestroDto modulosmaestroDto)
    {
        if(modulosmaestroDto.Id == 0)
        {
            modulosmaestroDto.Id = id;
        }
        if(modulosmaestroDto.Id != id)
        {
            return NotFound();
        }
        var modulosmaestro = _mapper.Map<ModulosMaestro>(modulosmaestroDto);
        if(modulosmaestro==null)
            return BadRequest();
        if(modulosmaestro.FechaCreacion == DateOnly.MinValue)
        {
            modulosmaestroDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            modulosmaestro.FechaCreacion = DateOnly.FromDateTime(DateTime.Now); 
        }
        if(modulosmaestroDto.FechaModificacion == DateOnly.MinValue)
        {
            modulosmaestroDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            modulosmaestro.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.ModulosMaestros.Update(modulosmaestro);
        await _unitOfWork.SaveAsync();
        return modulosmaestroDto;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var modulosmaestro = await _unitOfWork.ModulosMaestros.GetByIdAsync(id);
        if(modulosmaestro == null)
            return NotFound();
        _unitOfWork.ModulosMaestros.Remove(modulosmaestro);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

}
