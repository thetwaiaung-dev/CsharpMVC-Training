using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcTraining.Repositories.Blog
{
    [Table("Tbl_Blog")]
    public class BlogDto
    {
        [Key]
        public long Blog_Id { get; set; }
        public string Blog_Title { get; set; }
        public string Blog_Author { get; set; }
        public string Blog_Content { get; set; }
        public bool Is_Deleted { get; set; }
    }
}
