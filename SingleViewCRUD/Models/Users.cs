using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SingleViewCRUD.Models
{
    public class Users
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Enter Your Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Plese Select a Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Enter a phone Number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Enter a Email Number")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter a Password")]
        public string Password { get; set; }

        public int CityID { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
      
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        public List<SelectListItem> CountryList { get; set; }
    }
}