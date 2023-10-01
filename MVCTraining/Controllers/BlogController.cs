using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using MvcTraining.Repositories.Blog;
using System;
using MvcTraining.Models;
using System.Data;
using MvcTraining.Exceptions;
using MvcTraining.Repositories.Recipe;

namespace MvcTraining.Controllers
{
    public class BlogController : BaseController
    {
        private readonly BlogService _blogService;

        public BlogController(BlogService blogService)
        {
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            string successMessage = TempData["SuccessMessage"] as string;
            ViewData["SuccessMessage"]=successMessage;
            return View();
        }   

        [HttpPost]
        public IActionResult GetAllBlog()
        {
            BlogDataRequestModel model = new BlogDataRequestModel();
            var requestData = GetFormRequest();
            model.DataTablesRequest = requestData;
            var blogList = _blogService.GetAllBlog(model.DataTablesRequest);
            return ToJson(requestData.Draw, blogList.RequestTotal, blogList.RequestFilter, blogList.BlogList);
        }

        public IActionResult CreateBlog()
        {

            string errorMessage = TempData["ErrorMessage"] as string;
            BlogRequestModel requestModel = new BlogRequestModel();
            ViewData["ErrorMessage"] = errorMessage;
            return View(requestModel);
        }

        [HttpPost]
        public IActionResult SaveBlog(BlogRequestModel blogRequestModel  )
        {
            if (blogRequestModel.BlogTitle == null || blogRequestModel.BlogAuthor == null || blogRequestModel.BlogContent == null)
            {
                TempData["ErrorMessage"] = "Blog Field is required";
                return RedirectToAction("CreateBlog");
            }
            BlogDto blogDto = ChangeModel.Change(blogRequestModel);
            try
            {
                long saveResult = _blogService.SaveBlog(blogDto);
            }
            catch (DuplicateName ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return View("CreateBlog",blogRequestModel);
            }
            TempData["SuccessMessage"] = "Blog is successfully created.";
            return RedirectToAction("Index");
        }

        public IActionResult UpdateBlogView(int id)
        {
            //to catch error messgage
            string errorMessage = TempData["ErrorMessage"] as string;
            ViewData["ErrorMessage"] = errorMessage;

            BlogDto blogDto = _blogService.GetBlogById(id);
            BlogRequestModel blogRequest = ChangeModel.Change(blogDto);
            return View(blogRequest);
        }

        [HttpPost]
        public IActionResult UpdateBlog(BlogRequestModel blogRequestModel)
        {
            if (blogRequestModel.BlogTitle == null || blogRequestModel.BlogAuthor == null || blogRequestModel.BlogContent == null)
            {
                ViewData["ErrorMessage"] = "Blog Field is required";
                return View("UpdateBlogView",blogRequestModel);
            }
            BlogDto blogDto= ChangeModel.Change(blogRequestModel);
            try
            {
                int updateResult=_blogService.UpdateBlog(blogDto);
            }catch  (DuplicateName ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return View("UpdateBlogView",blogRequestModel);
            }
            TempData["SuccessMessage"] = "Blog is successfully updated.";
            return RedirectToAction("Index");
        }

        public IActionResult DeleteBlog(int id)
        {
            int result=_blogService.DeleteBlog(id);
            if(result > 0)
            {
                TempData["SuccessMessage"] = "Blog is successfully deleted";
                
            }
            else
            {
                TempData["SuccessMessage"] = "Delete fail."; 
            }
            return RedirectToAction("Index");
        }

    }
}
