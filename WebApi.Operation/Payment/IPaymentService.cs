using System.Security.Claims;
using WebApi.Base;
using WebApi.Schema;

namespace WebApi.Operation;

public interface IPaymentService
{
    Task<ApiResponse<PaymentResponse>> GetPayment(PaymentRequest paymentRequest, ClaimsPrincipal user, string orderNumber);
}
