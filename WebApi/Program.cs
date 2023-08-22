using Autofac;
using Autofac.Extensions.DependencyInjection;
using Serilog;
using WebApi.Base;
using WebApi.Service;
using WebApi.Service.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetService<IConfiguration>();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacModule());
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddResponseCompression();
builder.Services.AddCustomSwaggerExtension();
builder.Services.AddDbContextExtension(configuration);
builder.Services.AddLoggerExtension(configuration);
builder.Services.AddMapperExtension();
builder.Services.AddValidationExtension();
builder.Services.AddAuthorizationExtension();
builder.Services.AddSwaggerGen();
builder.Services.AddJwtExtension(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DefaultModelsExpandDepth(-1);
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi Project");
        c.DocumentTitle = "WebApi Project";
    });
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
Action<RequestProfilerModel> requestResponseHandler = requestProfilerModel =>
{
    Log.Information("-------------Request-Begin------------");
    Log.Information(requestProfilerModel.Request);
    Log.Information(Environment.NewLine);
    Log.Information(requestProfilerModel.Response);
    Log.Information("-------------Request-End------------");
};
app.UseMiddleware<RequestLoggingMiddleware>(requestResponseHandler);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
