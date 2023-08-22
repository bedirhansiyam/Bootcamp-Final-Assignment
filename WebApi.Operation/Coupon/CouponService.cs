using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WebApi.Base;
using WebApi.Data;
using WebApi.Data.Uow;
using WebApi.Schema;

namespace WebApi.Operation;

public class CouponService : BaseService<Coupon, CouponRequest, CouponResponse>, ICouponService
{
    private readonly UserManager<User> userManager;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public CouponService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager) : base(unitOfWork, mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.userManager = userManager;
    }

    public override ApiResponse<List<CouponResponse>> GetAll()
    {
        try
        {
            var entityList = unitOfWork.ReadRepository<Coupon>().GetAllWithIncludes(IncludeType.CouponToUser);
            var mapped = mapper.Map<List<Coupon>, List<CouponResponse>>(entityList);
            return new ApiResponse<List<CouponResponse>>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<CouponResponse>>(ex.Message);
        }
    }

    public override ApiResponse<CouponResponse> GetById(int id)
    {
        try
        {
            var entity = unitOfWork.ReadRepository<Coupon>().GetByIdWithIncludes(id, IncludeType.CouponToUser);
            if (entity is null)
            {
                return new ApiResponse<CouponResponse>("Record not found");
            }

            var mapped = mapper.Map<Coupon, CouponResponse>(entity);
            return new ApiResponse<CouponResponse>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<CouponResponse>(ex.Message);
        }
    }

    public ApiResponse InsertRange(int numberOfCoupons, CouponRequest request)
    {
        try
        {
            var entity = mapper.Map<CouponRequest, Coupon>(request);

            List<Coupon> coupons = new();
            for (int i = 0; i < numberOfCoupons; i++)
            {
                Coupon coupon = new();
                coupon.Code = CodeGenerator.CouponCode();
                coupon.IsUsed = entity.IsUsed;
                coupon.User = entity.User;
                coupon.Amount = entity.Amount;
                coupon.DueDate = entity.DueDate;
                coupon.UserName = null;
                coupons.Add(coupon);
            }
            unitOfWork.WriteRepository<Coupon>().InsertRange(coupons);
            unitOfWork.Complete();
            return new ApiResponse("Coupons have been successfully created", true);
        }
        catch (Exception ex)
        {
            return new ApiResponse(ex.Message);
        }
    }

    public override ApiResponse Insert(CouponRequest request)
    {
        try
        {
            var entity = mapper.Map<CouponRequest, Coupon>(request);
            var code = CodeGenerator.CouponCode();
            entity.Code = code;

            entity.UserName = null;

            unitOfWork.WriteRepository<Coupon>().Insert(entity);
            unitOfWork.Complete();
            return new ApiResponse($"The coupon has been successfully created. Code = {code}", true);
        }
        catch (Exception ex)
        {
            return new ApiResponse(ex.Message);
        }
    }

    public ApiResponse<List<CouponResponse>> GetByUserId(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            return new ApiResponse<List<CouponResponse>>("Invalid user name");

        try
        {
            var user = userManager.Users.Where(x => x.UserName == userName).FirstOrDefault();
            if (user is null)
                return new ApiResponse<List<CouponResponse>>("User does not exist");

            var entityList = unitOfWork.ReadRepository<Coupon>()
                .WhereWithInclude(x => x.UserName == userName, IncludeType.CouponToUser).ToList();
            var mapped = mapper.Map<List<Coupon>, List<CouponResponse>>(entityList);
            return new ApiResponse<List<CouponResponse>>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<CouponResponse>>(ex.Message);
        }
    }

    public ApiResponse AssignToUser(AssignCouponRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.UserName))
            return new ApiResponse("Invalid user name");

        if (request.CouponId == 0)
            return new ApiResponse("Invalid coupon id");

        try
        {
            var user = userManager.Users.Where(x => x.UserName == request.UserName).FirstOrDefault();
            if (user is null)
                return new ApiResponse("User does not exist");

            var coupon = unitOfWork.ReadRepository<Coupon>().GetById(request.CouponId);

            if (coupon.DueDate < DateTime.Now)
                return new ApiResponse("Coupon due date is already expired");

            if (coupon.IsUsed == true)
                return new ApiResponse("Coupon is already used");

            coupon.UserName = user.UserName;

            unitOfWork.WriteRepository<Coupon>().Update(coupon);
            unitOfWork.Complete();

            return new ApiResponse("The coupon has been successfully assigned to user", true);
        }
        catch (Exception ex)
        {
            return new ApiResponse(ex.Message);
        }
    }

    public override ApiResponse Update(int id, CouponRequest request)
    {
        try
        {
            var entity = unitOfWork.ReadRepository<Coupon>().GetById(id);
            if (entity is null)
            {
                return new ApiResponse("Record not found");
            }
            entity.Amount = request.Amount;
            entity.DueDate = request.DueDate;

            unitOfWork.WriteRepository<Coupon>().Update(entity);
            unitOfWork.Complete();
            return new ApiResponse("The coupon has been successfully updated", true);
        }
        catch (Exception ex)
        {
            return new ApiResponse(ex.Message);
        }
    }

    
}
