using Application.Interfaces;
using Domain.Entities.Character;
using System;
using System.Collections.ObjectModel;

namespace Application.Services.CharacterServices;

public class CharacterTypeServices : ICharacterTypeServices
{
    public readonly IGenericRepository<CharacterType> _characterTypeRepo;

    public CharacterTypeServices(IGenericRepository<CharacterType> characterTypeRepo)
    {
        _characterTypeRepo = characterTypeRepo;
    }
    public async Task<ICollection<CharacterType>> List()
    {
        var chaTy = await _characterTypeRepo.ListAsync();
        return chaTy;
    }
    public async Task<CharacterType> GetById(Guid characterTypeId)
    {
        var chaTy = await _characterTypeRepo.FindByIdAsync(characterTypeId);
        if (chaTy == null)
        {
            throw new Exception($"Asset attribute not exist");
        }
        else
        {
            return chaTy;
        }
    }
    public async Task<ICollection<CharacterType>> GetByGameId(Guid gameId)
    {
        ICollection<CharacterType> cha = await _characterTypeRepo.WhereAsync(
            c => c.GameId.Equals(gameId));
        if (cha.Count == 0)
        {
            throw new Exception($"Character Type or Game not found");
        }
        else
        {
            return cha;
        }
    }
    public async Task<int> Count()
    {
        return await _characterTypeRepo.CountAsync();
    }
    public async Task Create(CharacterType characterType)
    {
        
    }
    public async Task Update(Guid characterTypeId, CharacterType characterType)
    {
        
    }
    public async Task Delete(Guid characterTypeId)
    {
        
    }


}
