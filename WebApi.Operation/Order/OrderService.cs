using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebApi.Base;
using WebApi.Data;
using WebApi.Data.Uow;
using WebApi.Schema;

namespace WebApi.Operation;

public class OrderService : BaseService<Order, OrderRequest, OrderResponse>, IOrderService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly UserManager<User> userManager;
    private readonly IMapper mapper;
    public OrderService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager) : base(unitOfWork, mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.userManager = userManager;
    }

    public async Task<ApiResponse<List<OrderResponse>>> MyOrders(ClaimsPrincipal user)
    {
        if (user is null)
            return new ApiResponse<List<OrderResponse>>("Please sign in first");

        try
        {
            var authUser = await userManager.GetUserAsync(user);
            var userName = authUser.UserName;
            var orderList = unitOfWork.ReadRepository<Order>()
                .Where(x => x.UserName == userName).ToList();
            if (orderList is null)
                return new ApiResponse<List<OrderResponse>>("You have not placed any order");

            var mapped = mapper.Map<List<Order>, List<OrderResponse>>(orderList);
            return new ApiResponse<List<OrderResponse>>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<OrderResponse>>(ex.Message);
        }

    }
    public ApiResponse<List<OrderResponse>> GetByUserName(string userName)
    {
        try
        {
            var orderList = unitOfWork.ReadRepository<Order>()
                .Where(x => x.UserName == userName).ToList();
            if (orderList is null)
                return new ApiResponse<List<OrderResponse>>("User has not placed any order");

            var mapped = mapper.Map<List<Order>, List<OrderResponse>>(orderList);
            return new ApiResponse<List<OrderResponse>>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<OrderResponse>>(ex.Message);
        }
    }
    public async Task<ApiResponse> Create(ClaimsPrincipal user, OrderRequest orderRequest)
    {
        decimal totalAmount = 0;
        if (user is null)
            return new ApiResponse("Please sign in");
        try
        {
            Order order = new();
            List<OrderDetail> orderDetailList = new();

            var authUser = await userManager.GetUserAsync(user);
            var userName = authUser.UserName;
            var userId = authUser.Id;

            order.OrderedDate = DateTime.UtcNow;
            order.Address = orderRequest.Address;
            order.UserName = userName;
            order.OrderNumber = CodeGenerator.OrderNumber();
            order.Status = OrderStatus.Pending;

            var basket = unitOfWork.ReadRepository<Basket>()
                .WhereWithInclude(x => x.UserId == userId, new string[] { IncludeType.BasketToUser, IncludeType.BasketToProduct }).ToList();

            if (basket is null)
                return new ApiResponse("Your basket is empty. Please add products to your basket to buy.");

            foreach (var product in basket)
            {
                OrderDetail orderDetails = new();
                totalAmount += product.Quantity * product.Product.Price;

                orderDetails.Quantity = product.Quantity;
                orderDetails.OrderNumber = order.OrderNumber;
                orderDetails.ProductId = product.ProductId;
                orderDetails.UserName = userName;
                orderDetailList.Add(orderDetails);
            }

            order.TotalAmount = totalAmount;

            var coupon = unitOfWork.ReadRepository<Coupon>()
                    .WhereWithInclude(x => x.Code == orderRequest.CouponCode, IncludeType.CouponToUser).FirstOrDefault();
            if (coupon is null && !string.IsNullOrWhiteSpace(orderRequest.CouponCode))
            {
                return new ApiResponse("Coupon not found. Please enter a valid coupon code.");
            }
            if (coupon is not null && (coupon.IsUsed == true || coupon.UserName != userName || coupon.DueDate < DateTime.UtcNow))
            {
                return new ApiResponse("The coupon code entered is no longer valid or not belongs to you.");
            }

            order.Payment = CalculatePayment(order, authUser, coupon);
            decimal loyaltyPointsToBeEarned = CalculateLoyaltyPoints(order, basket);
            order.LoyaltyPointsToBeEarned = loyaltyPointsToBeEarned;
            
            if (coupon is not null)
                unitOfWork.WriteRepository<Coupon>().Update(coupon);

            unitOfWork.WriteRepository<Basket>().DeleteRange(basket);
            unitOfWork.WriteRepository<Order>().Insert(order);
            unitOfWork.WriteRepository<OrderDetail>().InsertRange(orderDetailList);

            bool ısSuccess = unitOfWork.CompleteWithTransaction();
            if(ısSuccess)
            {
                await userManager.UpdateAsync(authUser);
            }

            return new ApiResponse($"Thank you. Your order number is {order.OrderNumber}. Please make payment to complete your order", true);
        }
        catch (Exception ex)
        {
            return new ApiResponse(ex.Message);
        }
    }

    private static decimal CalculateLoyaltyPoints(Order order, List<Basket> basket)
    {
        if (order.Payment != 0)
        {
            decimal totalDiscount = order.TotalAmount - order.Payment;
            decimal discountPercentage = 100 * totalDiscount / order.TotalAmount;
            decimal totalPointsToBeEarned = 0;
            if (discountPercentage != 0)
            {      
                foreach (var product in basket)
                {
                    decimal price = product.Quantity * product.Product.Price;
                    decimal priceAfterDiscount = price - (price * discountPercentage / 100);
                    decimal pointsToBeEarned = priceAfterDiscount * product.Product.PercentageOfPoints / 100;
                    decimal totalMaxPoints = product.Product.MaxPointsEarned * product.Quantity;

                    if (pointsToBeEarned >= totalMaxPoints)
                        totalPointsToBeEarned += totalMaxPoints;
                    else
                        totalPointsToBeEarned += pointsToBeEarned;
                }

                return totalPointsToBeEarned;
            }
            foreach (var product in basket)
            {
                decimal price = product.Quantity * product.Product.Price;                
                decimal pointsToBeEarned = price * product.Product.PercentageOfPoints / 100;
                decimal totalMaxPoints = product.Product.MaxPointsEarned * product.Quantity;

                if (pointsToBeEarned >= totalMaxPoints)
                    totalPointsToBeEarned += totalMaxPoints;
                else
                    totalPointsToBeEarned += pointsToBeEarned;
            }

            return totalPointsToBeEarned;

        }
        else
        {
            return 0;
        }
    }

    private static decimal CalculatePayment(Order order, User user, Coupon? coupon)
    {
        decimal payment = order.TotalAmount;

        if (coupon is not null && user.LoyaltyPoints <= 0)
        {
            order.CouponAmount = coupon.Amount;
            order.CouponCode = coupon.Code;
            payment -= coupon.Amount;
            coupon.IsUsed = true;

            if (payment < 0)
            {
                payment = 0;
                return payment;
            }

            return payment;
        }

        if (coupon is null && user.LoyaltyPoints > 0)
        {
            if (user.LoyaltyPoints >= payment)
            {
                payment = 0;
                user.LoyaltyPoints -= payment;
                order.UserLoyaltyPoints = user.LoyaltyPoints;
                return payment;
            }

            order.UserLoyaltyPoints = user.LoyaltyPoints;
            payment -= user.LoyaltyPoints;
            user.LoyaltyPoints = 0;

            return payment;
        }

        if (coupon is not null && user.LoyaltyPoints > 0)
        {
            order.CouponAmount = coupon.Amount;
            order.CouponCode = coupon.Code;
            payment -= coupon.Amount;
            coupon.IsUsed = true;

            if (payment <= 0)
            {
                payment = 0;
                order.UserLoyaltyPoints = 0;
                return payment;
            }

            if (user.LoyaltyPoints >= payment)
            {
                payment = 0;
                user.LoyaltyPoints -= payment;
                order.UserLoyaltyPoints = user.LoyaltyPoints;
                return payment;
            }

            order.UserLoyaltyPoints = user.LoyaltyPoints;
            payment -= user.LoyaltyPoints;
            user.LoyaltyPoints = 0;

            return payment;
        }
        return payment;
    }

    
}
