using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Models;
using InventoryManagement.Repositories;

namespace InventoryManagement.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IRepository<Category> _categoryRepo;

        public CategoryController(IRepository<Category> categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepo.AddAsync(category);
                await _categoryRepo.SaveAsync();
                TempData["Success"] = "Category added successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.CategoryId) return BadRequest();

            if (ModelState.IsValid)
            {
                _categoryRepo.Update(category);
                await _categoryRepo.SaveAsync();
                TempData["Success"] = "Category updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            if (category != null)
            {
                _categoryRepo.Delete(category);
                await _categoryRepo.SaveAsync();
                TempData["Success"] = "Category deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
