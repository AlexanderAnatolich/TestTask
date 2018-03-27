using DAL.Models;
using DAL.DataContext;
using System.Data.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using Model.Models;
using System;
using System.Linq;

namespace DAL.Repositories
{
    public class GenerRepository
    {
        private string _connectionString;
        public GenerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<Gener> CreateAsync(Gener item)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.OpenAsync();

                    var sqlQuery = $@"INSERT INTO 
                                        [dbo].[Geners]([Genre])
                        VALUES
                        @Genre";
                    await db.QueryAsync<Gener>(sqlQuery, new
                    {
                        Genre=item.Genre
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
        public async Task<Gener> FindByIdAsync(int id)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                await db.OpenAsync();

                var sqlQuery = $@"SELECT *
                                FROM [dbo].[Geners]
                                WHERE [Id]= @id";
                var result = await db.QuerySingleAsync<Gener>(sqlQuery, new { id = id });

                return result;
            }
        }
        public async Task<IQueryable<Gener>> GetAllAsync()
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.OpenAsync();
                    var sqlQuery = $@"SELECT *
                                FROM [dbo].[Geners]";
                    var queryResult = await db.QueryAsync<Gener>(sqlQuery);
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
                                      FROM [dbo].[Geners]
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
        public async Task UpdateAsync(Gener item)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.OpenAsync();
                    var sqlQuery = $@"UPDATE[dbo].[Geners]
                                    SET 
                                    [Id] = @Id,
                                    [Genre] = @Genre,
                                    WHERE [Id]=@Id";
                    await db.ExecuteAsync(sqlQuery, new
                    {
                        Id = item.Id,
                        Genre = item.Genre
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