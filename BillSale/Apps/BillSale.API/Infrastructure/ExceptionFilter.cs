using BillSale.API.Models.Responces;
using BillSale.BLL.Services.Contracts.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BillSale.API.Infrastructure;

/// <summary>
/// Фильтр для исключений
/// </summary>
public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> logger;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="logger">логгер</param>
    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        this.logger = logger;
    }

    /// <inheritdoc />
    public void OnException(ExceptionContext context)
    {

        if (context.Exception is OperationCanceledException
            && context.HttpContext.RequestAborted.IsCancellationRequested)
        {
            return;
        }

        switch (context.Exception)
        {
            case NotFoundException ex:
                SetDataToContext(new NotFoundObjectResult(new ApiExceptionDetail
                {
                    Message = ex.Message,
                }), context);
                break;

            case BillSaleValidationException ex:
                SetDataToContext(
                    new ObjectResult(new ApiValidationExceptionDetail
                    {
                        Errors = ex.Errors
                    })
                    {
                        StatusCode = StatusCodes.Status422UnprocessableEntity
                    },
                    context);
                break;

            default:
                logger.LogError(
                    context.Exception,
                    "Ошибка обработки запроса {Method} {Path}. TraceId: {TraceId}",
                    context.HttpContext.Request.Method,
                    context.HttpContext.Request.Path,
                    context.HttpContext.TraceIdentifier);

                SetDataToContext(
                    new ObjectResult(new ApiExceptionDetail
                    {
                        Message = "Произошла внутренняя ошибка сервера."
                    })
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    },
                    context);
                break;
        }
    }

    /// <summary>
    /// Определяет контекст ответа
    /// </summary>
    private static void SetDataToContext(ObjectResult result, ExceptionContext context)
    {
        context.Result = result;
        context.ExceptionHandled = true;
    }
}
