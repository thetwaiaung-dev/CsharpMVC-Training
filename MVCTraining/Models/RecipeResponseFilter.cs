using MvcTraining.Repositories.Recipe;
using System.Collections.Generic;

namespace MvcTraining.Models
{
    public class RecipeResponseFilter: ResponseModel
    {
        public List<RecipeDto> recipes {  get; set; }
    }
}
