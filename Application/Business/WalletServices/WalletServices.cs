using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class WalletServices : IWalletServices
{
    private readonly IGenericRepository<WalletEntity> _walletRepo;
    private readonly IWalletCategoryServices _walletCategoryService;
    private readonly ICharacterServices _characterService;
    public WalletServices(IGenericRepository<WalletEntity> walletRepo, IWalletCategoryServices walletCategoryService
        , ICharacterServices characterService)
    {
        _walletRepo = walletRepo;
        _walletCategoryService = walletCategoryService;
        _characterService = characterService;
    }
    public async Task<ICollection<WalletEntity>> List()
    {
        return await _walletRepo.ListAsync();
    }
    public async Task<WalletEntity> GetById(Guid walletId)
    {
        return await _walletRepo.FoundOrThrowAsync(walletId,
           Constants.Entities.WALLET + Constants.Errors.NOT_EXIST_ERROR);
    }
    public async Task<ICollection<WalletEntity>> ListWalletsByCharacterId(Guid charId)
    {
        return await _walletRepo.WhereAsync(w => w.CharacterId.Equals(charId));
    }

    public async Task<ICollection<WalletEntity>> ListWalletsByGameId(Guid gameId)
    {
        var walletCategoryIds = (await _walletCategoryService.ListWalCatsByGameId(gameId)).Select(x=>x.Id);
        var characterIds = (await _characterService.ListCharByGameId(gameId)).Select(x=>x.Id);
        return await _walletRepo.WhereAsync(w => walletCategoryIds.Contains(w.WalletCategoryId)
                    || characterIds.Contains(w.CharacterId));

    }
    public async Task Create(WalletEntity wallet)
    {
        await CheckDuplicateWallet(wallet);
        await _walletRepo.CreateAsync(wallet);
    }
    public async Task Update(WalletEntity wallet)
    {
        await CheckDuplicateWallet(wallet);
        await _walletRepo.UpdateAsync(wallet);
    }
    public async Task Delete(Guid walletId)
    {
        await _walletRepo.DeleteSoftAsync(walletId);
    }
    public async Task CheckDuplicateWallet(WalletEntity wallet)
    {
        var checkWallet = await _walletRepo.FirstOrDefaultAsync(
            w => w.CharacterId.Equals(wallet.CharacterId) && w.WalletCategoryId.Equals(wallet.WalletCategoryId));
        if (checkWallet is not null && (checkWallet.Id == Guid.Empty || checkWallet.Id != wallet.Id))
        {
            throw new NotFoundException(Constants.Entities.WALLET + Constants.Errors.ALREADY_EXIST_ERROR);
        }
    }
}