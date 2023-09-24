using Microsoft.AspNetCore.Mvc;
using MVCTraining.Repositorys.Blog;

namespace MVCTraining.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogService _blogService;

        public BlogController(BlogService blogService)
        {
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            var blogList = _blogService.GetAllBlog();
            return View(blogList);
        }
    }
}
