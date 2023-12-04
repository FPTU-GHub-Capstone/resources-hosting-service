using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class WalletCategoryServices : IWalletCategoryServices
{
    public readonly IGenericRepository<WalletCategoryEntity> _walletCategoryRepo;
    public WalletCategoryServices(IGenericRepository<WalletCategoryEntity> walletCategoryRepo)
    {
        _walletCategoryRepo = walletCategoryRepo;
    }
    public async Task<ICollection<WalletCategoryEntity>> List()
    {
        return await _walletCategoryRepo.ListAsync();
    }
    public async Task<WalletCategoryEntity> GetById(Guid categoryId)
    {
        return await _walletCategoryRepo.FoundOrThrowAsync(categoryId,
           Constants.Entities.WALLET_CATEGORY + Constants.Errors.NOT_EXIST_ERROR);
    }
    public async Task<ICollection<WalletCategoryEntity>> ListWalCatsByGameId(Guid gameId)
    {
        return await _walletCategoryRepo.WhereAsync(wc => wc.GameId.Equals(gameId));
    }
    public async Task Create(WalletCategoryEntity walletCategory)
    {
        await CheckForDuplicateWalletCategory(walletCategory);
        await _walletCategoryRepo.CreateAsync(walletCategory);
    }
    public async Task Update(WalletCategoryEntity walletCategory)
    {
        await CheckForDuplicateWalletCategory(walletCategory);
        await _walletCategoryRepo.UpdateAsync(walletCategory);
    }
    public async Task Delete(Guid categoryId)
    {
        await _walletCategoryRepo.DeleteSoftAsync(categoryId);
    }

    public async Task CheckForDuplicateWalletCategory(WalletCategoryEntity walletCategory)
    {
        var checkWalCat = await _walletCategoryRepo.FirstOrDefaultAsync(l => l.Name == walletCategory.Name && l.GameId == walletCategory.GameId);
        if (checkWalCat is not null)
        {
            if (checkWalCat.Id == Guid.Empty || checkWalCat.Id != walletCategory.Id)
            {
                throw new BadRequestException(Constants.Entities.WALLET_CATEGORY + Constants.Errors.ALREADY_EXIST_ERROR);
            }
        }
    }
}