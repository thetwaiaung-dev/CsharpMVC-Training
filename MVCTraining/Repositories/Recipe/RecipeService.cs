using MvcTraining.Models;
using System;

namespace MvcTraining.Repositories.Recipe
{
    public class RecipeService
    {
        private readonly RecipeDao _recipeDao;
        public RecipeService(RecipeDao recipeDao)
        {
            _recipeDao = recipeDao;
        }
        public long SaveRecipe(RecipeDto recipe)
        {
            long result=_recipeDao.Create(recipe);
            return result;
        }

        public RecipeResponseFilter GetAll(DataTablesRequest request)
        {
            RecipeResponseFilter recipeFilter = new RecipeResponseFilter();
            string searchParam = string.Empty;
            string sortColumnParam = string.Empty;
            string pagination = string.Empty;
            string sortColumn = request.SortColumn.Trim();
            string sortColumnDirection = request.SortColumnDirection.Trim();
            string search = request.Search.Trim();
            string pageStartAndSize = " OFFSET " + request.Start + " ROWS FETCH NEXT " + request.Length + " ROWS ONLY ";
            if (!string.IsNullOrEmpty(search))
            {
                searchParam = " and title like '%" + search + "%' or author like '%" + search + "%'";
            }
            if (!string.IsNullOrEmpty(sortColumn))
            {
                switch (sortColumn)
                {
                    case "title":
                        sortColumnParam = " order by title  "+sortColumnDirection;
                        break;
                    case "author":
                        sortColumnParam = " order by author  "+sortColumnDirection;
                        break;
                    default:
                        sortColumnParam = " order by id desc ";
                        break;
                }
            }
            pagination = sortColumnParam + pageStartAndSize;
            var recipeList=_recipeDao.GetAll(searchParam, pagination);
            int recipeCount = _recipeDao.ListCount();
            int recipeFilterCount = _recipeDao.FilterListCount(searchParam);
            recipeFilter.recipes=recipeList;
            recipeFilter.RequestFilter = recipeFilterCount;
            recipeFilter.RequestTotal = recipeCount;

            return recipeFilter;
        }

        public RecipeDto GetById(long id)
        {
            RecipeDto dto=_recipeDao.GetById(id);
            return dto;
        }
    }
}
