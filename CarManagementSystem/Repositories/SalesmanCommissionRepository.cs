using CarManagementSystem.DTOs;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CarManagementSystem.Repositories
{
    public class SalesmanCommissionRepository : ISalesmanCommissionRepository
    {
        private readonly string _connectionString;

        public SalesmanCommissionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<SalesmanCommissionDto>> GetSalesDataAsync()
        {
            var salesData = new List<SalesmanCommissionDto>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                string query = @"
                SELECT 
                    s.Id AS SalesmanId, 
                    s.Name AS SalesmanName,
                    cb.Name AS Brand,
                    cb.FixedCommission,
                    cb.PriceThreshold,
                    cc.Name AS CarClass,
                    cc.CommissionPercentage,
                    sd.NumberOfCarsSold,
                    sd.ModelPrice,
                    s.LastYearTotalSales
                FROM SalesData sd
                INNER JOIN Salesman s ON sd.SalesmanId = s.Id
                INNER JOIN CarBrand cb ON sd.BrandId = cb.Id
                INNER JOIN CarClass cc ON sd.ClassId = cc.Id;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var salesmanId = Convert.ToInt32(reader["SalesmanId"]);
                            var salesmanName = reader["SalesmanName"].ToString();
                            var brand = reader["Brand"].ToString();
                            var carClass = reader["CarClass"].ToString();
                            var numberOfCarsSold = Convert.ToInt32(reader["NumberOfCarsSold"]);
                            var fixedCommission = Convert.ToDecimal(reader["FixedCommission"]);
                            var priceThreshold = Convert.ToDecimal(reader["PriceThreshold"]);
                            var commissionPercentage = Convert.ToDecimal(reader["CommissionPercentage"]);
                            var modelPrice = Convert.ToDecimal(reader["ModelPrice"]);
                            var lastYearTotalSales = Convert.ToDecimal(reader["LastYearTotalSales"]);

                            decimal totalFixedCommission = modelPrice > priceThreshold ? fixedCommission * numberOfCarsSold : 0;

                            decimal classCommission = (modelPrice * (commissionPercentage / 100)) * numberOfCarsSold;

                            decimal additionalCommission = 0;
                            if (lastYearTotalSales > 500000 && carClass == "A")
                            {
                                additionalCommission = (lastYearTotalSales * 2) / 100;
                            }

                            decimal totalCommission = totalFixedCommission + classCommission + additionalCommission;

                            decimal newTotalSales = lastYearTotalSales + totalCommission;

                            if (newTotalSales > lastYearTotalSales)
                            {
                                await UpdateLastYearTotalSalesAsync(salesmanId, newTotalSales, conn);
                            }

                            salesData.Add(new SalesmanCommissionDto
                            {
                                SalesmanId = salesmanId,
                                SalesmanName = salesmanName,
                                Brand = brand,
                                CarClass = carClass,
                                NumberOfCarsSold = numberOfCarsSold,
                                FixedCommission = fixedCommission,
                                TotalFixedCommission = totalFixedCommission,
                                ClassCommission = classCommission,
                                AdditionalCommission = additionalCommission,
                                TotalCommission = totalCommission
                            });
                        }
                    }
                }
            }

            return salesData;
        }

        private async Task UpdateLastYearTotalSalesAsync(int salesmanId, decimal newTotalSales, SqlConnection conn)
        {
            string updateQuery = @"
            UPDATE Salesman 
            SET LastYearTotalSales = @NewTotalSales
            WHERE Id = @SalesmanId AND LastYearTotalSales < @NewTotalSales";

            using (SqlConnection updateConn = new SqlConnection(_connectionString))
            {
                await updateConn.OpenAsync();

                using (SqlCommand updateCmd = new SqlCommand(updateQuery, updateConn))
                {
                    updateCmd.Parameters.Add("@NewTotalSales", SqlDbType.Decimal).Value = newTotalSales;
                    updateCmd.Parameters.Add("@SalesmanId", SqlDbType.Int).Value = salesmanId;

                    try
                    {
                        await updateCmd.ExecuteNonQueryAsync();
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Error updating last year's total sales", ex);
                    }
                }
            }
        }
    }
}
