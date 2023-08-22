using AutoMapper;
using WebApi.Base;
using WebApi.Data.Uow;

namespace WebApi.Operation;

public class BaseService<TEntity, TRequest, TResponse> : IBaseService<TEntity, TRequest, TResponse> where TEntity : BaseEntity
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public virtual ApiResponse Delete(int id)
    {
        try
        {
            var entity = unitOfWork.ReadRepository<TEntity>().GetById(id);
            if (entity is null)
            {
                return new ApiResponse("Record not found");
            }

            unitOfWork.WriteRepository<TEntity>().DeleteById(id);
            unitOfWork.Complete();
            return new ApiResponse();
        }
        catch (Exception ex)
        {
            return new ApiResponse(ex.Message);
        }
    }

    public virtual ApiResponse<List<TResponse>> GetAll()
    {
        try
        {
            var entityList = unitOfWork.ReadRepository<TEntity>().GetAllWithIncludes();
            var mapped = mapper.Map<List<TEntity>, List<TResponse>>(entityList);
            return new ApiResponse<List<TResponse>>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<TResponse>>(ex.Message);
        }
    }

    public virtual ApiResponse<TResponse> GetById(int id)
    {
        try
        {
            var entity = unitOfWork.ReadRepository<TEntity>().GetByIdWithIncludes(id);
            if (entity is null)
            {
                return new ApiResponse<TResponse>("Record not found");
            }

            var mapped = mapper.Map<TEntity, TResponse>(entity);
            return new ApiResponse<TResponse>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<TResponse>(ex.Message);
        }
    }

    public virtual ApiResponse Insert(TRequest request)
    {
        try
        {
            var entity = mapper.Map<TRequest, TEntity>(request);
            unitOfWork.WriteRepository<TEntity>().Insert(entity);
            unitOfWork.Complete();
            return new ApiResponse();
        }
        catch(Exception ex)
        {
            return new ApiResponse(ex.Message);
        }
    }

    public virtual ApiResponse Update(int id, TRequest request)
    {
        try
        {
            var entity = mapper.Map<TRequest, TEntity>(request);

            var exist = unitOfWork.ReadRepository<TEntity>().GetById(id);
            if(exist is null)
            {
                return new ApiResponse("Record not found");
            }

            entity.Id = id;

            unitOfWork.WriteRepository<TEntity>().Update(entity);
            unitOfWork.Complete();
            return new ApiResponse();
        }
        catch(Exception ex)
        {
            return new ApiResponse(ex.Message);
        }
    }
}
