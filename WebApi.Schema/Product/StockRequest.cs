using WebApi.Base;

namespace WebApi.Schema;

public class StockRequest : BaseRequest
{
    public int ProductId { get; set; }
    public int NumberOfStock { get; set; }
   
}
