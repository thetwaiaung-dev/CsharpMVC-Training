using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCTraining.DTOs.BlogDTO
{
    [Table("Tbl_Blog")]
    public class BlogDTO
    {
        [Key]
        public long Blog_Id { get; set; }
        public string Blog_Title { get; set; }
        public string Blog_Author { get; set; }
        public string Blog_Content { get; set; }
    }
}
