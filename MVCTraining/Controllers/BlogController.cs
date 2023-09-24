using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using MvcTraining.Repositories.Blog;

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

        [HttpPost]
        public IActionResult GetAllBlog()
        {
            BlogDataRequestModel model = new BlogDataRequestModel();
            var requestData = GetFormRequest();
            model.DataTablesRequest = requestData;
            var blogList = _blogService.GetAllBlog(model.DataTablesRequest);
            return ToJson(requestData.Draw, blogList.RequestTotal, blogList.RequestFilter, blogList.BlogList);
        }
    }
}
