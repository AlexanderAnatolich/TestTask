using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace BLL.Models
{
    public partial class BookViewModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [XmlAttribute()]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime YearOfPublish { get; set; }
        [Required]
        public virtual PublishHouseViewModel PublishHouse { get; set; }
        [Required]
        public double Price { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime DateInsert { get; set; }
        public List<GenerViewModel> Genre { get; set; }//TODO
    }
    public partial class CreateBookViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime YearOfPublish { get; set; }
        [Required]
        public virtual PublishHouseViewModel PublishHouse { get; set; }
        [Required]
        public double Price { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime DateInsert { get; set; }
        public List<GenerViewModel> Genre { get; set; }//TODO
    }
}
