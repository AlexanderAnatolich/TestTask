using DAL.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using DAL.DataContext;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Transactions;
using System.ComponentModel;
using Model.Models;

namespace DAL.Repositories
{
    
    public class BookRepository
    {
        
        private string _connectionString;
        public BookRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<Book> CreateAsync(Book item)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.OpenAsync();

                    var sqlQuery = $@"INSERT INTO [dbo].[Books]
                        ([Title],[Author],[YearOfPublish],
                        [Price],[DateInsert],[PublishHouseId])
                        VALUES
                        @Title,@Author,@YearOfPublish,@Price,@DateInsert,@PublishHouseId";
                    await db.QueryAsync<int>(sqlQuery, item);                    
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
        public async Task<Boolean> CreateAsync(IEnumerable<Book> item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlTransaction transaction = connection.BeginTransaction();
                using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                {
                    bulkCopy.BatchSize = 100;
                    bulkCopy.DestinationTableName = "dbo.Books";
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
        public async Task<Book> FindByIdAsync(int id)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                await db.OpenAsync();

                var sqlQuery = $@"SELECT *
                                FROM [dbo].[Books]
                                WHERE [Id]= @id";
                var result = await db.QuerySingleAsync<Book>(sqlQuery, new
                {
                    id = id
                });

                return result;
            }
        }
        public async Task<IQueryable<BookViewModel>> GetAllAsync()
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    db.Open();
                    var sqlQuery = $@"SELECT *
                                FROM [dbo].[Books] 
                                LEFT JOIN [dbo].[BookGenerRelations] 
                                ON [dbo].[Books].[Id] = [dbo].[BookGenerRelations].[BookID]
                                LEFT JOIN [dbo].[Geners] 
                                ON [dbo].[BookGenerRelations].[GenreId] = [dbo].[Geners].[Id]
                                LEFT JOIN [dbo].[PublishHouses]
                                ON [dbo].[Books].[PublishHouseId] = [dbo].[PublishHouses].[Id]";
                    var invoiceDictionary = new Dictionary<int, BookViewModel>();
                    var queryResult = await db.QueryAsync<BookViewModel, GenerViewModel, PublishHouseViewModel, BookViewModel>(sqlQuery,
                        (book, gener, publishHouse) =>
                        {
                            BookViewModel model;
                            if (!invoiceDictionary.TryGetValue(book.Id, out model))
                            {
                                model = book;
                                model.Genre = new List<GenerViewModel>();
                                invoiceDictionary.Add(model.Id, model);
                            }
                            model.Genre.Add(gener);
                            model.PublishHouse = publishHouse;
                            return model;
                        }
                        );
                    var result = queryResult.Distinct().AsQueryable();
                    return result;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }
        public async Task<IEnumerable<Book>> GetByAuthorAsync(string author)
        {
            try
            { 
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.OpenAsync();

                    var sqlQuery = $@"SELECT *
                                    FROM [dbo].[Books]
                                    WHERE [Author]= @value";

                    var result = await db.QueryAsync<Book>(sqlQuery, new
                    {
                        value = author
                    });
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
                                      FROM [dbo].[Books]
                                      WHERE [Id]= @value";

                    await db.QueryAsync<Book>(sqlQuery, new
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
                                  FROM [dbo].[Books]
                                  WHERE [Id] IN @value";
                    await db.QueryAsync<Book>(sqlQuery, new {
                        value = ids
                    });
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
        public async Task UpdateAsync(Book item)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.OpenAsync();
                    var sqlQuery = $@"UPDATE[dbo].[Books]
                                    SET 
                                    [Title] = @Title,
                                    [Author] = @Author,
                                    [YearOfPublish] = @YearOfPublish,
                                    [Price] = @Price,
                                    [DateInsert] = @DateInsert,
                                    [PublishHouseId] = @PublishHouseId
                                    WHERE [Id]=@Id";
                    await db.ExecuteAsync(sqlQuery, item);
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