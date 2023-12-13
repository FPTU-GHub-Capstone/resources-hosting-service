using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
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
        if (!CurrentScp.Contains("payments:*:get"))
        {
            throw new ForbiddenException();
        }
        return Ok(await _paymentServices.List());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPayment(Guid id)
    {
        return Ok(await _paymentServices.List());
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