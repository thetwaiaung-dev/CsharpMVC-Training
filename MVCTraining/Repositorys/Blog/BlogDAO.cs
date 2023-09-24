using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using MVCTraining.DBHelper;
using MVCTraining.DTOs.BlogDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace MVCTraining.Repositorys.BlogRepository
{
    public class BlogDAO : IRepository<BlogDTO>
    {
        private readonly ConnectionString _connection;

        public BlogDAO(IOptions<ConnectionString> connection)
        {
            _connection = connection.Value;
        }

        public int Create(BlogDTO item)
        {
            throw new System.NotImplementedException();
        }

        public int Delete(long id)
        {
            throw new System.NotImplementedException();
        }

        public bool DuplicateCreate(BlogDTO item)
        {
            throw new System.NotImplementedException();
        }

        public bool DuplicateUpdate(BlogDTO item)
        {
            throw new System.NotImplementedException();
        }

        public List<BlogDTO> GetAll()
        {
            List<BlogDTO> blogList = new List<BlogDTO>();
            string query = "select * from Tbl_Blog";
            try
            {
                using (var con = new SqlConnection(_connection.SqlString))
                {
                    con.Open();
                    var cmd = con.CreateCommand();
                    cmd.CommandText = query;
                    using(SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            BlogDTO dto = new BlogDTO();
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

        public BlogDTO GetOne(long id)
        {
            throw new System.NotImplementedException();
        }

        public int Update(long id, BlogDTO item)
        {
            throw new System.NotImplementedException();
        }
    }
}
