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
    public PaymentsController(IPaymentServices paymentServices)
    {
        _paymentServices = paymentServices;
    }
    [HttpGet]
    public async Task<IActionResult> GetPayments()
    {
        RequiredScope("payments:*:get");
        return Ok(await _paymentServices.List());
    }
}