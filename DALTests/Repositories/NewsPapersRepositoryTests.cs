using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using Model.Models;
using DAL.Models;

namespace DAL.Repositories.Tests
{
    [TestClass()]
    public class NewsPapersRepositoryTests
    {
        [TestMethod()]
        public void GetNews()
        {
            NewsPaper item = new NewsPaper()
            {
                Price = 123,
                PrintDate = DateTime.Now,
                PublishHouseId = 1,
                Title = "TEst",
                DateInsert = DateTime.Now
            };
            using (var db = new SqlConnection(@"data source=DESKTOP-1RUU7VH\ANATOLICHDB;initial catalog=MyDB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"))
            {
                db.Open();

                var sqlQuery = $@"INSERT INTO [dbo].[NewsPapers]
                                   ([Title]
                                   ,[Price]
                                   ,[DateInsert]
                                   ,[PublishHouseId]
                                   ,[PrintDate])
                        VALUES (@Title,
                            @Price,@DateInsert,@PublishHouseId,@PrintDate)";
                db.Query(sqlQuery, new
                {
                    Title = item.Title,
                    Price = item.Price,
                    DateInsert = item.DateInsert,
                    PublishHouseId = item.PublishHouseId,
                    PrintDate = item.PrintDate
                });
            }
            //using (var db = new SqlConnection(@"data source=DESKTOP-1RUU7VH\ANATOLICHDB;initial catalog=MyDB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"))
            //{
            //    db.Open();
            //    var sqlQuery = $@"SELECT *
            //                    FROM [dbo].[NewsPapers] 
            //                    LEFT JOIN [dbo].[PublishHouses]
            //                    ON [dbo].[NewsPapers].[PublishHouseId] = [dbo].[PublishHouses].[Id]";
            //    var queryResult = db.Query<NewsPaperViewModel, PublishHouseViewModel, NewsPaperViewModel>(sqlQuery,
            //        (book, publishHouse) =>
            //        {
            //            book.PublishHouse = publishHouse;
            //            book.PublishHouse_Id = publishHouse.Id;
            //            return book;
            //        }
            //        );
            //    var result = queryResult.ToList();
            //    var g = result;
            //}
        }

        [TestMethod()]
        public void CreateAsyncTest()
        {

        }
    }
}