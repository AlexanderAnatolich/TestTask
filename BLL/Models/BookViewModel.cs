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
        [ScaffoldColumn(false)]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime YearOfPublish { get; set; }
        [Required]
        public string PublishingHouse { get; set; }
        public int Price { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateInsert { get; set; }
        public int Genre { get; set; }
        [JsonIgnore]
        [ScriptIgnore]
        public virtual GenerViewModel Gener { get; set; }
    }
    public partial class EditBookViewModel
    {
        [Key]
        [XmlAttribute()]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime YearOfPublish { get; set; }
        [Required]
        public string PublishingHouse { get; set; }
        public int Price { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateInsert { get; set; }
        public int Genre { get; set; }
        [XmlIgnore]
        [JsonIgnore]
        public virtual GenerViewModel Gener { get; set; }
        [XmlIgnore]
        [JsonIgnore]
        [ScriptIgnore]
        public IEnumerable<SelectListItem> ListGeners { get; set; }
    }
    public partial class CreateBookViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int Genre { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime YearOfPublish { get; set; }
        [Required]
        public string PublishingHouse { get; set; }
        public int Price { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateInsert { get; set; }      
        public virtual GenerViewModel Gener { get; set; }
        public IEnumerable<SelectListItem> ListGeners { get; set; }
    }


}
