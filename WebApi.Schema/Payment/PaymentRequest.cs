using WebApi.Base;

namespace WebApi.Schema;

public class PaymentRequest : BaseRequest
{
    public string CardNumber { get; set; }
    public string CardHolder { get; set; }
    public int Cvv { get; set; }
    public int ExpireYear { get; set; }
    public int ExpireMonth { get; set; }

}
