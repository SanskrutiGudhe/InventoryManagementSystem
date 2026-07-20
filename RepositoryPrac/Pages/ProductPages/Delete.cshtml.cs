using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RepositoryPrac.Model;
using RepositoryPrac.Data;

namespace RepositoryPrac.Pages.ProductPages;

public class DeleteModel : PageModel
{
    private readonly ProductDbContext _context;

    public DeleteModel(ProductDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Product Product { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(System.Guid? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var product = await _context.Products.FirstOrDefaultAsync(m => m.ID == id);
        if (product is null)
        {
            return NotFound();
        }
        else
        {
            Product = product;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(System.Guid? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            Product = product;
            _context.Products.Remove(Product);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
