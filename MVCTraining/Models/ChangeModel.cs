using MVCTraining.Dtos.Blog;
using MVCTraining.Models.Blog;

namespace MVCTraining.Models
{
    public static class ChangeModel
    {
        public static BlogDto Change(this BlogRequestModel model)
        {
            if (model == null) return null;
            return new BlogDto
            {
                Blog_Id = model.BlogId,
                Blog_Title = model.BlogTitle,
                Blog_Author = model.BlogAuthor,
                Blog_Content = model.BlogContent               
            };
        }
        
        public static BlogRequestModel Change(this BlogDto dto)
        {
            if(dto == null) return null;
            return new BlogRequestModel
            {
                BlogId = dto.Blog_Id,
                BlogTitle = dto.Blog_Title,
                BlogAuthor = dto.Blog_Author,
                BlogContent = dto.Blog_Content
            };
        }
    }
}
