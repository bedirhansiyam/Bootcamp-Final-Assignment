using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebApi.Base;
using WebApi.Data;
using WebApi.Data.Uow;
using WebApi.Schema;

namespace WebApi.Operation;

public class PaymentService : IPaymentService
{
    private readonly UserManager<User> userManager;
    private readonly IUnitOfWork unitOfWork;

    public PaymentService(UserManager<User> userManager, IUnitOfWork unitOfWork)
    {
        this.userManager = userManager;
        this.unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse<PaymentResponse>> GetPayment(PaymentRequest paymentRequest, ClaimsPrincipal user, string orderNumber)
    {
        try
        {
            // An external payment service can be integrated here
            bool isPaymentSuccess = true;
            if (isPaymentSuccess)
            {
                var authUser = await userManager.GetUserAsync(user);
                var userName = authUser.UserName;
                var order = unitOfWork.ReadRepository<Order>().Where(x => x.OrderNumber == orderNumber).FirstOrDefault();

                if (userName != order.UserName)
                    return new ApiResponse<PaymentResponse>($"Order {order.OrderNumber} does not belong to you.");

                if(order.Status == OrderStatus.Completed)
                    return new ApiResponse<PaymentResponse>("Payment already has been completed for this order");

                var referenceNumber = CodeGenerator.ReferenceNumber();
                order.Status = OrderStatus.Completed;
                order.PaymentReferenceNumber = referenceNumber;
                authUser.LoyaltyPoints += (decimal)order.LoyaltyPointsToBeEarned;

                unitOfWork.WriteRepository<Order>().Update(order);
                unitOfWork.Complete();
                await userManager.UpdateAsync(authUser);

                PaymentResponse response = new()
                {
                    Message = "Thank you. Your payment is successful.",
                    ReferenceNumber = referenceNumber,
                    OrderNumber = orderNumber,
                    Amount = order.Payment
                };

                return new ApiResponse<PaymentResponse>(response);
            }
            return new ApiResponse<PaymentResponse>("Payment failed, please try again later");
        }
        catch (Exception ex)
        {
            return new ApiResponse<PaymentResponse>(ex.Message);
        }
    }
}
