using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace BLL.Models
{
    public class NewsPaperViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int PublishHouse { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PrindDate { get; set; }
        [Required]
        public int Price { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateInsert { get; set; }

        [JsonIgnore]
        [ScriptIgnore]
        public virtual PaperPublishHouseViewModel PaperPublishHous { get; set; }
    }
    public class EditNewsPaperViewModel
    {
        [Key]
        [XmlAttribute()]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int PublishHouse { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PrindDate { get; set; }
        [Required]
        public int Price { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateInsert { get; set; }
        [XmlIgnore]
        [JsonIgnore]
        public virtual PaperPublishHouseViewModel PaperPublishHous { get; set; }
        [XmlIgnore]
        [JsonIgnore]
        public IEnumerable<SelectListItem> ListPublichHouse { get; set; }
    }
    public class CreateNewsPaperViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int PublishHouse { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PrindDate { get; set; }
        [Required]
        public int Price { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateInsert { get; set; }
        public virtual PaperPublishHouseViewModel PaperPublishHous { get; set; }
        public IEnumerable<SelectListItem> ListPublichHouse { get; set; }
    }
}