using DAL.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class BookGenerRelationsRepository
    {
        private string _connectionString;
        public BookGenerRelationsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<BookGenerRelations> CreateAsync(BookGenerRelations item)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.OpenAsync();

                    var sqlQuery = $@"INSERT INTO [dbo].[BookGenerRelations]
                        ([BookId],[GenreId])
                        VALUES
                        @BookId,@GenreId";
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
        public async Task<Boolean> CreateAsync(IEnumerable<BookGenerRelations> item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlTransaction transaction = connection.BeginTransaction();
                using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                {
                    bulkCopy.BatchSize = 100;
                    bulkCopy.DestinationTableName = "dbo.BookGenerRelations";
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
        public async Task<IEnumerable<BookGenerRelations>> FindByBookId(int id)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                await db.OpenAsync();
                var sqlQuery = $@"SELECT [BookId],[GenreId]
                                FROM [dbo].[BookGenerRelations]
                                WHERE [BookId]= @id";
                var result = await db.QueryAsync<BookGenerRelations>(sqlQuery, id);
                return result;
            }
        }
        public async Task<IEnumerable<BookGenerRelations>> FindByGenerId(int id)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                await db.OpenAsync();
                var sqlQuery = $@"SELECT [BookId],[GenreId]
                                FROM [dbo].[BookGenerRelations]
                                WHERE [GenreId]= @id";
                var result = await db.QueryAsync<BookGenerRelations>(sqlQuery, id);
                return result;
            }
        }
        public async Task<IEnumerable<int>> GetAllGenerId(int id)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                await db.OpenAsync();
                var sqlQuery = $@"SELECT [BookId]
                                FROM [dbo].[BookGenerRelations]
                                WHERE [GenreId]= @id";
                var f = await db.QueryAsync<BookGenerRelations>(sqlQuery, id);
                var result = from m in f where m.BookId == id select m.GenreId;
                return result;
            }
        }
        public async Task<IEnumerable<BookGenerRelations>> GetAllAsync()
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.OpenAsync();

                    var sqlQuery = $@"SELECT [BookId],[GenreId]
                                      FROM [dbo].[BookGenerRelations]";
                    var result = await db.QueryAsync<BookGenerRelations>(sqlQuery);

                    return result;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }
        public async Task<Boolean> RemoveAsync(int bookId)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.OpenAsync();
                    var sqlQuery = $@"DELETE 
                                      FROM [dbo].[BookGenerRelations]
                                      WHERE [BookId] IN @value";
                    await db.QueryAsync<BookGenerRelations>(sqlQuery, new
                    {
                        value = bookId
                    });
                    return true;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }
        public async Task<Boolean> RemoveAsync(List<int> bookIds)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.OpenAsync();
                    var sqlQuery = $@"DELETE 
                                      FROM [dbo].[BookGenerRelations]
                                      WHERE [BookId] IN @Value";
                    await db.QueryAsync<BookGenerRelations>(sqlQuery, new {
                        Value = bookIds
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
                    var sqlQuery = $@"UPDATE[dbo].[BookGenerRelations]
                                    SET 
                                    [BookId] = @BookId,
                                    [GenreId] = @GenreId,
                                    WHERE [BookId]=@BookId";
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
