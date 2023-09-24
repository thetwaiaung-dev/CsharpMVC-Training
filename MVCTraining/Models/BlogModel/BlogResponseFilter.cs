using MVCTraining.DTOs.BlogDTO;
using System.Collections.Generic;

namespace MVCTraining.Models.BlogModel
{
    public class BlogResponseFilter : ResponseModel
    {
        public List<BlogDTO> BlogList { get; set; }
    }
}
