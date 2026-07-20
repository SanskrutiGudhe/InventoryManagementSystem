using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Supplier Name is required.")]
        [StringLength(100, ErrorMessage = "Supplier Name cannot exceed 100 characters.")]
        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid Phone Number format.")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; } = string.Empty;
    }
}
