using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MvcTraining.Models;
using MvcTraining.Repositories.Recipe;
using System;
using System.IO;

namespace MvcTraining.Controllers
{
    public class RecipeController : BaseController
    {
        private readonly RecipeService _recipeService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RecipeController(RecipeService recipeService,
                                IWebHostEnvironment webHostEnvironment)
        {
            _recipeService = recipeService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult GetAllView()
        {
            return View();
        }
        public IActionResult Create()
        {
            RecipeModel recipe = new RecipeModel();
            return View(recipe);
        }

        [HttpPost]
        public IActionResult Save(RecipeModel recipe)
        {
            if (ModelState.IsValid)
            {
                string folder = "dish_photo/";
                folder += Guid.NewGuid().ToString() + "_" + recipe.PhotoUrl.FileName;
                string serverFolder=Path.Combine(_webHostEnvironment.WebRootPath, folder);

                recipe.DishPhoto = "/" + folder; 

                RecipeDto recipeDto = ChangeModel.Change(recipe);
                long saveResult=_recipeService.SaveRecipe(recipeDto);

                recipe.PhotoUrl.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                TempData["SuccessMessage"] = "Saved successful";
                return RedirectToAction("Index","Blog");
            }
            return View("Create",recipe);
        }

        [HttpPost]
        public IActionResult GetAll()
        {
            var requestData = GetFormRequest();
            var recipeList=_recipeService.GetAll(requestData);
            return ToJson(requestData.Draw,recipeList.RequestTotal,recipeList.RequestFilter,recipeList.recipes);
        }
    }
}
