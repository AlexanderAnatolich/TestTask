using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.Models
{
    public class NewsPaperViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PrintDate { get; set; }
        [Required]
        public int Price { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateInsert { get; set; }
        public int PublishHouse_Id { get; set; }
        public virtual PublishHouseViewModel PublishHouse { get; set; }
    }
    public class CreateNewsPaperViewModel
    {
        public string Title { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PrintDate { get; set; }
        [Required]
        public int Price { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateInsert { get; set; }
        public int PublishHouse_Id { get; set; }
        public virtual PublishHouseViewModel PublishHouse { get; set; }
    }
}