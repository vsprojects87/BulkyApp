using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyApp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1,100,ErrorMessage ="Display Order must be between 1 and 100")]
        public int DisplayOrder { get; set; }
    }
    // above in display order we can tell how the database coloumn heading should be display
    // and in which name it should be display when we will use it in asp-for tag
}
