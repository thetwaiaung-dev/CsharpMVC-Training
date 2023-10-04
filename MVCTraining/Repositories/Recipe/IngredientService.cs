using System.Collections.Generic;
using System.Threading.Tasks;

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

        public int Update(IngredientDto dto)
        {
            int result=_ingredientDao.Update(dto);
            return result;
        }

        public IngredientDto GetById(long id)
        {
            IngredientDto dto=_ingredientDao.GetById(id);
            return dto;
        }

        public async Task<IngredientDto> GetByIdAsync(long id)
        {
            IngredientDto dto=await _ingredientDao.GetByIdAsync(id);

            return dto;
        }
    }
}
