using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Models;
using InventoryManagement.Repositories;

namespace InventoryManagement.Controllers
{
    public class SalesController : Controller
    {
        private readonly IRepository<Sales> _salesRepo;
        private readonly IRepository<Product> _productRepo;

        public class StockValidationException : Exception
        {
            public StockValidationException(string message) : base(message) { }
        }

        public SalesController(IRepository<Sales> salesRepo, IRepository<Product> productRepo)
        {
            _salesRepo = salesRepo;
            _productRepo = productRepo;
        }

        // GET: Sales
        public async Task<IActionResult> Index()
        {
            var sales = await _salesRepo.GetAllWithIncludesAsync(s => s.Product!);
            return View(sales);
        }

        // GET: Sales/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Products = await _productRepo.GetAllAsync();
            return View();
        }

        // POST: Sales/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sales sale)
        {
            if (ModelState.IsValid)
            {
                var product = await _productRepo.GetByIdAsync(sale.ProductId);
                if (product != null)
                {
                    // Safeguard Check: Prevent users from selling more than what is currently in stock
                    if (product.Stock < sale.Quantity)
                    {
                        ModelState.AddModelError("Quantity", $"Insufficient Stock! Only {product.Stock} units remaining in the warehouse.");
                        ViewBag.Products = await _productRepo.GetAllAsync();
                        return View(sale);
                    }

                    // Core dynamic logic: Decrement stock pool balance
                    product.Stock -= sale.Quantity;
                    _productRepo.Update(product);

                    await _salesRepo.AddAsync(sale);
                    await _salesRepo.SaveAsync();

                    TempData["Success"] = "Sales order processed successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("ProductId", "Selected product does not exist.");
                }
            }

            ViewBag.Products = await _productRepo.GetAllAsync();
            return View(sale);
        }
    }
}
