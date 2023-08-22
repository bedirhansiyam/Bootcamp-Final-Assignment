using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebApi.Base;
using WebApi.Data;
using WebApi.Data.Uow;
using WebApi.Schema;

namespace WebApi.Operation;

public class BasketService : BaseService<Basket, BasketRequest, BasketResponse>, IBasketService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly UserManager<User> userManager;
    public BasketService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager) : base(unitOfWork, mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.userManager = userManager;
    }

    public ApiResponse ClearBasket(ClaimsPrincipal user)
    {
        if (user is null)
            return new ApiResponse("Please sign in first");

        string userId = userManager.GetUserId(user);

        try
        {
            var basketList = unitOfWork.ReadRepository<Basket>().Where(x => x.UserId == userId).ToList();
            if (!basketList.Any())
            {
                return new ApiResponse("Your basket is empty");
            }

            foreach (var item in basketList)
            {
                var product = unitOfWork.ReadRepository<Product>().Where(x => x.Id == item.ProductId).FirstOrDefault();
                product.Stock += item.Quantity;
                unitOfWork.WriteRepository<Product>().Update(product);
            }

            unitOfWork.WriteRepository<Basket>().DeleteRange(basketList);
            unitOfWork.CompleteWithTransaction();
            return new ApiResponse("Your basket has been emptied", true);
        }
        catch (Exception ex)
        {
            return new ApiResponse(ex.Message);
        }
    }

    public override ApiResponse<List<BasketResponse>> GetAll()
    {
        try
        {
            var entityList = unitOfWork.ReadRepository<Basket>()
                .GetAllWithIncludes(new string[] { IncludeType.BasketToUser, IncludeType.BasketToProduct });
            var mapped = mapper.Map<List<Basket>, List<BasketResponse>>(entityList);
            return new ApiResponse<List<BasketResponse>>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<BasketResponse>>(ex.Message);
        }
    }

    public ApiResponse<List<BasketResponse>> GetByUserId(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            return new ApiResponse<List<BasketResponse>>("Invalid user id");

        try
        {
            var entityList = unitOfWork.ReadRepository<Basket>()
                .WhereWithInclude(x => x.UserId == userId, new string[] { IncludeType.BasketToUser, IncludeType.BasketToProduct }).ToList();

            var mapped = mapper.Map<List<Basket>, List<BasketResponse>>(entityList);
            return new ApiResponse<List<BasketResponse>>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<BasketResponse>>(ex.Message);
        }
    }

    public ApiResponse Insert(BasketRequest request, ClaimsPrincipal user)
    {
        if (user is null)
            return new ApiResponse("Please sign in first");

        if (request.Quantity < 0)
            return new ApiResponse("Invalid quantity");

        if (request.ProductId < 0)
            return new ApiResponse("Invalid product id");

        string userId = userManager.GetUserId(user);

        try
        {
            var item = unitOfWork.ReadRepository<Basket>()
                .Where(x => x.UserId == userId && x.ProductId == request.ProductId).FirstOrDefault();
            var product = unitOfWork.ReadRepository<Product>()
                .Where(x => x.Id == request.ProductId).FirstOrDefault();

            if(product.Stock < request.Quantity)
                return new ApiResponse("Not enough products are in stock");

            if(product.IsActive == false)
                return new ApiResponse("Product is not active");

            if (item is not null)
            {
                item.Quantity += request.Quantity;
                unitOfWork.WriteRepository<Basket>().Update(item);
                product.Stock -= request.Quantity;
                unitOfWork.WriteRepository<Product>().Update(product);
                unitOfWork.CompleteWithTransaction();
                return new ApiResponse("Product has been successfully added to your basket", true);
            }

            var entity = mapper.Map<BasketRequest, Basket>(request);
            entity.UserId = userId;
            unitOfWork.WriteRepository<Basket>().Insert(entity);
            product.Stock -= request.Quantity;
            unitOfWork.WriteRepository<Product>().Update(product);
            unitOfWork.CompleteWithTransaction();
            return new ApiResponse("Product has been successfully added to your basket", true);

        }
        catch (Exception ex)
        {
            return new ApiResponse(ex.Message);
        }
    }

    public ApiResponse RemoveItem(ClaimsPrincipal user, int productId)
    {
        if (user is null)
            return new ApiResponse("Please sign in first");

        string userId = userManager.GetUserId(user);

        try
        {
            var item = unitOfWork.ReadRepository<Basket>()
                .Where(x => x.UserId == userId && x.ProductId == productId).FirstOrDefault();
            if (item is null)
                return new ApiResponse("This product is not in your basket");

            var product = unitOfWork.ReadRepository<Product>().Where(x => x.Id ==  productId).FirstOrDefault();
            product.Stock += item.Quantity;

            unitOfWork.WriteRepository<Product>().Update(product);
            unitOfWork.WriteRepository<Basket>().Delete(item);
            unitOfWork.CompleteWithTransaction();
            return new ApiResponse("Product has been successfully removed from your basket", true);
        }
        catch (Exception ex)
        {
            return new ApiResponse(ex.Message);
        }
    }

    public ApiResponse Update(ClaimsPrincipal user, BasketRequest request)
    {
        if (request.Quantity < 0)
            return new ApiResponse("Invalid quantity");

        if (request.ProductId < 0)
            return new ApiResponse("Invalid product id");

        string userId = userManager.GetUserId(user);

        try
        {
            var item = unitOfWork.ReadRepository<Basket>()
                .Where(x => x.UserId == userId && x.ProductId == request.ProductId).FirstOrDefault();
            if (item is null)
                return new ApiResponse("Item not found");

            var product = unitOfWork.ReadRepository<Product>().Where(x => x.Id == request.ProductId).FirstOrDefault();
            product.Stock += request.Quantity;
            unitOfWork.WriteRepository<Product>().Update(product);
            unitOfWork.Complete();

            item.Quantity = request.Quantity;

            unitOfWork.WriteRepository<Basket>().Update(item);
            unitOfWork.Complete();
            return new ApiResponse("Your basket has been successfully updated", true);
        }
        catch (Exception ex)
        {
            return new ApiResponse(ex.Message);
        }
    }
    public ApiResponse<List<BasketResponse>> GetMyBasket(ClaimsPrincipal user)
    {
        if (user is null)
            return new ApiResponse<List<BasketResponse>>("Please sign in first");

        string userId = userManager.GetUserId(user);

        try
        {
            var entityList = unitOfWork.ReadRepository<Basket>()
                .WhereWithInclude(x => x.UserId == userId, new string[] { IncludeType.BasketToUser, IncludeType.BasketToProduct }).ToList();

            var mapped = mapper.Map<List<Basket>, List<BasketResponse>>(entityList);
            return new ApiResponse<List<BasketResponse>>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<BasketResponse>>(ex.Message);
        }
    }
}
