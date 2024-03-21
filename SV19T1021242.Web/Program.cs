using Microsoft.AspNetCore.Authentication.Cookies;
using SV19T1021242.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
     .AddMvcOptions(option =>
     {
         option.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
     });
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    option.Cookie.Name = "AuthenticationCookie";
                    option.LoginPath = "/Account/Login";
                    option.AccessDeniedPath = "/Account/AccessDenied";
                    option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                });
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(60);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


ApplicationContext.Configure
(
    httpContextAccessor: app.Services.GetRequiredService<IHttpContextAccessor>(),
    hostEnvironment: app.Services.GetService<IWebHostEnvironment>()
);
string connectionString = "server=localhost;user id =sa;password=Rinpro123123!;database=LiteCommerceDB;TrustServerCertificate=true";
SV19T1021242.BusinessLayers.Configuration.Initialize(connectionString);
app.Run();

