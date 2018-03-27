using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using Model.Models;

namespace DAL.Repositories.Tests
{
    [TestClass()]
    public class BookGenerRelationsRepositoryTests
    {
        [TestMethod()]
        public void GetAllGenerId()
        {
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
                                ON [dbo].[Books].[PublishHouseId] = [dbo].[PublishHouses].[Id]";
                var invoiceDictionary = new Dictionary<int, BookViewModel>();
                var f = db.Query<BookViewModel, GenerViewModel, PublishHouseViewModel, BookViewModel>(sqlQuery,
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
                    ).Distinct().AsQueryable();
            }
        }
    }
}