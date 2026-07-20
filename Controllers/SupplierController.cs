using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Models;
using InventoryManagement.Repositories;

namespace InventoryManagement.Controllers
{
    public class SupplierController : Controller
    {
        private readonly IRepository<Supplier> _supplierRepo;

        public SupplierController(IRepository<Supplier> supplierRepo)
        {
            _supplierRepo = supplierRepo;
        }

        public async Task<IActionResult> Index()
        {
            var suppliers = await _supplierRepo.GetAllAsync();
            return View(suppliers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                await _supplierRepo.AddAsync(supplier);
                await _supplierRepo.SaveAsync();
                TempData["Success"] = "Supplier registered successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var supplier = await _supplierRepo.GetByIdAsync(id);
            if (supplier == null) return NotFound();
            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Supplier supplier)
        {
            if (id != supplier.SupplierId) return BadRequest();

            if (ModelState.IsValid)
            {
                _supplierRepo.Update(supplier);
                await _supplierRepo.SaveAsync();
                TempData["Success"] = "Supplier profiles saved successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _supplierRepo.GetByIdAsync(id);
            if (supplier == null) return NotFound();
            return View(supplier);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplier = await _supplierRepo.GetByIdAsync(id);
            if (supplier != null)
            {
                _supplierRepo.Delete(supplier);
                await _supplierRepo.SaveAsync();
                TempData["Success"] = "Supplier terminated successfully!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
