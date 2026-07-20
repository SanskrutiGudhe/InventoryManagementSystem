using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models
{
    public class Purchase
    {
        [Key]
        public int PurchaseId { get; set; }

        [Required(ErrorMessage = "Please select a Product.")]
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 100000, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Purchase Date is required.")]
        [Display(Name = "Purchase Date")]
        [DataType(DataType.Date)]
        public DateTime PurchaseDate { get; set; } = DateTime.Today;
    }
}
