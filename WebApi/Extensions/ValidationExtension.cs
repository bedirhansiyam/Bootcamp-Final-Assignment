using FluentValidation.AspNetCore;
using WebApi.Schema;

namespace WebApi.Service;

public static class ValidationExtension
{
    public static void AddValidationExtension(this IServiceCollection services)
    {
        services.AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<TokenRequestValidator>());
    }
}
