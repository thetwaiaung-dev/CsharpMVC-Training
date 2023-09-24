using System.Collections.Generic;
using MvcTraining.Models;

namespace MvcTraining.Repositories.Blog
{
    public class BlogResponseFilter : ResponseModel
    {
        public List<BlogDto> BlogList { get; set; }
    }
}
