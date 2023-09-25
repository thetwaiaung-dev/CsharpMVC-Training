using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using MvcTraining.Repositories.Blog;
using System;
using MvcTraining.Models;
using System.Data;
using MvcTraining.Exceptions;

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
            return View();
        }

        public IActionResult UpdateBlogView(int id)
        {
            BlogDto blogDto=_blogService.GetBlogById(id);
            BlogRequestModel blogRequest = ChangeModel.Change(blogDto);
            return View(blogRequest);
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
                TempData["ErrorMessage"] = "Data Field is required";
                return RedirectToAction("CreateBlog");
            }
            BlogDto blogDto = ChangeModel.Change(blogRequestModel);
            try
            {
                int saveResult = _blogService.SaveBlog(blogDto);
            }
            catch (DuplicateName ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("CreateBlog");
            }

            return RedirectToAction("Index");
        }

      

    }
}
