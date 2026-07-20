using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RepositoryPrac.Model;
using RepositoryPrac.Data;

namespace RepositoryPrac.Pages.ProductPages;

public class IndexModel : PageModel
{
    private readonly ProductDbContext _context;

    public IndexModel(ProductDbContext context)
    {
        _context = context;
    }

    public IList<Product> Product { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Product = await _context.Products.ToListAsync();
    }
}
