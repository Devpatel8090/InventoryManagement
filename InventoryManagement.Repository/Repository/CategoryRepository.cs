using InventoryManagement.Entities.Model;
using InventoryManagement.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InventoryManagement.Repository.Repository
{
    public class CategoryRepository : ICategoryRepository
    {

        /*private readonly string _connectionString;
        
        public CityRepository(string connectionString)
        {
             _connectionString = connectionString;
        }*/


        private readonly IDataAccessRepository _dataAccess;

        public CategoryRepository(IDataAccessRepository dataAccess)
        {

            _dataAccess = dataAccess;
        }

        public IEnumerable<Category> GetCategories()
        {

            /*using (var connection = new SqlConnection(_connectionString))*/
            using (var connection = _dataAccess.CreateConnection())
            {
                connection.Open();

                try
                {
                    SqlCommand command = new SqlCommand("[dbo].sp_INVCategory_GetAllCategories", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = command.ExecuteReader();

                    List<Category> CategoryList = new List<Category>();
                    Category category = null;

                    while (reader.Read())
                    {
                        category = new Category();
                        category.CategoryName = reader["CategoryName"].ToString();
                        category.Description = reader["Description"].ToString();
                        category.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                        /*city.CityName = Convert.ToString(reader["CityName"]);*/
                        CategoryList.Add(category);
                    }

                    return CategoryList;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error => ", ex.Message);
                    return null;
                }
                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }

        public int AddOrUpdateCategories(IncomeViewModel model)
        {
            /*using (var connection = new SqlConnection(_connectionString))*/
            using (var connection = _dataAccess.CreateConnection())
            {
                connection.Open();
                try
                {
                    if (model.Category.CategoryId == 0)
                    {
                        SqlCommand command = new SqlCommand("[dbo].sp_INVCategory_AddCategories", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("CategoryName", model.Category.CategoryName);
                        command.Parameters.AddWithValue("Description", model.Category.Description);
                        var data = command.ExecuteNonQuery();
                        return data;

                    }
                    else
                    {
                        SqlCommand command = new SqlCommand("[dbo].sp_INVCategory_UpdateCategory", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("categoryId", model.Category.CategoryId);
                        command.Parameters.AddWithValue("categoryName", model.Category.CategoryName);
                        command.Parameters.AddWithValue("description", model.Category.Description);
                        var data = command.ExecuteNonQuery();
                        return data;
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine("Error => ", e.Message);
                    return -1;
                }
                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }


        }
        public string EditCategory(long categoryId)
        {
            /*using (var connection = new SqlConnection(_connectionString))*/
            using (var connection = _dataAccess.CreateConnection())
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("[dbo].sp_INVCategory_GetCategoryById", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("categoryId", categoryId);
                    SqlDataReader reader = command.ExecuteReader();
                    Category category = null;

                    while (reader.Read())
                    {
                        category = new Category();
                        category.CategoryName = reader["CategoryName"].ToString();
                        category.Description = reader["Description"].ToString();
                        category.CategoryId = Convert.ToInt32(reader["CategoryId"]);

                    }
                    return Newtonsoft.Json.JsonConvert.SerializeObject(category);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error => ", e.Message);
                    return null;
                }
                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }


        public int DeleteCategory(long categoryId)
        {
            /*using (var connection = new SqlConnection(_connectionString))*/
            using (var connection = _dataAccess.CreateConnection())
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("[dbo].sp_INVCategory_DeleteCategoryById", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("categoryId", categoryId);
                    var affectedRows = command.ExecuteNonQuery();
                    return affectedRows;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error => ", e.Message);
                    return 0;
                }
                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }

        public async Task<IEnumerable<Category>> SearchCategory(string searchString)
        {
            /*var string = */
            /* using (var connection = _dataAccess.CreateConnection())
             {
                 connection.Open();*/
            try
            {
                var categories = await _dataAccess.GetData<Category, dynamic>("[dbo].sp_INVCategory_GetCategoriesBySearch", new { searchString });

                return categories;

            }
            catch (Exception e)
            {
                Console.WriteLine("Error => ", e.Message);
                return null;

            }
            /* finally
             {
                 if (connection != null && connection.State == ConnectionState.Open)
                 {
                     connection.Close();
                 }
             }
         }*/


        }

        public async Task<IEnumerable<Category>> CategoryPagination(long pageNo)
            {
                try
                {
                    var categories = await _dataAccess.GetData<Category, dynamic>("[dbo].sp_INVCategory_Pagination", new { pageNo });
                    return categories;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error => ", e.Message);
                    return null;

                }

            }

            public async Task<IEnumerable<Category>> SearchCategoryWithPagination(string searchString,int pageNo = 1,int pageSize = 3)
        {
           
            try
            {
                var categories = await _dataAccess.GetData<Category, dynamic>("[dbo].sp_INVCategory_GetCategoriesBySearchWithPagination", new { searchString,pageNo,pageSize });

                return categories;

            }
            catch (Exception e)
            {
                Console.WriteLine("Error => ", e.Message);
                return null;

            }
            /* finally
             {
                 if (connection != null && connection.State == ConnectionState.Open)
                 {
                     connection.Close();
                 }
             }
         }*/


        }

    }
}
