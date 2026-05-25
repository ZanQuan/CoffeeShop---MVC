using CoffeeShop.Data;
using CoffeeShop.Models.Interfaces;
using CoffeeShop.Models.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Đăng ký ProductRepository
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Đăng ký ShoppingCartRepository (dùng GetCart để tạo theo session)
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>(
    sp => ShoppingCartRepository.GetCart(sp));

// Đăng ký DbContext
builder.Services.AddDbContext<CoffeeshopDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("CoffeeShopDbContextConnection")
    )
);

// Thêm Session và HttpContextAccessor
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Bật Session (phải đặt TRƯỚC MapControllerRoute)
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();