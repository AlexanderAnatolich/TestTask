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
using DAL.DTO;

namespace DAL.Repositories
{
    
    public class BookRepository
    {
        
        private string _connectionString;
        public BookRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task CreateAsync(BookDTO item)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.OpenAsync();
                    SqlTransaction trans = db.BeginTransaction();
                    var sqlQuery = $@"INSERT INTO [dbo].[Books]
                        ([Title],[Author],[YearOfPublish],
                        [Price],[DateInsert],[PublishHouseId])
                        VALUES
                        (@Title,@Author,@YearOfPublish,@Price,@DateInsert,@PublishHouseId)
                        SELECT CAST(SCOPE_IDENTITY() as int)";
                    var queryResult = await db.QueryAsync<int>(sqlQuery, new
                    {
                        Title = item.Title,
                        Author = item.Author,
                        YearOfPublish = item.YearOfPublish,
                        Price = item.Price,
                        DateInsert = item.DateInsert,
                        PublishHouseId = item.PublishHouseId,
                    },
                    transaction: trans);
                    var id = queryResult.Single();

                    var relations = item.Genre.Select(x => new BookGenerRelations()
                    {
                        BookId = id,
                        GenreId = x.Id
                    });

                    var sqlQuery2 = $@"INSERT INTO[dbo].[BookGenerRelations]
                                            ([BookId],[GenreId])
                                            VALUES(@BookId, @GenreId)";

