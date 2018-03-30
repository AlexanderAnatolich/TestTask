using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models;
using System.Data.SqlClient;
using Dapper;
using DAL.DTO;
using DAL.Models;

namespace DAL.Repositories.Tests
{
    [TestClass()]
    public class BookRepositoryTests
    {
        [TestMethod()]
        public void Find()
        {
            int id = 4;
            using (var db = new SqlConnection(@"data source=DESKTOP-1RUU7VH\ANATOLICHDB;initial catalog=MyDB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"))
            {
                db.Open();
                var sqlQuery = $@"SELECT *
                                FROM [dbo].[Books]
                                LEFT JOIN [dbo].[BookGenerRelations]
                                ON [dbo].[Books].[Id] = [dbo].[BookGenerRelations].[BookID]                                 
                                LEFT JOIN [dbo].[Geners] 
                                ON [dbo].[BookGenerRelations].[GenreId] = [dbo].[Geners].[Id]
                                LEFT JOIN [dbo].[PublishHouses]
                                ON [dbo].[Books].[PublishHouseId] = [dbo].[PublishHouses].[Id]
                                WHERE [dbo].[Books].[Id]= '4'";
                var invoiceDictionary = new Dictionary<int, BookViewModel>();
                var queryResult = db.Query<BookViewModel, GenerViewModel, PublishHouseViewModel, BookViewModel>(sqlQuery,
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
                    , new { id = id });
                var result = queryResult.Distinct().AsQueryable();
            }
        }

        [TestMethod()]
        public async Task CreateAsyncTestBGAsync()
        {
            SqlTransaction trans = null;
            BookDTO item = new BookDTO()
            {
                Author = "fdfdf",
                DateInsert = DateTime.Now,
                Genre = new List<Models.Gener>()
                {
                    new Models.Gener()
                    {
                        Id=1,
                        Genre = "Genre1"
                    }
                },
                Price = 123,
                PublishHouseId = 1,
                Title = "sdfsdf",
                YearOfPublish = DateTime.Now,
            };

            try
            {
                using (var db = new SqlConnection(@"data source=DESKTOP-1RUU7VH\ANATOLICHDB;initial catalog=MyDB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"))
                {
                    await db.OpenAsync();
                    trans = db.BeginTransaction();
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

                    var result = db.Execute(sqlQuery2, relations, transaction: trans);
                    trans.Commit();
                    trans.Dispose();
                }
            }
            catch (SqlException ex)
            {
                trans.Rollback();
                trans.Dispose();
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }

        [TestMethod()]
        public async Task FirndByTitleAsyncTestBGtitAsync()
        {
            var title = "B";
            try
            {
                using (var db = new SqlConnection(@"data source=DESKTOP-1RUU7VH\ANATOLICHDB;initial catalog=MyDB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"))
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
                        }, new { value = '%'+title+'%' });
                    var result = resultQuery.Distinct().ToList();
                }
            }catch(Exception ex)
            {
                var t = ex.Message;
            }
        }
    }
}