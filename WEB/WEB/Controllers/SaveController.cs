using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using Model.Models;
using Model.Models.Enum;
using BLL.Services;
using Kendo.Mvc.UI;
using Microsoft.Ajax.Utilities;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using Model.Models.DTO;
namespace WEB.Controllers
{
    public class SaveController : Controller
    {
        private BookService bookService = new BookService(ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString());
        private NewsPaperService paperService = new NewsPaperService(ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString());

        [HttpGet]
        public async Task<FileResult> SaveAsXML(DataFileViewModel data)
        {
            var model = data;
            var formatedData = await FormatInputData(model);
            var rand = new Random();
            var indexFormatFile = FormatFile.XML;

            var currFile = Server.MapPath("~/Files/" + rand.Next().ToString() + "." + indexFormatFile.ToString());

            using (var file = System.IO.File.CreateText(currFile))
            {
                var serializer = new XmlSerializer(typeof(List<BookViewModel>));
                serializer.Serialize(file, formatedData[0]);
            }
            using (var file = System.IO.File.AppendText(currFile))
            {
                var serializer = new XmlSerializer(typeof(List<NewsPaperViewModel>));
                serializer.Serialize(file, formatedData[1]);
            }
            return SendFile(currFile, indexFormatFile);
        }
        [HttpGet]
        public async Task<ActionResult> SaveAsJSON(DataFileViewModel data)
        {
            var model = data;
            var formatedData = await FormatInputData(model);
            var rand = new Random();
            var indexFormatFile = FormatFile.JSON;

            var currFile = Server.MapPath("~/Files/" + rand.Next().ToString() + "." + indexFormatFile.ToString());

            using (var file = System.IO.File.CreateText(currFile))
            {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(file, formatedData);
                file.Close();
                return SendFile(currFile, indexFormatFile);
            }
        }
        public async Task<IList> FormatInputData(DataFileViewModel model)
        {
            IList[] returnedValue = new IList[2];
            returnedValue[0] = new List<BookViewModel>();
            returnedValue[1] = new List<NewsPaperViewModel>();
            try
            {
                for (int i = 0; i < model.CT.Length; i++)
                {
                    if (model.CT[i] == "Book")
                    {
                        var item = await bookService.GetAsync(model.CId[i]);
                        returnedValue[0].Add(item);
                    }
                    else
                    {
                        var item = paperService.GetAsync(model.CId[i]);
                        returnedValue[1].Add(item);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
            return returnedValue;
        }
        private FileResult SendFile(string filePathName, FormatFile formatFile)
        {

            var fs = new FileStream(filePathName, FileMode.Open);
            var file_type = "application/" + formatFile.ToString();
            var file_name = "YouFile." + formatFile.ToString();
            var xz = File(fs, file_type, file_name);
            return xz;
        }
    }
}