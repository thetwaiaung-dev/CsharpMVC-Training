using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using MvcTraining.Models;
using MvcTraining.Resources;
using System;
using System.Collections.Generic;

namespace MvcTraining.Repositories.Recipe
{
    public class IngredientDao : IRepository<IngredientDto>
    {
        private readonly ConnectionStringModel _connection;

        public IngredientDao(IOptions<ConnectionStringModel> connection)
        {
            _connection=connection.Value;
        }
        public int Create(IngredientDto item,long recipeId)
        {
            int result = 0;
            try
            {
                using(var con=new SqlConnection(_connection.DbConnection))
                {
                    con.Open();
                    var cmd=con.CreateCommand();
                    cmd.CommandText = SqlResources.SaveIngredient;
                    cmd.Parameters.AddWithValue("@name", item.Name);
                    cmd.Parameters.AddWithValue("@quantity", item.Quantity);
                    cmd.Parameters.AddWithValue("@unit", item.Unit);
                    cmd.Parameters.AddWithValue("@recipeId", recipeId);

                    result = cmd.ExecuteNonQuery();
                }
            }catch(Exception ex)
            {
                var con = new SqlConnection(_connection.DbConnection);
                con.Open() ;
                var cmd= con.CreateCommand();
                //if error have,to delete previous ingredients that inserted 
                cmd.CommandText = SqlResources.DeleteIngredient;
                cmd.Parameters.AddWithValue("@recipeId", recipeId);
                cmd.ExecuteNonQuery();
                //if error have, delete recipe
                cmd.CommandText = SqlResources.DeleteRecipe;
                cmd.Parameters.AddWithValue("@id", recipeId);
                cmd.ExecuteNonQuery();
                con.Close();
                throw new Exception(ex.Message);
            }
            return result;
        }

        public int Create(IngredientDto item)
        {
            throw new NotImplementedException();
        }

        public List<IngredientDto> GetIngredientByRecipeId(long recipeId)
        {
            List<IngredientDto> dtoList=new List<IngredientDto>();
            try
            {
                using(var con=new SqlConnection(_connection.DbConnection))
                {
                    con.Open() ;
                    var cmd=con.CreateCommand();
                    cmd.CommandText = SqlResources.GetIngredientsByRecipeId;
                    cmd.Parameters.AddWithValue("@recipeId", recipeId);
                    using(SqlDataReader rd=cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            IngredientDto dto=new IngredientDto();
                            dto.Id = Convert.ToInt64(rd["id"]);
                            dto.Name = rd["name"].ToString();
                            dto.Quantity = Convert.ToInt16(rd["quantity"]);
                            dto.Unit = rd["unit"].ToString();
                            dtoList.Add(dto);
                        }
                    }

                    con.Close();
                }
            }catch( Exception ex )
            {
                throw new Exception(ex.Message);
            }
            return dtoList;
        }

        public int Delete(long id)
        {
            int result = 0;
            try
            {
                using(var con=new SqlConnection(_connection.DbConnection))
                {
                    con.Open() ;
                    var cmd=con.CreateCommand();
                    cmd.CommandText = SqlResources.DeleteSortIngredient;
                    cmd.Parameters.AddWithValue("IsDeleted", true);
                    cmd.Parameters.AddWithValue("id", id);
                    result=cmd.ExecuteNonQuery();

                    con.Close();
                }
            }catch(Exception ex)
            {
                throw new Exception (ex.Message);
            }
            return result;
        }

        public int DuplicateCreate(IngredientDto item)
        {
            throw new System.NotImplementedException();
        }

        public int DuplicateUpdate(IngredientDto item)
        {
            throw new System.NotImplementedException();
        }

        public int FilterListCount(string searchParam)
        {
            throw new System.NotImplementedException();
        }

        public List<IngredientDto> GetAll(string searchParam, string pagination)
        {
            throw new System.NotImplementedException();
        }

        public IngredientDto GetById(long id)
        {
            throw new System.NotImplementedException();
        }

        public int ListCount()
        {
            throw new System.NotImplementedException();
        }

        public int Update(IngredientDto item)
        {
            throw new System.NotImplementedException();
        }
    }
}
