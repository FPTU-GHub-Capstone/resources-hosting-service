using AutoWrapper.Extensions;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiLayer.Filters
{
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                throw new BadRequestException(context.ModelState.AllErrors());
            }
        }
    }
}
