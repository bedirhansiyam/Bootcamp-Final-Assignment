using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Base;
using WebApi.Operation;
using WebApi.Schema;

namespace WebApi.Service;

[Route("webapi/v1/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService service;

    public PaymentController(IPaymentService service)
    {
        this.service = service;
    }

    [Authorize(Policy = Policy.User)]
    [HttpPost("{orderNumber}")]
    public async Task<ApiResponse<PaymentResponse>> Payment(string orderNumber, [FromBody] PaymentRequest request)
    {
        return await service.GetPayment(request, HttpContext.User, orderNumber);
    }
}
