namespace WebApi.Base;

public class IncludeType
{
    public const string ProductsToCategory = "Categories.Category";
    public const string UserToCoupon = "Coupons";
    public const string CouponToUser = "User";
    public const string BasketToUser = "User";
    public const string BasketToProduct = "Product";
    public const string ProductsToBasket = "Baskets";
    public const string OrderToProduct = "OrderDetails.Product";
    public const string OrderDetailToProduct = "Product";
}
