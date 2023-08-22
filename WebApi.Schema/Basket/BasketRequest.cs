using WebApi.Base;

namespace WebApi.Schema;

public class BasketRequest : BaseRequest
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
