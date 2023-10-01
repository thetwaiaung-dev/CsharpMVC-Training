using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace MvcTraining.Models
{
    public class RecipeModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage ="Please enter recipe title.")]
        [StringLength(200,ErrorMessage ="Title must be within 200 characters.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter recipe description.")]
        [StringLength(500,ErrorMessage ="Description must be within 500 characters.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter recipe instruction.")]
        [StringLength(500,ErrorMessage ="Instruction must be within 500 characters.")]
        public string Instruction { get; set;}
        [Required(ErrorMessage = "Please enter preparation time.")]
        [RegularExpression(@"^\d+(.+|)(Hour|Minute)$", ErrorMessage ="Please also enter Hour or Minute.")]
        public string PreparationTime {  get; set; }
        [Required(ErrorMessage ="Please enter cooking time.")]
        [RegularExpression(@"^\d+(.+|)(Hour|Minute)$", ErrorMessage = "Please also enter Hour or Minute.")]
        public string CookingTime {  get; set; }
        [Required(ErrorMessage ="Please enter author name")]
        [StringLength(45,ErrorMessage ="Author name must be within 45 characters.")]
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set;}
        [Required(ErrorMessage ="Please enter category name")]
        [StringLength(50,ErrorMessage ="Category name must be within 50 characters.")]
        public string Category { get;set; }
        [Required(ErrorMessage ="Choose photo for dish.")]
        public IFormFile PhotoUrl { get; set; }
        public string DishPhoto {  get; set; }  
    }

   
}
