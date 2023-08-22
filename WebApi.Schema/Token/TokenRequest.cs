using WebApi.Base;

namespace WebApi.Schema;

public class TokenRequest: BaseRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
