using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        [StringLength(100, ErrorMessage = "Product Name cannot exceed 100 characters.")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 999999.99, ErrorMessage = "Price must be greater than zero.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Initial Stock is required.")]
        [Range(0, 1000000, ErrorMessage = "Stock level cannot be negative.")]
        [Display(Name = "Current Stock")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Please select a Category.")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        [Required(ErrorMessage = "Please select a Supplier.")]
        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier? Supplier { get; set; }
    }
}
