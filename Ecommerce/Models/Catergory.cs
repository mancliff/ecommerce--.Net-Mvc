using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class Catergory
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Catergory Name Required")]
        [StringLength(100, ErrorMessage = "minimum 3 and minimum 5 and maximum 100 characters are allowed", MinimumLength = 3)]
        public string Categoryname { get; set; }
        public Nullable<bool> isDelete { get; set; }
    }
}