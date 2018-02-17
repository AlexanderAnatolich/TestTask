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
using BLL.Models;
using BLL.Models.Enum;
using BLL.Services;
using Kendo.Mvc.UI;
using Microsoft.Ajax.Utilities;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;

namespace WEB.Controllers
{
    public class MyClass
    {
        public MyClass()
        {

        }
        public int[] CId { get; set; }
        public string[] CT { get; set; }
    }
    public class SaveController : Controller
    {
        private BookService bookService = new BookService(ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString());
        private NewsPaperService paperService = new NewsPaperService(ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString());

        [HttpGet]
        public FileResult SaveAsXML(string data)
        {
            var model = JsonConvert.DeserializeObject<MyClass>(data);
            var formatedData = FormatInputData(model);
            var rand = new Random();
            var indexFormatFile = FormatFile.XML;

            var currFile = Server.MapPath("~/Files/" + rand.Next().ToString() + "." + indexFormatFile.ToString());

            using (var file = System.IO.File.CreateText(currFile))
            {
                var serializer = new XmlSerializer(typeof(List<EditBookViewModel>));
                serializer.Serialize(file, formatedData[0]);
            }
            using (var file = System.IO.File.AppendText(currFile))
            {
                var serializer = new XmlSerializer(typeof(List<EditNewsPaperViewModel>));
                serializer.Serialize(file, formatedData[1]);
            }
            return SendFile(currFile, indexFormatFile);
        }
        [HttpGet]
        public ActionResult SaveAsJSON(string data)
        {
            var model = JsonConvert.DeserializeObject<MyClass>(data);
            var formatedData = FormatInputData(model);
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
        public IList FormatInputData(MyClass model)
        {
            IList[] returnedValue = new IList[2];
            returnedValue[0] = new List<EditBookViewModel>();
            returnedValue[1] = new List<EditNewsPaperViewModel>();

            for (int i = 0; i < model.CT.Length; i++)
            {
                if (model.CT[i] == "Book")
                {
                    var item = bookService.GetBook(model.CId[i]);
                    returnedValue[0].Add(item);
                }
                else
                {
                    var item = paperService.GetNewsPaper(model.CId[i]);
                    returnedValue[1].Add(item);
                }
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