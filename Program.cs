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
// File Path: Program.cs (Pipeline Configuration Section)
if (!app.Environment.IsDevelopment())
{
    // Catches uncaught runtime errors and redirects users to a friendly error page
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    // Detailed page helper during developer phases
    app.UseDeveloperExceptionPage();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
