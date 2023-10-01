using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcTraining.Repositories.Recipe
{
    [Table("Tbl_Recipe")]
    public class RecipeDto
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("title")]
        public string Title {  get; set; }
        [Column("descript")]
        public string Description { get; set; }
        [Column("instruction")]
        public string Instruction {  get; set; }
        [Column("prepare_time")]
        public string PreparationTime { get; set; }
        [Column("cooking_time")]
        public string CookingTime {  get; set; }
        [Column("author")]
        public string Author {  get; set; }
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }
        [Column("modified_date")]
        public DateTime ModifiedDate {  get; set; }
        [Column("category")]
        public string Category {  get; set; }
        [Column("dish_photo")]
        public string DishPhoto { get; set; }
    }
}
