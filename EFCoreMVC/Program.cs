using EFCoreMVC.Authorization;
using EFCoreMVC.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add database context
builder.Services.AddDbContext<EFCoreMVCContext>(options => 
    options.UseMySQL("server=localhost;database=EFCoreMVCContext;user=newuser;password=M@tkhau108"));

//Install-Package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Add Authentication services
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Account/Denied";
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });
//builder.Services.AddAuthorization();
//builder.Services.AddSingleton<IAuthorizationHandler, RoleAuthorizationHandler>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

//Khởi tạo dữ liệu mẫu ban đầu cho CSDL
using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider services = scope.ServiceProvider;
    var context = services.GetRequiredService<EFCoreMVCContext>();
    DbInitializer.Initialize(context);
}


app.UseStaticFiles();

app.UseRouting();

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
