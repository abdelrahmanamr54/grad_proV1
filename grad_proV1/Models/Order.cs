﻿using System.ComponentModel.DataAnnotations;

namespace grad_proV1.Models
{
    public class Order
    {
      //  [Key]
      public  int Id { get; set; }
        

        public List<Product> products { get; set; }
    }
}