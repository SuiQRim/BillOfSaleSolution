using BillSale.API.Models.Responces;
using BillSale.BLL.Services.Contracts.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BillSale.API.Infrastructure;

/// <summary>
/// Фильтр для исключений
/// </summary>
public class ExceptionFilter : IExceptionFilter
{
    /// <inheritdoc />
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception as BillSaleException;
        if (exception == null)
        {
            return;
        }

        switch (exception)
        {
            case NotFoundException ex:
                SetDataToContext(new NotFoundObjectResult(new ApiExceptionDetail
                {
                    Message = ex.Message,
                }), context);
                break;
            case BillSaleValidationException ex:
                SetDataToContext(
                    new BadRequestObjectResult(new ApiValidationExceptionDetail { Errors = ex.Errors, })
                    {
                        StatusCode = StatusCodes.Status422UnprocessableEntity
                    },
                    context);
                break;

            default:
                SetDataToContext(new BadRequestObjectResult(new ApiExceptionDetail
                {
                    Message = exception.Message,
                }), context);
                break;
        }
    }

    /// <summary>
    /// Определяет контекст ответа
    /// </summary>
    private static void SetDataToContext(ObjectResult data, ExceptionContext context)
    {
        context.ExceptionHandled = true;
        var response = context.HttpContext.Response;
        response.StatusCode = data.StatusCode ?? StatusCodes.Status400BadRequest;
        context.Result = data;
    }
}
