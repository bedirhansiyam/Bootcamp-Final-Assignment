using AutoMapper;
using WebApi.Base;
using WebApi.Data;
using WebApi.Data.Uow;
using WebApi.Schema;
using static Dapper.SqlMapper;

namespace WebApi.Operation;

public class CategoryService : BaseService<Category, CategoryRequest, CategoryResponse>, ICategoryService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public override ApiResponse Delete(int id)
    {
        try
        {
            var entity = unitOfWork.ReadRepository<Category>().GetById(id);
            if (entity is null)
            {
                return new ApiResponse("Record not found");
            }

            var exist = unitOfWork.ReadRepository<ProductCategory>().Where(x => x.CategoryId == id);
            if(exist.Any())
            {
                return new ApiResponse("There are some products under this category. Cannot be deleted");
            }

            unitOfWork.WriteRepository<Category>().DeleteById(id);
            unitOfWork.Complete();
            return new ApiResponse("The category has been successfully deleted", true);
        }
        catch (Exception ex)
        {
            return new ApiResponse(ex.Message);
        }
    }
}
