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
    }
}
