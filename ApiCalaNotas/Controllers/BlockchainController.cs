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
public class BlockchainController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BlockchainController(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<BlockchainDto>>> Get()
    {
        var blockchain = await _unitOfWork.Blockchains.GetAllAsync();
        return _mapper.Map<List<BlockchainDto>>(blockchain);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BlockchainDto>> Get(int id)
    {
        var blockchain = await _unitOfWork.Blockchains.GetByIdAsync(id);
        if(blockchain == null)
        {
            return NotFound();
        }
        return _mapper.Map<BlockchainDto>(blockchain);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BlockchainDto>> Post([FromBody] BlockchainDto blockchainDto)
    {
        var blockchain = _mapper.Map<Blockchain>(blockchainDto);

        if(blockchain == null)
            return BadRequest();
        if (blockchainDto.FechaCreacion == DateOnly.MinValue)
        {
            blockchainDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            blockchain.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (blockchain.FechaModificacion == DateOnly.MinValue)
        {
            blockchainDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            blockchain.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.Blockchains.Add(blockchain);
        await _unitOfWork.SaveAsync();
        blockchainDto.Id = blockchain.Id;
        return CreatedAtAction(nameof(Post),new {id = blockchain.Id},blockchainDto);
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BlockchainDto>> Put(int id, [FromBody] BlockchainDto blockchainDto)
    {
        if(blockchainDto.Id == 0)
        {
            blockchainDto.Id = id;
        }
        if(blockchainDto.Id != id)
        {
            return NotFound();
        }
        var blockchain = _mapper.Map<Blockchain>(blockchainDto);
        if(blockchain==null)
            return BadRequest();
        if(blockchain.FechaCreacion == DateOnly.MinValue)
        {
            blockchainDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            blockchain.FechaCreacion = DateOnly.FromDateTime(DateTime.Now); 
        }
        if(blockchainDto.FechaModificacion == DateOnly.MinValue)
        {
            blockchainDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            blockchain.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.Blockchains.Update(blockchain);
        await _unitOfWork.SaveAsync();
        return blockchainDto;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var blockchain = await _unitOfWork.Blockchains.GetByIdAsync(id);
        if(blockchain == null)
            return NotFound();
        _unitOfWork.Blockchains.Remove(blockchain);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

}
