using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcTraining.Repositories.Recipe
{
    [Table("Tbl_Ingredient")]
    public class IngredientDto
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("name")]    
        public string Name { get; set; }
        [Column("quantity")]
        public short Quantity {  get; set; }
        [Column("unit")]
        public string Unit {  get; set; }
    }
}
