using WebApi.Base;

namespace WebApi.Schema;

public class CategoryRequest : BaseRequest
{
    public string Name { get; set; }
    public string Tag { get; set; }
    public string Url { get; set; }
}
