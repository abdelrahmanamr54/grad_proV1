﻿namespace grad_proV1.Models
{
    public class Service
    {


        public int Id { get; set; }
        public string Name { get; set; 
        }
        public string Description { get; set; }

        public List<Provider>providers{ get; set; }

    }
}
