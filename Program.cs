using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. Configure EF Core Database Connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Register Generic Repository Implementation
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// 3. Setup HTTP Pipeline & Exception Handlers
if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
