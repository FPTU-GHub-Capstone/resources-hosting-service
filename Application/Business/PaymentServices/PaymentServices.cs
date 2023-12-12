using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class PaymentServices : IPaymentServices
{
    private readonly IGenericRepository<PaymentEntity> _paymentRepo;
    private readonly IWalletServices _walletServices;
    private readonly IGameUserServices _gameUserServices;
    private readonly ICharacterServices _characterServices;

    public PaymentServices(IGenericRepository<PaymentEntity> paymentRepo, IWalletServices walletServices
        , IGameUserServices gameUserServices, ICharacterServices characterServices)
    {
        _paymentRepo = paymentRepo;
        _walletServices = walletServices;
        _gameUserServices = gameUserServices;
        _characterServices = characterServices;
    }
    public async Task<ICollection<PaymentEntity>> List()
    {
        return await _paymentRepo.ListAsync();
    }
    public async Task<ICollection<PaymentEntity>> ListPaymentByUserId(Guid id)
    {
        return await _paymentRepo.WhereAsync(c => c.UserId == id);
    }
    public async Task<ICollection<PaymentEntity>> ListPaymentByGameId(Guid id)
    {
        var walletIds = (await _walletServices.ListWalletsByGameId(id)).Select(x=>x.Id);
        var userIds = (await _gameUserServices.ListUsersByGameId(id)).Select(x => x.Id);
        var characterIds = (await _characterServices.ListCharByGameId(id)).Select(x => x.Id);
        return await _paymentRepo.WhereAsync(x=>walletIds.Contains(x.WalletId)
            || userIds.Contains(x.UserId)
            || characterIds.Contains(x.CharacterId));
    }
    public async Task Create(PaymentEntity entity) {
        var gameCheck = await _paymentRepo.FirstOrDefaultAsync(
            g => g.WalletId.Equals(entity.WalletId));
        if (gameCheck != null)
        {
            throw new BadRequestException("Wallet already exist in another payment");
        }
        await _paymentRepo.CreateAsync(entity);
    }
    public async Task Update(PaymentEntity entity) {
        await _paymentRepo.UpdateAsync(entity);
    }
    public async Task Delete(Guid paymentId) {
        await _paymentRepo.DeleteSoftAsync(paymentId);
    }
}
