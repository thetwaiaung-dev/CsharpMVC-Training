using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using MvcTraining.Models;
using MvcTraining.Resources;
using System;
using System.Collections.Generic;

namespace MvcTraining.Repositories.Recipe
{
    public class RecipeDao : IRepository<RecipeDto>
    {
        public readonly ConnectionStringModel _connection;

        public RecipeDao(IOptions<ConnectionStringModel> connection)
        {
            _connection = connection.Value;
        }
        public long Create(RecipeDto item)
        {
            long result = 0;
            try
            {
                using(var con=new SqlConnection(_connection.DbConnection))
                {
                    con.Open();
                    var cmd=con.CreateCommand();
                    cmd.CommandText = SqlResources.SaveRecipe;
                    cmd.Parameters.AddWithValue("@title",item.Title);
                    cmd.Parameters.AddWithValue("@descript",item.Description);
                    cmd.Parameters.AddWithValue("@instruction",item.Instruction);
                    cmd.Parameters.AddWithValue("@prepareTime",item.PreparationTime);
                    cmd.Parameters.AddWithValue("@cookingTime",item.CookingTime);
                    cmd.Parameters.AddWithValue("@author",item.Author);
                    cmd.Parameters.AddWithValue("@createdDate",DateTime.Now);
                    cmd.Parameters.AddWithValue("@modifiedDate",DateTime.Now);
                    cmd.Parameters.AddWithValue("@category",item.Category);
                    cmd.Parameters.AddWithValue("@image", item.DishPhoto);
                     result=(long)(cmd.ExecuteScalar());
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public int Delete(long id)
        {
            throw new System.NotImplementedException();
        }

        public int DuplicateCreate(RecipeDto item)
        {
            throw new System.NotImplementedException();
        }

        public int DuplicateUpdate(RecipeDto item)
        {
            throw new System.NotImplementedException();
        }

        public int ListCount()
        {
            int result = 0;
            try
            {
                using (var con = new SqlConnection(_connection.DbConnection))
                {
                    con.Open();
                    var cmd = con.CreateCommand();
                    cmd.CommandText = SqlResources.RecipeCount;
                    result = (int)cmd.ExecuteScalar();

                    con.Close();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
            
        }

        public int FilterListCount(string searchParam)
        {
            int count = 0;
            try
            {
                using(var con=new SqlConnection(_connection.DbConnection))
                {
                    con.Open();
                    var cmd = con.CreateCommand();
                    cmd.CommandText = SqlResources.RecipeCount;
                    if (!String.IsNullOrEmpty(searchParam))
                    {
                        cmd.CommandText += searchParam;
                    }
                   count= (int)cmd.ExecuteScalar();
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return count;
        }

        public List<RecipeDto> GetAll(string searchParam, string pagination)
        {
            List<RecipeDto> recipeDtos = new List<RecipeDto>();
            try
            {
                using(var con=new SqlConnection(_connection.DbConnection))
                {
                    con.Open();
                    var cmd=con.CreateCommand();
                    cmd.CommandText = SqlResources.GetAllRecipe;
                    if (!String.IsNullOrEmpty(searchParam))
                    {
                        cmd.CommandText+= searchParam;
                    }
                    cmd.CommandText += pagination;
                   using(SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            RecipeDto dto = new RecipeDto();
                            dto.Title = rdr["title"].ToString();
                            dto.Description = rdr["descript"].ToString();
                            dto.Instruction = rdr["instruction"].ToString();
                            dto.Category = rdr["category"].ToString();
                            dto.Author = rdr["author"].ToString();
                            dto.PreparationTime = rdr["prepare_time"].ToString();
                            dto.CookingTime = rdr["cooking_time"].ToString();
                            dto.DishPhoto = rdr["dish_image"].ToString();
                            dto.CreatedDate = Convert.ToDateTime(rdr["created_date"]);
                            recipeDtos.Add(dto);
                        }
                    }
                    con.Close();
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return recipeDtos;
        }

        public RecipeDto GetById(long id)
        {
            throw new System.NotImplementedException();
        }



        public int Update(RecipeDto item)
        {
            throw new System.NotImplementedException();
        }

        int IRepository<RecipeDto>.Create(RecipeDto item)
        {
            throw new NotImplementedException();
        }
    }
}
