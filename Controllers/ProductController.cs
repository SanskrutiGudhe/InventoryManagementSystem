using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Models;
using InventoryManagement.Repositories;

namespace InventoryManagement.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<Category> _categoryRepo;
        private readonly IRepository<Supplier> _supplierRepo;

        public ProductController(
            IRepository<Product> productRepo,
            IRepository<Category> categoryRepo,
            IRepository<Supplier> supplierRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _supplierRepo = supplierRepo;
        }

        // GET: Product (With Search)
        public async Task<IActionResult> Index(string searchString)
        {
            var products = await _productRepo.GetAllWithIncludesAsync(p => p.Category!, p => p.Supplier!);

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase));
                ViewData["CurrentFilter"] = searchString;
            }

            return View(products);
        }

        // GET: Product/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryRepo.GetAllAsync();
            ViewBag.Suppliers = await _supplierRepo.GetAllAsync();
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepo.AddAsync(product);
                await _productRepo.SaveAsync();
                TempData["Success"] = "Product cataloged successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = await _categoryRepo.GetAllAsync();
            ViewBag.Suppliers = await _supplierRepo.GetAllAsync();
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null) return NotFound();

            ViewBag.Categories = await _categoryRepo.GetAllAsync();
            ViewBag.Suppliers = await _supplierRepo.GetAllAsync();
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.ProductId) return BadRequest();

            if (ModelState.IsValid)
            {
                _productRepo.Update(product);
                await _productRepo.SaveAsync();
                TempData["Success"] = "Product details adjusted!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = await _categoryRepo.GetAllAsync();
            ViewBag.Suppliers = await _supplierRepo.GetAllAsync();
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null) return NotFound();

            // Fetch list item with related models for full details display
            var productsList = await _productRepo.GetAllWithIncludesAsync(p => p.Category!, p => p.Supplier!);
            var productDetails = productsList.FirstOrDefault(p => p.ProductId == id);

            return View(productDetails);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product != null)
            {
                _productRepo.Delete(product);
                await _productRepo.SaveAsync();
                TempData["Success"] = "Product removed from catalog.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
