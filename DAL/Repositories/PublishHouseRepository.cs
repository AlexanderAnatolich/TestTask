using DAL.Models;
using Dapper;
using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PublishHouseRepository
    {
        private string _connectionString;
        public PublishHouseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<PublishHouse> CreateAsync(PublishHouse item)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.OpenAsync();

                    var sqlQuery = $@"INSERT INTO 
                                        [dbo].[PublishHouses]([House])
                                        VALUES @House";
                    await db.QueryAsync<PublishHouse>(sqlQuery, new
                    {
                        House = item.House
                    });
                }
                return item;
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }
        public async Task<PublishHouse> FindByIdAsync(int id)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                await db.OpenAsync();

                var sqlQuery = $@"SELECT *
                                FROM [dbo].[PublishHouses]
                                WHERE [Id]= @id";
                var result = await db.QuerySingleAsync<PublishHouse>(sqlQuery, new { id = id });

                return result;
            }
        }
        public async Task<IQueryable<PublishHouse>> GetAllAsync()
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.OpenAsync();
                    var sqlQuery = $@"SELECT *
                                FROM [dbo].[PublishHouses]";
                    var queryResult = await db.QueryAsync<PublishHouse>(sqlQuery);
                    var result = queryResult.AsQueryable();
                    return result;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }
        public async Task<Boolean> RemoveAsync(int item)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.OpenAsync();

                    var sqlQuery = $@"DELETE 
                                      FROM [dbo].[PublishHouses]
                                      WHERE [Id]= @value";
                    await db.QueryAsync(sqlQuery, new
                    {
                        value = item
                    });

                    return true;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }
        public async Task UpdateAsync(PublishHouse item)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.OpenAsync();
                    var sqlQuery = $@"UPDATE[dbo].[PublishHouses]
                                    SET 
                                    [Id] = @Id,
                                    [House] = @House,
                                    WHERE [Id]=@Id";
                    await db.ExecuteAsync(sqlQuery, new
                    {
                        Id = item.Id,
                        House = item.House
                    });
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }
    }
}