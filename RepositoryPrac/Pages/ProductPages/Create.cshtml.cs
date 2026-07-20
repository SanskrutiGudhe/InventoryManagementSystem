using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RepositoryPrac.Model;
using RepositoryPrac.Data;

namespace RepositoryPrac.Pages.ProductPages;

public class CreateModel : PageModel
{
    private readonly ProductDbContext _context;

    public CreateModel(ProductDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Product Product { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Products.Add(Product);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
