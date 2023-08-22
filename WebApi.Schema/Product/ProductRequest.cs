using WebApi.Base;

namespace WebApi.Schema;

public class ProductRequest : BaseRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ProducerCompany { get; set; }
    public DateTime ReleaseDate { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public decimal MaxPointsEarned { get; set; }
    public decimal PercentageOfPoints { get; set; }
    public bool IsActive { get; set; }
    public int[] CategoryIds { get; set; }
}
