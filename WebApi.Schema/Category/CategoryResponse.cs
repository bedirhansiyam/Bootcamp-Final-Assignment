using WebApi.Base;

namespace WebApi.Schema;

public class CategoryResponse : BaseResponse
{
    public string Name { get; set; }
    public string Tag { get; set; }
    public string Url { get; set; }
}
