using DAL.DTO;
using DAL.Models;
using Dapper;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class JournalRepository
    {
        private string _connectionString;
        public JournalRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<Journal> CreateAsync(Journal item)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.OpenAsync();

                    var sqlQuery = $@"INSERT INTO [dbo].[Journals]
                                   ([Title]
                                   ,[YearOfPublish]
                                   ,[Price]
                                   ,[DateInsert]
                                   ,[PublishHouseId])
                        VALUES
                        (@Title,@YearOfPublish,@Price,@DateInsert,@PublishHouseId)";
                    var result = await db.QueryAsync<Journal>(sqlQuery, new
                    {
                        Title = item.Title,
                        YearOfPublish = item.YearOfPublish,
                        Price = item.Price,
                        DateInsert = item.DateInsert,
                        PublishHouseId = item.PublishHouseId
                    });
                    return result.First();
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
        public async Task<Boolean> CreateAsync(IEnumerable<Journal> item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlTransaction transaction = connection.BeginTransaction();
                using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                {
                    bulkCopy.BatchSize = 100;
                    bulkCopy.DestinationTableName = "dbo.Journals";
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
        public async Task<JournalDTO> FindByIdAsync(int id)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                await db.OpenAsync();
                var sqlQuery = $@"SELECT *
                                FROM [dbo].[Journals] 
                                LEFT JOIN [dbo].[PublishHouses]
                                ON [dbo].[Journals].[PublishHouseId] = [dbo].[PublishHouses].[Id]";
                var queryResult = await db.QueryAsync<JournalDTO, PublishHouse, JournalDTO>(sqlQuery,
                    (book, publishHouse) =>
                    {
                        book.PublishHouse = publishHouse;
                        book.PublishHouseId = publishHouse.Id;
                        return book;
                    }
                    );
                var result = queryResult.FirstOrDefault();
                return result;
            }
        }
        public async Task<IEnumerable<JournalViewModel>> GetAllAsync()
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.OpenAsync();
                    var sqlQuery = $@"SELECT *
                                FROM [dbo].[Journals] 
                                LEFT JOIN [dbo].[PublishHouses]
                                ON [dbo].[Journals].[PublishHouseId] = [dbo].[PublishHouses].[Id]";
                    var queryResult = await db.QueryAsync<JournalViewModel, PublishHouseViewModel, JournalViewModel>(sqlQuery,
                        (book, publishHouse) =>
                        {
                            book.PublishHouse = publishHouse;
                            book.PublishHouseId = publishHouse.Id;
                            return book;
                        }
                        );
                    return queryResult;
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
                                      FROM [dbo].[Journals]
                                      WHERE [Id]= @value";

                    await db.QueryAsync<Journal>(sqlQuery, new
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
        public async Task<IEnumerable<JournalDTO>> FirndByTitleAsync(string title)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.OpenAsync();
                    var sqlQuery = $@"SELECT* FROM[dbo].[Journals]
                                      LEFT JOIN[dbo].[PublishHouses]
                                      ON[dbo].[Journals].[PublishHouseId] = [dbo].[PublishHouses].[Id]
                                      WHERE [dbo].[Journals].[Title] LIKE @value";
                    var invoiceDictionary = new Dictionary<int, JournalDTO>();
                    var queryResult = await db.QueryAsync<JournalDTO, PublishHouse, JournalDTO>(sqlQuery,
                    (book, publishHouse) =>
                    {
                        book.PublishHouse = publishHouse;
                        book.PublishHouseId = publishHouse.Id;
                        return book;
                    }, new { value = '%' + title + '%' });

                    var result = queryResult.Distinct();
                    return result;
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
                                  FROM [dbo].[Journals]
                                  WHERE [Id] IN @value";
                    await db.QueryAsync<Journal>(sqlQuery, ids);
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
        public async Task UpdateAsync(Journal item)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.OpenAsync();
                    var sqlQuery = $@"UPDATE [dbo].[Journals]
                                    SET 
                                    [Title] = @Title,
                                    [YearOfPublish] = @YearOfPublish,
                                    [Price] = @Price,
                                    [DateInsert]=@DateInsert,
                                    [PublishHouseId] = @PublishHouseId,
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
