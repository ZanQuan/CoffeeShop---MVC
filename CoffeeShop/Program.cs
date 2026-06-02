using CoffeeShop.Data;
using CoffeeShop.Models.Interfaces;
using CoffeeShop.Models.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Đăng ký các Repository
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>(
    sp => ShoppingCartRepository.GetCart(sp));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Đăng ký DbContext
builder.Services.AddDbContext<CoffeeshopDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("CoffeeShopDbContextConnection")
    )
);

// Thêm ASP.NET Identity ← THÊM MỚI
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CoffeeshopDbContext>();

// Session và HttpContextAccessor
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

// Razor Pages (cần cho Identity UI) 
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// Seed role Admin
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider
        .GetRequiredService<RoleManager<IdentityRole>>();
    if (!await roleManager.RoleExistsAsync("Admin"))
        await roleManager.CreateAsync(new IdentityRole("Admin"));
}

app.Run();