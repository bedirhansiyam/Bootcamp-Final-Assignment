using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebApi.Base;
using WebApi.Data;
using WebApi.Data.Uow;
using WebApi.Schema;

namespace WebApi.Operation;

public class OrderDetailService : IOrderDetailService
{
    private readonly IMapper mapper;
    private readonly UserManager<User> userManager;
    private readonly IUnitOfWork unitOfWork;

    public OrderDetailService(IMapper mapper, UserManager<User> userManager, IUnitOfWork unitOfWork)
    {
        this.mapper = mapper;
        this.userManager = userManager;
        this.unitOfWork = unitOfWork;
    }

    public ApiResponse<List<OrderDetailResponse>> GetAll()
    {
        try
        {
            var orderDetailList = unitOfWork.ReadRepository<OrderDetail>()
                .GetAllWithIncludes(IncludeType.OrderDetailToProduct).ToList();

            var mapped = mapper.Map<List<OrderDetail>, List<OrderDetailResponse>>(orderDetailList);
            return new ApiResponse<List<OrderDetailResponse>>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<OrderDetailResponse>>(ex.Message);
        }
    }

    public async Task<ApiResponse<List<OrderDetailResponse>>> GetByOrderNumber(ClaimsPrincipal user, string orderNumber)
    {
        try
        {
            var authUser = await userManager.GetUserAsync(user);
            var userName = authUser.UserName;

            var orderDetailList = unitOfWork.ReadRepository<OrderDetail>()
                .WhereWithInclude(x => x.OrderNumber == orderNumber, IncludeType.OrderDetailToProduct).ToList();

            if(authUser.Role == Role.Admin || orderDetailList[0].UserName == userName)
            {
                var mapped = mapper.Map<List<OrderDetail>, List<OrderDetailResponse>>(orderDetailList);
                return new ApiResponse<List<OrderDetailResponse>>(mapped);
            }

            return new ApiResponse<List<OrderDetailResponse>>("You are not authorized to view the details of this order");
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<OrderDetailResponse>>(ex.Message);
        }
    }
}
