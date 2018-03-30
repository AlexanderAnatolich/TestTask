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
        public async Task<IEnumerable<BookGenerRelations>> GetAllAsync()
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.OpenAsync();

                    var sqlQuery = $@"SELECT *
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
