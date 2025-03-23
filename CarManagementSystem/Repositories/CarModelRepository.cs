using CarManagementSystem.DTOs;
using CarManagementSystem.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace CarManagementSystem.Repositories
{
    public class CarModelRepository : ICarModelRepository
    {
        private readonly string _connectionString;

        public CarModelRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<CarModel> GetAllCarModels()
        {
            List<CarModel> carModels = new List<CarModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"
                SELECT cm.*, 
                    ISNULL(STRING_AGG(cmi.ImagePath, ','), '') AS ImagePaths
                FROM CarModels cm
                LEFT JOIN CarModelImages cmi ON cm.Id = cmi.CarModelId
                GROUP BY cm.Id, cm.Brand, cm.Class, cm.ModelName, cm.ModelCode, cm.Description, 
                    cm.Features, cm.Price, cm.DateOfManufacturing, cm.IsActive, cm.SortOrder
                ORDER BY cm.DateOfManufacturing DESC, cm.SortOrder ASC;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            carModels.Add(new CarModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Brand = reader["Brand"].ToString(),
                                Class = reader["Class"].ToString(),
                                ModelName = reader["ModelName"].ToString(),
                                ModelCode = reader["ModelCode"].ToString(),
                                Description = reader["Description"].ToString(),
                                Features = reader["Features"].ToString(),
                                Price = Convert.ToDecimal(reader["Price"]),
                                DateOfManufacturing = Convert.ToDateTime(reader["DateOfManufacturing"]),
                                IsActive = Convert.ToBoolean(reader["IsActive"]),
                                SortOrder = Convert.ToInt32(reader["SortOrder"]),
                                Images = !string.IsNullOrEmpty(reader["ImagePaths"].ToString())
                                    ? reader["ImagePaths"].ToString().Split(',').ToList()
                                    : new List<string>()
                            });
                        }
                    }
                }
            }
            return carModels;
        }

        public List<CarModel> GetCarModels(string search, string orderBy)
        {
            List<CarModel> carModels = new List<CarModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string orderByColumn = orderBy switch
                {
                    "DateOfManufacturing" => "cm.DateOfManufacturing DESC",
                    "SortOrder" => "cm.SortOrder ASC",
                    _ => "cm.DateOfManufacturing DESC"
                };

                string query = $@"
                SELECT cm.*, 
                        ISNULL(STRING_AGG(cmi.ImagePath, ','), '') AS ImagePaths
                FROM CarModels cm
                LEFT JOIN CarModelImages cmi ON cm.Id = cmi.CarModelId
                WHERE cm.ModelName LIKE @search OR cm.ModelCode LIKE @search
                GROUP BY cm.Id, cm.Brand, cm.Class, cm.ModelName, cm.ModelCode, cm.Description, 
                        cm.Features, cm.Price, cm.DateOfManufacturing, cm.IsActive, cm.SortOrder
                ORDER BY {orderByColumn};";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@search", $"%{search}%");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            carModels.Add(new CarModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Brand = reader["Brand"].ToString(),
                                Class = reader["Class"].ToString(),
                                ModelName = reader["ModelName"].ToString(),
                                ModelCode = reader["ModelCode"].ToString(),
                                Description = reader["Description"].ToString(),
                                Features = reader["Features"].ToString(),
                                Price = Convert.ToDecimal(reader["Price"]),
                                DateOfManufacturing = Convert.ToDateTime(reader["DateOfManufacturing"]),
                                IsActive = Convert.ToBoolean(reader["IsActive"]),
                                SortOrder = Convert.ToInt32(reader["SortOrder"]),
                                Images = !string.IsNullOrEmpty(reader["ImagePaths"].ToString())
                                    ? reader["ImagePaths"].ToString().Split(',').ToList()
                                    : new List<string>()
                            });
                        }
                    }
                }
            }
            return carModels;
        }

        public int AddCarModel(CarModelDto model)
        {
            int newCarModelId = 0;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"
            INSERT INTO CarModels (Brand, Class, ModelName, ModelCode, Description, Features, Price, DateOfManufacturing, IsActive, SortOrder)
            VALUES (@Brand, @Class, @ModelName, @ModelCode, @Description, @Features, @Price, @DateOfManufacturing, @IsActive, @SortOrder);
            SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Brand", model.Brand);
                    cmd.Parameters.AddWithValue("@Class", model.Class);
                    cmd.Parameters.AddWithValue("@ModelName", model.ModelName);
                    cmd.Parameters.AddWithValue("@ModelCode", model.ModelCode);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@Features", model.Features);
                    cmd.Parameters.AddWithValue("@Price", model.Price);
                    cmd.Parameters.AddWithValue("@DateOfManufacturing", model.DateOfManufacturing);
                    cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                    cmd.Parameters.AddWithValue("@SortOrder", model.SortOrder);

                    newCarModelId = Convert.ToInt32(cmd.ExecuteScalar()); // Retrieve new ID
                }
            }

            return newCarModelId;
        }

        public void AddCarModelImage(int carModelId, List<IFormFile> images)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var image in images)
                {
                    string filePath = SaveImage(image);

                    string query = "INSERT INTO CarModelImages (CarModelId, ImagePath) VALUES (@CarModelId, @ImagePath)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CarModelId", carModelId);
                        cmd.Parameters.AddWithValue("@ImagePath", filePath);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private string SaveImage(IFormFile image)
        {
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }

            return "/images/" + uniqueFileName;
        }

        public async Task<bool> DeleteCarModelAsync(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM CarModels WHERE Id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
