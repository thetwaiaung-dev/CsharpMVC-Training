using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using MvcTraining.DbHelper;
using MvcTraining.Dtos.Blog;
using MvcTraining.Resources;

namespace MvcTraining.Repositories.Blog
{
    public class BlogDao : IRepository<BlogDto>
    {
        private readonly ConnectionString _connection;

        public BlogDao(IOptions<ConnectionString> connection)
        {
            _connection = connection.Value;
        }

        public int Create(BlogDto item)
        {
            throw new System.NotImplementedException();
        }

        public int Delete(long id)
        {
            throw new System.NotImplementedException();
        }

        public int DuplicateCreate(BlogDto item)
        {
            throw new System.NotImplementedException();
        }

        public int DuplicateUpdate(BlogDto item)
        {
            throw new System.NotImplementedException();
        }

        public int ListCount()
        {
            int count = 0;
            try
            {
                using (var con = new SqlConnection(_connection.DbConnection))
                {
                    con.Open();
                    var cmd = con.CreateCommand();
                    cmd.CommandText = SqlResources.BlogCount;
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            count = Convert.ToInt32(rd["Blog_Count"]);
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return count;
        }

        public int FilterListCount(string searchParam)
        {
            int count = 0;
            try
            {
                using (var con = new SqlConnection(_connection.DbConnection))
                {
                    con.Open();
                    var cmd = con.CreateCommand();
                    cmd.CommandText = SqlResources.BlogCount;
                    if (!String.IsNullOrEmpty(searchParam))
                    {
                        cmd.CommandText += searchParam;
                    }
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            count = Convert.ToInt32(rd["Blog_Count"]);
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return count;
        }

        public List<BlogDto> GetAll(string searchParam, string pagination)
        {
            List<BlogDto> blogList = new List<BlogDto>();
            try
            {
                using (var con = new SqlConnection(_connection.DbConnection))
                {
                    con.Open();
                    var cmd = con.CreateCommand();
                    cmd.CommandText = SqlResources.GetAllBlog;
                    if (!String.IsNullOrEmpty(searchParam))
                    {
                        cmd.CommandText += searchParam;
                    }
                    cmd.CommandText += pagination;
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            BlogDto dto = new BlogDto();
                            dto.Blog_Id = Convert.ToInt64(rd["Blog_Id"]);
                            dto.Blog_Title = rd["Blog_Title"].ToString();
                            dto.Blog_Author = rd["Blog_Author"].ToString();
                            dto.Blog_Content = rd["Blog_Content"].ToString();
                            blogList.Add(dto);
                        }
                    }
                    con.Close();
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return blogList;
        }

        public BlogDto GetOne(long id)
        {
            throw new System.NotImplementedException();
        }
        
        public int Update(long id, BlogDto item)
        {
            throw new System.NotImplementedException();
        }
    }
}
