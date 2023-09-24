using System.Collections.Generic;

namespace MvcTraining.Repositories.Blog
{
    public class BlogResponseFilter : ResponseModel
    {
        public List<BlogDto> BlogList { get; set; }
    }
}
