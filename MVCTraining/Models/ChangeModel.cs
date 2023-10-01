using AutoMapper;
using MvcTraining.Repositories.Blog;
using MvcTraining.Repositories.Recipe;
using System.Reflection;

namespace MvcTraining.Models
{
    public static class ChangeModel
    {
        public static BlogDto Change(this BlogRequestModel model)
        {
            
            if (model == null) return null;
            return new BlogDto
            {
                Blog_Id = model.BlogId,
                Blog_Title = model.BlogTitle,
                Blog_Author = model.BlogAuthor,
                Blog_Content = model.BlogContent               
            };
        }
        
        public static BlogRequestModel Change(this BlogDto dto)
        {
            if(dto == null) return null;
            return new BlogRequestModel
            {
                BlogId = dto.Blog_Id,
                BlogTitle = dto.Blog_Title,
                BlogAuthor = dto.Blog_Author,
                BlogContent = dto.Blog_Content
            };
        }

        public static RecipeDto Change(this RecipeModel model)
        {
            if(model == null) return null;
            return new RecipeDto
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Instruction = model.Instruction,
                PreparationTime = model.PreparationTime,
                CookingTime = model.CookingTime,
                Author = model.Author,
                CreatedDate = model.CreatedDate,
                ModifiedDate = model.ModifiedDate,
                Category = model.Category,
                DishPhoto=model.DishPhoto,
                
            };
        }

        public static RecipeModel Change(this RecipeDto dto)
        {
            if(dto == null) return null;
            return new RecipeModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                Instruction = dto.Instruction,
                PreparationTime = dto.PreparationTime,
                CookingTime = dto.CookingTime,
                Author = dto.Author,
                CreatedDate = dto.CreatedDate,
                ModifiedDate = dto.ModifiedDate,
                Category = dto.Category,
                DishPhoto=dto.DishPhoto,
            };
        }

        public static IngredientDto Change(this IngredientModel model)
        {
            if(model == null) return null;
            return new IngredientDto
            {
                Id = model.Id,
                Name = model.Name,
                Quantity = model.Quantity,
                Unit = model.Unit,
            };
        }

        public static IngredientModel Change(this IngredientDto dto)
        {
            if (dto == null) return null;
            return new IngredientModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Quantity = dto.Quantity,
                Unit = dto.Unit,
            };
        }
      
    }

    //public class AutoMap
    //{
    //    public RecipeDto ChangeRecipe(RecipeModel model)
    //    {
    //        if (model == null) return null;
    //        var recipeDto = Mapper.Map<RecipeModel, RecipeDto>(model);
    //        return recipeDto;
    //    }
    //}
}
