using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.Http.API_VERSION + "/gms/payments")]
public class PaymentsController : BaseController
{
    private readonly IPaymentServices _paymentServices;
    private readonly IGenericRepository<PaymentEntity> _paymentRepo;
    private readonly IGenericRepository<CharacterEntity> _characterRepo;
    private readonly IGenericRepository<UserEntity> _userRepo;
    private readonly IGenericRepository<WalletEntity> _walletRepo;
    public PaymentsController(IPaymentServices paymentServices, IGenericRepository<PaymentEntity> paymentRepo
        , IGenericRepository<CharacterEntity> characterRepo, IGenericRepository<UserEntity> userRepo,
          IGenericRepository<WalletEntity> walletRepo)
    {
        _paymentServices = paymentServices;
        _paymentRepo = paymentRepo;
        _characterRepo = characterRepo;
        _userRepo = userRepo;
        _walletRepo = walletRepo;
    }
    [HttpGet]
    public async Task<IActionResult> GetPayments()
    {
        if (CurrentScp.Contains("payments:*:get"))
        {
            return Ok(await _paymentServices.List());
        }
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPayment(Guid id)
    {
        return Ok(await _paymentServices.List());
    }

    [HttpPost]
    public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequest payment)
    {
        await _characterRepo.FoundOrThrowAsync(payment.CharacterId, Constants.Entities.CHARACTER + Constants.Errors.NOT_EXIST_ERROR);
        await _userRepo.FoundOrThrowAsync(payment.UserId, Constants.Entities.USER + Constants.Errors.NOT_EXIST_ERROR);
        await _walletRepo.FoundOrThrowAsync(payment.WalletId, Constants.Entities.WALLET + Constants.Errors.NOT_EXIST_ERROR);
        var newPayment = new PaymentEntity();
        Mapper.Map(payment, newPayment);
        await _paymentServices.Create(newPayment);
        return CreatedAtAction(nameof(GetPayment), new { id = newPayment.Id }, newPayment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePayment(Guid id, [FromBody] UpdatePaymentRequest payment)
    {
        var updatePayment = await _paymentRepo.FoundOrThrowAsync(id, Constants.Entities.PAYMENT + Constants.Errors.NOT_EXIST_ERROR);
        Mapper.Map(payment, updatePayment);
        await _paymentServices.Update(updatePayment);
        return Ok(updatePayment);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePayment(Guid id)
    {
        await _paymentRepo.FoundOrThrowAsync(id, Constants.Entities.PAYMENT + Constants.Errors.NOT_EXIST_ERROR);
        await _paymentServices.Delete(id);
        return NoContent();
    }
}