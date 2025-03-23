using CarManagementSystem.Models;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace CarManagementSystem.Repositories
{
    public class SalesDataRepository : ISalesDataRepository
    {
        private readonly string _connectionString;

        public SalesDataRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void CreateSalesData(SalesData salesData)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO SalesData (SalesmanId, BrandId, ClassId, NumberOfCarsSold, ModelPrice) VALUES (@SalesmanId, @BrandId, @ClassId, @NumberOfCarsSold, @ModelPrice)", conn))
                {
                    cmd.Parameters.AddWithValue("@SalesmanId", salesData.SalesmanId);
                    cmd.Parameters.AddWithValue("@BrandId", salesData.BrandId);
                    cmd.Parameters.AddWithValue("@ClassId", salesData.ClassId);
                    cmd.Parameters.AddWithValue("@NumberOfCarsSold", salesData.NumberOfCarsSold);
                    cmd.Parameters.AddWithValue("@ModelPrice", salesData.ModelPrice);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public SalesData GetSalesDataById(int id)
        {
            SalesData salesData = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM SalesData WHERE Id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            salesData = new SalesData
                            {
                                Id = reader.GetInt32(0),
                                SalesmanId = reader.GetInt32(1),
                                BrandId = reader.GetInt32(2),
                                ClassId = reader.GetInt32(3),
                                NumberOfCarsSold = reader.GetInt32(4),
                                ModelPrice = reader.GetDecimal(5)
                            };
                        }
                    }
                }
            }

            return salesData;
        }

        public void UpdateSalesData(SalesData salesData)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE SalesData SET SalesmanId = @SalesmanId, BrandId = @BrandId, ClassId = @ClassId, NumberOfCarsSold = @NumberOfCarsSold, ModelPrice = @ModelPrice WHERE Id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", salesData.Id);
                    cmd.Parameters.AddWithValue("@SalesmanId", salesData.SalesmanId);
                    cmd.Parameters.AddWithValue("@BrandId", salesData.BrandId);
                    cmd.Parameters.AddWithValue("@ClassId", salesData.ClassId);
                    cmd.Parameters.AddWithValue("@NumberOfCarsSold", salesData.NumberOfCarsSold);
                    cmd.Parameters.AddWithValue("@ModelPrice", salesData.ModelPrice);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteSalesData(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM SalesData WHERE Id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Salesman> GetAllSalesman()
        {
            List<Salesman> salesmen = new List<Salesman>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Id, Name FROM Salesman", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            salesmen.Add(new Salesman
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return salesmen;
        }

        public List<Brand> GetAllBrands()
        {
            List<Brand> brands = new List<Brand>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Id, Name FROM CarBrand", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            brands.Add(new Brand
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                }
            }
            return brands;
        }

        public List<Class> GetAllClasses()
        {
            List<Class> classes = new List<Class>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Id, Name FROM CarClass", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            classes.Add(new Class
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return classes;
        }

    }
}
