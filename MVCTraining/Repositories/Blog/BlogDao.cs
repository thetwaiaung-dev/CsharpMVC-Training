using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using MvcTraining.Exceptions;
using MvcTraining.Models;
using MvcTraining.Resources;

namespace MvcTraining.Repositories.Blog
{
    public class BlogDao : IRepository<BlogDto>
    {
        private readonly ConnectionStringModel _connection;

        public BlogDao(IOptions<ConnectionStringModel> connection)
        {
            _connection = connection.Value;
        }

        public int Create(BlogDto item)
        {
            int result = 0;
            int duplicateNameResult=DuplicateCreate(item);
            if(duplicateNameResult >0 ) 
            {
                throw new DuplicateName("This blog has already exists.");
            }
            try
            {
                using (var con = new SqlConnection(_connection.DbConnection))
                {
                    con.Open();
                    var cmd = con.CreateCommand();
                    cmd.CommandText = SqlResources.SaveBlog;
                    cmd.Parameters.AddWithValue("@BlogTitle", item.Blog_Title);
                    cmd.Parameters.AddWithValue("@BlogAuthor", item.Blog_Author);
                    cmd.Parameters.AddWithValue("@BlogContent", item.Blog_Content);
                    cmd.Parameters.AddWithValue("@IsDeleted", item.Is_Deleted);
                    
                    result=cmd.ExecuteNonQuery();

                    con.Close();

            }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;
           
        }

        public int Delete(long id)
        {
            int result = 0;
            try
            {
                using(var con=new SqlConnection(_connection.DbConnection))
                {
                    con.Open();
                    var cmd = con.CreateCommand();
                    cmd.CommandText = SqlResources.DeleteBlogBySort;
                    cmd.Parameters.AddWithValue("@IsDeleted", true);
                    cmd.Parameters.AddWithValue("@BlogId", id);

                    result = cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }

        public int DuplicateCreate(BlogDto item)
        {
            int result = 0;
            try
            {
                using(var con=new SqlConnection(_connection.DbConnection))
                {
                    con.Open();
                    var cmd = con.CreateCommand();
                    cmd.CommandText = SqlResources.DuplicateName;
                    cmd.Parameters.AddWithValue("@BlogTitle", item.Blog_Title);
                    cmd.Parameters.AddWithValue("@BlogAuthor", item.Blog_Author);
                    cmd.Parameters.AddWithValue("@IsDeleted", false);

                    result = (int)cmd.ExecuteScalar();
                    con.Close();
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

            return result;
        }

        public int DuplicateUpdate(BlogDto item)
        {
            int result = 0;
            try
            {
                using(var con=new SqlConnection(_connection.DbConnection))
                {
                    con.Open();
                    var cmd = con.CreateCommand();
                    cmd.CommandText= SqlResources.DuplicateUpdate;
                    cmd.Parameters.AddWithValue("@BlogTitle", item.Blog_Title);
                    cmd.Parameters.AddWithValue("@BlogAuthor", item.Blog_Author);
                    cmd.Parameters.AddWithValue("@BlogId", item.Blog_Id);
                    cmd.Parameters.AddWithValue("@IsDeleted", false);

                    result = (int)cmd.ExecuteScalar();
                    con.Close();

                }
            }
            catch( Exception e) 
            {
                throw new Exception (e.Message);
            }
            return result;
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

        public BlogDto GetById(long id)
        {
            BlogDto blogDto = new BlogDto();

            using(var con=new SqlConnection(_connection.DbConnection))
            {
                con.Open();
                var cmd = con.CreateCommand();
                cmd.CommandText = SqlResources.GetBlogById;
                cmd.Parameters.AddWithValue("@BlogId", id);
                using(SqlDataReader rd = cmd.ExecuteReader()) 
                {
                    while (rd.Read())
                    {
                        blogDto.Blog_Id = Convert.ToInt64(rd["Blog_Id"]);
                        blogDto.Blog_Title = rd["Blog_Title"].ToString();
                        blogDto.Blog_Author = rd["Blog_Author"].ToString();
                        blogDto.Blog_Content = rd["Blog_Content"].ToString();
                    }
                }
                con.Close();
            }

            return blogDto;
        }
        
        public int Update(BlogDto item)
        {
            int duplicateNameResult = DuplicateUpdate(item);
            if (duplicateNameResult > 0)
            {
                throw new DuplicateName("This blog has already existed.");
            }

            int result = 0;
            try
            {
                using (var con = new SqlConnection(_connection.DbConnection))
                {
                    con.Open();
                    var cmd = con.CreateCommand();
                    cmd.CommandText = SqlResources.UpdateBlog;
                    cmd.Parameters.AddWithValue("@BlogTitle", item.Blog_Title);
                    cmd.Parameters.AddWithValue("@BlogAuthor", item.Blog_Author);
                    cmd.Parameters.AddWithValue("@BlogContent", item.Blog_Content);
                    cmd.Parameters.AddWithValue("@BlogId", item.Blog_Id);

                    result = cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
           
        }
    }
}
