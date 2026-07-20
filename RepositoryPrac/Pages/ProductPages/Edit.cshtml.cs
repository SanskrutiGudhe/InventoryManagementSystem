using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RepositoryPrac.Model;
using RepositoryPrac.Data;

namespace RepositoryPrac.Pages.ProductPages;

public class EditModel : PageModel
{
    private readonly ProductDbContext _context;

    public EditModel(ProductDbContext context)
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
        Product = product;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(Product.ID))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool ProductExists(System.Guid id)
    {
        return _context.Products.Any(e => e.ID == id);
    }
}
