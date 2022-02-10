using Misa.Amis.Core.Entities;
using Misa.Amis.Core.Exceptions;

namespace Misa.Amis.Api.Middlewares
{
  /// <summary>
  /// MIddleware xu ly exception
  /// </summary>
  public class ExceptionHandlerMiddleware
  {
    readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
      _next = next;
    }
    /// <summary>
    /// Xu ly cac exception duoc throw tu cac controller method
    /// </summary>
    /// <param name="http"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext http)
    {
      try
      {
        await _next(http);
      }
      catch (ValidationException validationExc)//Loi validate du lieu
      {
        http.Response.StatusCode = 400;
        await http.Response.WriteAsJsonAsync(new ApiError(validationExc));
      }
      catch (ResourceNotFoundException notFoundExc)//Loi khong tim thay thuc the
      {
        http.Response.StatusCode = 404;
        await http.Response.WriteAsJsonAsync(new ApiError(notFoundExc));
      }
      catch (Exception exc)//Cac loi khac
      {
        http.Response.StatusCode = 500;
        await http.Response.WriteAsJsonAsync(new ApiError("Có lỗi xảy ra. Vui lòng liên hệ Misa để được hỗ trợ", exc));
      }
    }
  }
}