                    var result = await db.ExecuteAsync(sqlQuery2, relations, transaction: trans);
                    trans.Commit();
                    trans.Dispose();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() Error ", GetType().FullName), ex);
            }
        }
        public async Task<BookDTO> FindByIdAsync(int id)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                await db.OpenAsync();
                var sqlQuery = $@"SELECT *
                                FROM [dbo].[Books]                               
                                LEFT JOIN [dbo].[BookGenerRelations] 
                                ON [dbo].[Books].[Id] = [dbo].[BookGenerRelations].[BookID]
                                LEFT JOIN [dbo].[Geners] 
                                ON [dbo].[BookGenerRelations].[GenreId] = [dbo].[Geners].[Id]
                                LEFT JOIN [dbo].[PublishHouses]
                                ON [dbo].[Books].[PublishHouseId] = [dbo].[PublishHouses].[Id]
                                WHERE [dbo].[Books].[Id]= @id";
                var invoiceDictionary = new Dictionary<int, BookDTO>();
                var resultQuery = await db.QueryAsync<BookDTO, Gener, PublishHouse, BookDTO>(sqlQuery,
                    (book, gener, publishHouse) =>
                    {
                        BookDTO model;
                        if (!invoiceDictionary.TryGetValue(book.Id, out model))
                        {
                            model = book;
                            model.Genre = new List<Gener>();
                            invoiceDictionary.Add(model.Id, model);
                        }
                        model.Genre.Add(gener);
                        model.PublishHouse = publishHouse;
                        return model;
                    }, new { id = id });
                var result = resultQuery.First();
                return result;
            }           
        }
        public async Task<IEnumerable<BookDTO>> GetAllAsync()
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.OpenAsync();
                    var sqlQuery = $@"SELECT *
                                FROM [dbo].[Books] 
                                LEFT JOIN [dbo].[BookGenerRelations] 
                                ON [dbo].[Books].[Id] = [dbo].[BookGenerRelations].[BookID]
                                LEFT JOIN [dbo].[Geners] 
                                ON [dbo].[BookGenerRelations].[GenreId] = [dbo].[Geners].[Id]
                                LEFT JOIN [dbo].[PublishHouses]
                                ON [dbo].[Books].[PublishHouseId] = [dbo].[PublishHouses].[Id]";
                    var invoiceDictionary = new Dictionary<int, BookDTO>();
                    var resultQuery = await db.QueryAsync<BookDTO, Gener, PublishHouse, BookDTO>(sqlQuery,
                        (book, gener, publishHouse) =>
                        {
                            BookDTO model;
                            if (!invoiceDictionary.TryGetValue(book.Id, out model))
                            {
                                model = book;
                                model.Genre = new List<Gener>();
                                invoiceDictionary.Add(model.Id, model);
                            }
                            model.Genre.Add(gener);
                            model.PublishHouse = publishHouse;
                            return model;
                        });
                    var result = resultQuery.Distinct().AsEnumerable();
                    return result;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }
        public async Task<IEnumerable<BookDTO>> GetByAuthorAsync(string author)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.OpenAsync();
                    var sqlQuery = $@"SELECT *
                                FROM [dbo].[Books]
                                LEFT JOIN [dbo].[BookGenerRelations] 
                                ON [dbo].[Books].[Id] = [dbo].[BookGenerRelations].[BookID]
                                LEFT JOIN [dbo].[Geners] 
                                ON [dbo].[BookGenerRelations].[GenreId] = [dbo].[Geners].[Id]
                                LEFT JOIN [dbo].[PublishHouses]
                                ON [dbo].[Books].[PublishHouseId] = [dbo].[PublishHouses].[Id]
                                WHERE [dbo].[Books].[Author]= @value";
                    var invoiceDictionary = new Dictionary<int, BookDTO>();
                    var resultQuery = await db.QueryAsync<BookDTO, Gener, PublishHouse, BookDTO>(sqlQuery,
                        (book, gener, publishHouse) =>
                        {
                            BookDTO model;
                            if (!invoiceDictionary.TryGetValue(book.Id, out model))
                            {
                                model = book;
                                model.Genre = new List<Gener>();
                                invoiceDictionary.Add(model.Id, model);
                            }
                            model.Genre.Add(gener);
                            model.PublishHouse = publishHouse;
                            return model;
                        }, new { value = author });
                    var result = resultQuery.Distinct().AsQueryable();
                    return result;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }
        public async Task<IEnumerable<BookDTO>> FirndByTitleAsync(string title)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.OpenAsync();
                    var sqlQuery = $@"SELECT *
                                FROM [dbo].[Books]
                                LEFT JOIN [dbo].[BookGenerRelations] 
                                ON [dbo].[Books].[Id] = [dbo].[BookGenerRelations].[BookID]
                                LEFT JOIN [dbo].[Geners] 
                                ON [dbo].[BookGenerRelations].[GenreId] = [dbo].[Geners].[Id]
                                LEFT JOIN [dbo].[PublishHouses]
                                ON [dbo].[Books].[PublishHouseId] = [dbo].[PublishHouses].[Id]
                                WHERE [dbo].[Books].[Title] LIKE @value";
                    var invoiceDictionary = new Dictionary<int, BookDTO>();
                    var resultQuery = await db.QueryAsync<BookDTO, Gener, PublishHouse, BookDTO>(sqlQuery,
                        (book, gener, publishHouse) =>
                        {
                            BookDTO model;
                            if (!invoiceDictionary.TryGetValue(book.Id, out model))
                            {
                                model = book;
                                model.Genre = new List<Gener>();
                                invoiceDictionary.Add(model.Id, model);
                            }
                            model.Genre.Add(gener);
                            model.PublishHouse = publishHouse;
                            return model;
                        }, new { value = '%' + title + '%' });
                    var result = resultQuery.Distinct();
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
            using (var db = new SqlConnection(_connectionString))
            {
                SqlTransaction trans = null;
                try
                {
                    await db.OpenAsync();
                    trans = db.BeginTransaction();

                    var sqlQuery = $@"DELETE 
                                  FROM [dbo].[Books]
                                  WHERE [Id] = @value;
                                  DELETE
                                  FROM [dbo].[BookGenerRelations]
                                  WHERE [BookId] = @value";
                    var result = await db.ExecuteAsync(sqlQuery, new
                    {
                        value = item
                    }, transaction: trans);
                    trans.Commit();
                    trans.Dispose();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    trans.Dispose();
                    return false;
                    throw new Exception(String.Format("{0}.WithConnection() Exeption", GetType().FullName), ex);
                }
            }
        }
        public async Task<Boolean> RemoveAsync(List<int> ids)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                SqlTransaction trans = null;
                try
                {
                    await db.OpenAsync();
                    trans = db.BeginTransaction();

                    var sqlQuery = $@"DELETE 
                                  FROM [dbo].[Books]
                                  WHERE [Id] IN @value;
                                  DELETE
                                  FROM [dbo].[BookGenerRelations]
                                  WHERE [BookId] IN @value";
                    var result = await db.ExecuteAsync(sqlQuery, new
                    {
                        value = ids
                    }, transaction: trans);
                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    trans.Dispose();
                    return false;
                    throw new Exception(String.Format("{0}.WithConnection() Exeption", GetType().FullName), ex);
                }
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