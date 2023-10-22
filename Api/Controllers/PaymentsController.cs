using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.HTTP.API_VERSION + "/gms/payments")]
public class PaymentsController : BaseController
{
    private readonly IPaymentServices _paymentServices;
    private readonly IGenericRepository<PaymentEntity> _paymentRepo;
    public PaymentsController(IPaymentServices paymentServices, IGenericRepository<PaymentEntity> paymentRepo)
    {
        _paymentServices = paymentServices;
        _paymentRepo = paymentRepo;
    }
    [HttpGet]
    public async Task<IActionResult> GetPayments()
    {
        return Ok(await _paymentServices.List());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPayment(Guid id)
    {
        return Ok(await _paymentServices.List());
    }

    [HttpPost]
    public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequest payment)
    {
        var newPayment = new PaymentEntity();
        Mapper.Map(payment, newPayment);
        await _paymentServices.Create(newPayment);
        return CreatedAtAction(nameof(GetPayment),new {id = newPayment.Id}, newPayment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePayment(Guid id, [FromBody] UpdatePaymentRequest payment)
    {
        var updatePayment = await _paymentRepo.FoundOrThrowAsync(id,"Payment not exist");
        Mapper.Map(payment, updatePayment);
        await _paymentServices.Update(updatePayment);
        return Ok(updatePayment);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePayment(Guid id)
    {
        await _paymentRepo.FoundOrThrowAsync(id, "Payment not exist");
        await _paymentServices.Delete(id);
        return NoContent();
    }
}
