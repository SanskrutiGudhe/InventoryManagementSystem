using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models
{
    public class Sales
    {
        [Key]
        public int SalesId { get; set; }

        [Required(ErrorMessage = "Please select a Product.")]
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 100000, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Sales Date is required.")]
        [Display(Name = "Sales Date")]
        [DataType(DataType.Date)]
        public DateTime SalesDate { get; set; } = DateTime.Today;
    }
}
