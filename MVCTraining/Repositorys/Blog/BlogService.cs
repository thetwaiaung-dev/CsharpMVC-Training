using Microsoft.Extensions.DependencyInjection;
using MVCTraining.DTOs.BlogDTO;
using MVCTraining.Models.BlogModel;
using MVCTraining.Models.ChangeModel;
using MVCTraining.Repositorys.BlogRepository;
using System.Collections.Generic;
using System.Linq;

namespace MVCTraining.Repositorys.Blog
{
    public class BlogService
    {
        private readonly BlogDAO _blogDAO;

        public BlogService(BlogDAO blogDAO)
        {
            _blogDAO = blogDAO;
        }

        public List<BlogRequestModel> GetAllBlog()
        {
            List<BlogRequestModel> blogRequestModels = new List<BlogRequestModel>();
            var blogList = _blogDAO.GetAll();
            blogRequestModels.AddRange(blogList.Where(x => x.Blog_Id != 0).Select(blogList => blogList.Change()).ToList());
            return blogRequestModels;
        }
    }
}
