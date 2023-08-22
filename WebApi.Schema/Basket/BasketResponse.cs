using WebApi.Base;

namespace WebApi.Schema;

public class BasketResponse : BaseResponse
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public int Quantity { get; set; }
    public string UserId { get; set; }
}
