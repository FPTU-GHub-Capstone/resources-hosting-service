using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using System.Diagnostics;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.Http.API_VERSION + "/gms/transactions")]
public class TransactionsController : BaseController
{
    private readonly ITransactionServices _transactionServices;
    public TransactionsController(ITransactionServices transactionServices)
    {
        _transactionServices = transactionServices;
    }
    [HttpGet]
    public async Task<IActionResult> GetTransactions()
    {
        RequiredScope("games:*:get", "transactions:*:get");
        return Ok(await _transactionServices.List());
    }
}