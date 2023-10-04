using System.Collections.Generic;

namespace MvcTraining.Repositories.Recipe
{
    public class IngredientService
    {
        private readonly IngredientDao _ingredientDao;

        public IngredientService(IngredientDao ingredientDao)
        {
            _ingredientDao = ingredientDao;
        }

        public int SaveIngredient(IngredientDto dto,long recipeId)
        {
            int result = _ingredientDao.Create(dto, recipeId);
            return result;
        }

        public List<IngredientDto>  GetIngredientByRecipeId(long recipeId)
        {

            List<IngredientDto> dtoList = _ingredientDao.GetIngredientByRecipeId(recipeId);
            return dtoList;
        }

        public int Delete(long id)
        {
            int result=_ingredientDao.Delete(id);
            return result;
        }
    }
}
