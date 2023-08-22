using Autofac;
using WebApi.Data.Uow;
using WebApi.Operation;

namespace WebApi.Service;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
        builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
        builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
        builder.RegisterType<CouponService>().As<ICouponService>().InstancePerLifetimeScope();
        builder.RegisterType<BasketService>().As<IBasketService>().InstancePerLifetimeScope();
        builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
        builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
        builder.RegisterType<OrderDetailService>().As<IOrderDetailService>().InstancePerLifetimeScope();
        builder.RegisterType<PaymentService>().As<IPaymentService>().InstancePerLifetimeScope();

        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
    }
}
