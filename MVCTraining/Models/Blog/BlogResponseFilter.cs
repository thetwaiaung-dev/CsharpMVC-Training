using System.Collections.Generic;
using MvcTraining.Dtos.Blog;

namespace MvcTraining.Models.Blog
{
    public class BlogResponseFilter : ResponseModel
    {
        public List<BlogDto> BlogList { get; set; }
    }
}
