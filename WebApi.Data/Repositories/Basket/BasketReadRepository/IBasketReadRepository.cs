namespace WebApi.Data.Repositories;

public interface IBasketReadRepository : IReadRepository<Basket>
{
    List<Basket> GetByUserId(string userId);
}
