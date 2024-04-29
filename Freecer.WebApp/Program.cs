using Freecer.Application.Authorization;
using Freecer.Application.Middleware;
using Freecer.Domain;
using Freecer.Domain.Configs;
using Freecer.Domain.Interfaces.Authorization;
using Freecer.Infra;
using Freecer.WebApp.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<FreecerContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddDbContext<TenantContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddDbContext<UserContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.Configure<AuthConfig>(builder.Configuration.GetSection("AuthConfig"));

builder.Services
    .AddScoped<UnitOfWork>()
    .AddScoped<ICurrentTenant, CurrentTenant>()
    .AddScoped<ICurrentUser, CurrentUser>()
    .AddScoped<IAuthService, AuthService>(serviceProvider => new AuthService(serviceProvider.GetRequiredService<UnitOfWork>().Context.Users))
    .AddScoped<ITokenService, TokenService>()
    ;

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(
        options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromDays(7);
            options.Cookie.Name = FreecerCookies.AuthCookie;
            options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
            options.SlidingExpiration = true;
        }
    );

builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(opt =>
{
    opt.AllowCredentials()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed((_) => true);
});


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication()
    .UseAuthorization();

app.UseRouting();

app.UseMiddleware<TenantResolver>();
app.UseMiddleware<UserResolver>();


app.MapControllerRoute(
    name: "api",
    pattern: "api/{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
