using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RepositoryPrac.Model;
using RepositoryPrac.Data;

namespace RepositoryPrac.Pages.ProductPages;

public class DetailsModel : PageModel
{
    private readonly ProductDbContext _context;
    public DetailsModel(ProductDbContext context)
    {
        _context = context;
    }

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
}
