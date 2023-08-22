using WebApi.Base;

namespace WebApi.Schema;

public class OrderDetailResponse : BaseResponse
{
    public string OrderNumber { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public string UserName { get; set; }
}
