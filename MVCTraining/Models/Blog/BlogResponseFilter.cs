using System.Collections.Generic;
using MVCTraining.Dtos.Blog;

namespace MVCTraining.Models.Blog
{
    public class BlogResponseFilter : ResponseModel
    {
        public List<BlogDto> BlogList { get; set; }
    }
}
