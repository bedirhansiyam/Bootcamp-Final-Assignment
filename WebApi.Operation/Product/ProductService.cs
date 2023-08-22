using AutoMapper;
using System.Security.Cryptography.X509Certificates;
using WebApi.Base;
using WebApi.Data;
using WebApi.Data.Uow;
using WebApi.Schema;
using static Dapper.SqlMapper;

namespace WebApi.Operation;

public class ProductService : BaseService<Product, ProductRequest, ProductResponse>, IProductService
{

    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public ProductService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public ApiResponse AddStock(StockRequest request)
    {
        try
        {
            var product = unitOfWork.ReadRepository<Product>().GetById(request.ProductId);
            if (product is null)
                return new ApiResponse("Product not found");

            product.Stock += request.NumberOfStock;
            unitOfWork.WriteRepository<Product>().Update(product);
            unitOfWork.Complete();
            return new ApiResponse("Stock has been added", true);            
        }
        catch (Exception ex)
        {
            return new ApiResponse(ex.Message);
        }
    }
    public ApiResponse UpdateStock(StockRequest request)
    {
        try
        {
            var product = unitOfWork.ReadRepository<Product>().GetById(request.ProductId);
            if (product is null)
                return new ApiResponse("Product not found");

            product.Stock = request.NumberOfStock;
            unitOfWork.WriteRepository<Product>().Update(product);
            unitOfWork.Complete();
            return new ApiResponse("Stock has been updated", true);
        }
        catch (Exception ex)
        {
            return new ApiResponse(ex.Message);
        }
    }

    public override ApiResponse<List<ProductResponse>> GetAll()
    {
        try
        {
            var entityList = unitOfWork.ReadRepository<Product>().GetAllWithIncludes(IncludeType.ProductsToCategory);
            var mapped = mapper.Map<List<Product>, List<ProductResponse>>(entityList);
            return new ApiResponse<List<ProductResponse>>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<ProductResponse>>(ex.Message);
        }
    }

    public override ApiResponse<ProductResponse> GetById(int id)
    {
        try
        {
            var entity = unitOfWork.ReadRepository<Product>().GetByIdWithIncludes(id, IncludeType.ProductsToCategory);
            if (entity is null)
            {
                return new ApiResponse<ProductResponse>("Record not found");
            }

            var mapped = mapper.Map<Product, ProductResponse>(entity);
            return new ApiResponse<ProductResponse>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<ProductResponse>(ex.Message);
        }
    }

    public override ApiResponse Insert(ProductRequest request)
    {
        try
        {
            List<ProductCategory> productCategoryList = new();
            var entity = mapper.Map<ProductRequest, Product>(request);

            unitOfWork.WriteRepository<Product>().Insert(entity);
            unitOfWork.Complete();

            for (int i = 0; i<request.CategoryIds.Length; i++)
            {
                ProductCategory productCategory = new();
                productCategory.ProductId = entity.Id;
                productCategory.CategoryId = request.CategoryIds[i];
                productCategoryList.Add(productCategory);                
            }
            
            unitOfWork.WriteRepository<ProductCategory>().InsertRange(productCategoryList);
            unitOfWork.Complete();            

            return new ApiResponse("The product has been successfully created", true);
        }
        catch (Exception ex)
        {
            return new ApiResponse(ex.Message);
        }
    }

    public override ApiResponse Update(int id, ProductRequest request)
    {
        try
        {
            var entity = mapper.Map<ProductRequest, Product>(request);

            var exist = unitOfWork.ReadRepository<Product>().GetById(id);
            if (exist is null)
            {
                return new ApiResponse("Product not found");
            }
            entity.Id = id;
            unitOfWork.WriteRepository<Product>().Update(entity);

            var oldCategories = unitOfWork.ReadRepository<ProductCategory>().Where(x => x.ProductId == id).ToList();
            unitOfWork.WriteRepository<ProductCategory>().DeleteRange(oldCategories);

            for (int i = 0; i < request.CategoryIds.Length; i++)
            {
                ProductCategory productCategory = new();
                productCategory.ProductId = entity.Id;
                productCategory.CategoryId = request.CategoryIds[i];
                unitOfWork.WriteRepository<ProductCategory>().Insert(productCategory);
            }

            unitOfWork.CompleteWithTransaction();

            return new ApiResponse("The product has been successfully updated", true);
        }
        catch (Exception ex)
        {
            return new ApiResponse(ex.Message);
        }
    }

    public ApiResponse<List<ProductResponse>> GetByCategory(int categoryId)
    {
        try
        {
            List<Product> productList = new();

            var category = unitOfWork.ReadRepository<ProductCategory>().Where(x => x.CategoryId == categoryId).ToList();
            if (category is null)
            {
                return new ApiResponse<List<ProductResponse>>("There are no products in this category");
            }

            foreach (var item in category)
            {
                Product product = new();
                product = unitOfWork.ReadRepository<Product>()
                    .WhereWithInclude(x => x.Id == item.ProductId, IncludeType.ProductsToCategory).FirstOrDefault();

                productList.Add(product);
            }


            var mapped = mapper.Map<List<Product>, List<ProductResponse>>(productList);
            return new ApiResponse<List<ProductResponse>>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<ProductResponse>>(ex.Message);
        }
    }

    public ApiResponse ActivateProduct(int id)
    {
        try
        {
            var product = unitOfWork.ReadRepository<Product>().GetById(id);
            if (product.IsActive == true)
                return new ApiResponse("This product is already active", true);

            product.IsActive = true;
            unitOfWork.WriteRepository<Product>().Update(product);
            unitOfWork.Complete();
            return new ApiResponse("The product has been activated", true);
        }
        catch(Exception ex)
        {
            return new ApiResponse(ex.Message);
        }
    }

    public ApiResponse InactivateProduct(int id)
    {
        try
        {
            var product = unitOfWork.ReadRepository<Product>().GetById(id);
            if (product.IsActive == false)
                return new ApiResponse("This product is already inactive", true);

            product.IsActive = false;
            unitOfWork.WriteRepository<Product>().Update(product);
            unitOfWork.Complete();
            return new ApiResponse("The product has been inactivated", true);
        }
        catch (Exception ex)
        {
            return new ApiResponse(ex.Message);
        }
    }
}
