﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace OptShopAPI.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string username { get; set; }

        public string email { get; set; }

        public string phoneNumber { get; set; }

        public string country { get; set; }

        public string city { get; set; }

        public string streetAddress { get; set; }

        public string houseNumber { get; set; }

        public string index { get; set; }

      //  public string? orderIds { get; set; }
        public string? province { get; set;}


      
    }
}
