using DAL.Models;
using Dapper;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class NewsPapersRepository
    {
        private string _connectionString;
        public NewsPapersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<NewsPaper> CreateAsync(NewsPaper item)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.OpenAsync();

                    var sqlQuery = $@"INSERT INTO [dbo].[NewsPapers]
                                   ([Title]
                                   ,[Price]
                                   ,[DateInsert]
                                   ,[PublishHouseId]
                                   ,[PrintDate])
                        VALUES
                        @Title,@Price,@DateInsert,@PublishHouseId,@PrintDate";
                    await db.QueryAsync<NewsPaper>(sqlQuery, new
                    {
                        Title = item.Title,
                        Price = item.Price,
                        DateInsert = item.DateInsert,
                        PublishHouseId= item.PublishHouseId,
                        PrintDate = item.PrintDate                       
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
        public async Task<Boolean> CreateAsync(IEnumerable<NewsPaper> item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlTransaction transaction = connection.BeginTransaction();
                using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                {
                    bulkCopy.BatchSize = 100;
                    bulkCopy.DestinationTableName = "dbo.NewsPapers";
                    try
                    {
                        await bulkCopy.WriteToServerAsync(item.AsDataTable());
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        connection.Close();
                    }
                }
                transaction.Commit();
            }
            return true;
        }
        public async Task<NewsPaper> FindByIdAsync(int id)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                await db.OpenAsync();

                var sqlQuery = $@"SELECT *
                                FROM [dbo].[NewsPapers]
                                WHERE [Id]= @id";
                var result = await db.QuerySingleAsync<NewsPaper>(sqlQuery, new { id=id });

                return result;
            }
        }
        public async Task<IQueryable<NewsPaperViewModel>> GetAllAsync()
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.OpenAsync();
                    var sqlQuery = $@"SELECT *
                                FROM [dbo].[NewsPapers] 
                                LEFT JOIN [dbo].[PublishHouses]
                                ON [dbo].[NewsPapers].[PublishHouseId] = [dbo].[PublishHouses].[Id]";
                    var queryResult = await db.QueryAsync<NewsPaperViewModel, PublishHouseViewModel, NewsPaperViewModel>(sqlQuery,
                        (book, publishHouse) =>
                        {
                            book.PublishHouse = publishHouse;
                            book.PublishHouse_Id = publishHouse.Id;
                            return book;
                        }
                        );
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
                                      FROM [dbo].[NewsPapers]
                                      WHERE [Id]= @value";

                    await db.QueryAsync<NewsPaper>(sqlQuery, new
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
        public async Task<Boolean> RemoveAsync(List<int> ids)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.OpenAsync();
                    var sqlQuery = $@"DELETE 
                                  FROM [dbo].[NewsPapers]
                                  WHERE [Id] IN @value";
                    await db.QueryAsync<NewsPaper>(sqlQuery, ids);
                    return true;
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
        public async Task UpdateAsync(NewsPaper item)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.OpenAsync();
                    var sqlQuery = $@"UPDATE [dbo].[NewsPapers]
                                    SET 
                                    [Title] = @Title,
                                    [Price] = @Price,
                                    [DateInsert] = @DateInsert,
                                    [PublishHouseId] = @PublishHouseId,
                                    [PrintDate] = @PrintDate
                                    WHERE [Id]=@Id";
                    await db.QueryAsync(sqlQuery, item);
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