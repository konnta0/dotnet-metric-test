using Infrastructure.Context;
using Infrastructure.Extension;
using MessagePack.AspNetCoreMvcFormatter;
using MessagePack.Resolvers;
using MessagePipe;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Presentation.Extension;
using UseCase.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));

    options.OutputFormatters.Add(new MessagePackOutputFormatter(ContractlessStandardResolver.Options));
    options.InputFormatters.Add(new MessagePackInputFormatter(ContractlessStandardResolver.Options));
});


builder.Services.AddMessagePipe(options =>
{
#if DEBUG
    options.EnableCaptureStackTrace = true;
#endif
});

builder.Services.AddUseCase(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "DotnetMetricTest",
        Description = "This is metric test service."
    });
});

builder.Services.AddDbContext<EmployeesContext>(optionsBuilder =>
{
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));
    optionsBuilder.UseMySql(EmployeesContext.GetConnectionString(), serverVersion)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseInfrastructure();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
