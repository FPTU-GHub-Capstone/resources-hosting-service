﻿using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.HTTP.API_VERSION + "/gms/character-types")]
public class CharacterTypesController : BaseController
{
    private readonly ICharacterTypeServices _characterTypeServices;
    private readonly IGenericRepository<CharacterTypeEntity> _characterTypeRepo;
    private readonly IGenericRepository<GameEntity> _gameRepo;
    public CharacterTypesController(ICharacterTypeServices characterTypeServices, IGenericRepository<CharacterTypeEntity> characterTypeRepo
, IGenericRepository<GameEntity> gameRepo)
    {
        _characterTypeServices = characterTypeServices;
        _characterTypeRepo = characterTypeRepo;
        _gameRepo = gameRepo;
    }
    [HttpGet]
    public async Task<IActionResult> GetCharacterTypes()
    {
        var ctList = await _characterTypeServices.List();
        return Ok(ctList);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCharacterType(Guid id)
    {
        var ct = await _characterTypeServices.GetById(id);
        return Ok(ct);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCharacterType([FromBody] CreateCharacterTypeRequest charType)
    {
        await _gameRepo.FoundOrThrowAsync(charType.GameId, Constants.ENTITY.GAME + Constants.ERROR.NOT_EXIST_ERROR);
        var newCT = new CharacterTypeEntity();
        Mapper.Map(charType, newCT);
        await _characterTypeServices.Create(newCT);
        return CreatedAtAction(nameof(GetCharacterType), new {id = newCT.Id}, newCT);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCharacterType(Guid id, [FromBody] UpdateCharacterTypeRequest charType)
    {
        var ct = await _characterTypeRepo.FoundOrThrowAsync(id, Constants.ENTITY.CHARACTER_TYPE + Constants.ERROR.NOT_EXIST_ERROR);
        Mapper.Map(charType,ct);
        await _characterTypeServices.Update(ct);
        return Ok(ct);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCharacterType(Guid id)
    {
        await _characterTypeRepo.FoundOrThrowAsync(id, Constants.ENTITY.CHARACTER_TYPE + Constants.ERROR.NOT_EXIST_ERROR);
        await _characterTypeServices.Delete(id);
        return NoContent();
    }
}
