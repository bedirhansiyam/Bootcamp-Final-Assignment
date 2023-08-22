namespace WebApi.Schema;

public class PaymentResponse
{
    public string Message { get; set; }
    public string ReferenceNumber { get; set; }
    public string OrderNumber { get; set; }
    public decimal Amount { get; set; }
}
