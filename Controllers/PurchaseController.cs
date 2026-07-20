using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Models;
using InventoryManagement.Repositories;

namespace InventoryManagement.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IRepository<Purchase> _purchaseRepo;
        private readonly IRepository<Product> _productRepo;

        public PurchaseController(IRepository<Purchase> purchaseRepo, IRepository<Product> productRepo)
        {
            _purchaseRepo = purchaseRepo;
            _productRepo = productRepo;
        }

        // GET: Purchase
        public async Task<IActionResult> Index()
        {
            var purchases = await _purchaseRepo.GetAllWithIncludesAsync(p => p.Product!);
            return View(purchases);
        }

        // GET: Purchase/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Products = await _productRepo.GetAllAsync();
            return View();
        }

        // POST: Purchase/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                var product = await _productRepo.GetByIdAsync(purchase.ProductId);
                if (product != null)
                {
                    // Core dynamic logic: Add new shipment quantities to existing stock pool
                    product.Stock += purchase.Quantity;
                    _productRepo.Update(product);

                    await _purchaseRepo.AddAsync(purchase);
                    await _purchaseRepo.SaveAsync(); // Share DB context save cycle

                    TempData["Success"] = "Stock shipment recorded, database balances incremented!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("ProductId", "Selected product invalid.");
                }
            }

            ViewBag.Products = await _productRepo.GetAllAsync();
            return View(purchase);
        }
    }
}
