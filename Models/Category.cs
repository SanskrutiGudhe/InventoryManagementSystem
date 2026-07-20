using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category Name is required.")]
        [StringLength(50, ErrorMessage = "Category Name cannot exceed 50 characters.")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; } = string.Empty;
    }
}
