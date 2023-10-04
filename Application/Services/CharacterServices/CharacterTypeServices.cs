using Application.Interfaces;
using Domain.Entities;
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
        return await _characterTypeRepo.ListAsync();
    }
    public async Task<CharacterType> GetById(Guid characterTypeId)
    {
        return await _characterTypeRepo.FindByIdAsync(characterTypeId);
    }
    public async Task<ICollection<CharacterType>> GetByGameId(Guid gameId)
    {
        return await _characterTypeRepo.WhereAsync(c => c.GameId.Equals(gameId));
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
