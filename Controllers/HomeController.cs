// File Path: Controllers/HomeController.cs
using InventoryManagement.Models;
using InventoryManagement.Repositories;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InventoryManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<Purchase> _purchaseRepo;
        private readonly IRepository<Sales> _salesRepo;

        public HomeController(
            IRepository<Product> productRepo,
            IRepository<Purchase> purchaseRepo,
            IRepository<Sales> salesRepo)
        {
            _productRepo = productRepo;
            _purchaseRepo = purchaseRepo;
            _salesRepo = salesRepo;
        }

        // GET: Dashboard (Conventional Route mapped to root /)
        public async Task<IActionResult> Index()
        {
            // Fetch transactional entities with their navigations
            var products = await _productRepo.GetAllWithIncludesAsync(p => p.Category!, p => p.Supplier!);
            var sales = await _salesRepo.GetAllWithIncludesAsync(s => s.Product!);
            var purchases = await _purchaseRepo.GetAllWithIncludesAsync(p => p.Product!);

            // 1. Calculate General Aggregations
            ViewBag.TotalProducts = products.Count();
            ViewBag.TotalStockInWarehouse = products.Sum(p => p.Stock);
            ViewBag.TotalSalesTransactions = sales.Count();
            ViewBag.TotalPurchasesTransactions = purchases.Count();

            // 2. Financial Dashboard Calculations
            // Gross Sales Value = Sold Quantity * Product Unit Price
            decimal grossSalesVal = sales.Sum(s => s.Quantity * (s.Product?.Price ?? 0));
            ViewData["GrossSalesRevenue"] = grossSalesVal.ToString("C");

            // 3. Extract items under 5 units for our Partial Warning component
            var lowStockProducts = products.Where(p => p.Stock < 5).ToList();

            // 4. Group Sales Data by Product Name for our analytical Chart (passed as raw arrays for simplicity)
            var topSellingGroup = sales
                .GroupBy(s => s.Product?.ProductName ?? "Unknown")
                .Select(g => new { ProductName = g.Key, QuantitySold = g.Sum(x => x.Quantity) })
                .OrderByDescending(o => o.QuantitySold)
                .Take(5)
                .ToList();

            ViewBag.ChartLabels = topSellingGroup.Select(x => x.ProductName).ToArray();
            ViewBag.ChartData = topSellingGroup.Select(x => x.QuantitySold).ToArray();

            return View(lowStockProducts); // Return low-stock list as the Model
        }

        // Dedicated Exception Error Fallback Action
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
