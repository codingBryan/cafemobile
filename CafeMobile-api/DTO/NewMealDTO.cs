﻿using CafeMobile_api.Models.Entities;

namespace CafeMobile_api.DTO
{
    public class NewMealDTO
    {
        public string name { get; set; }
        public double price { get; set; }   
        public string? image { get; set; }
        public string category { get; set; }
        public string? description { get; set; }

    }
}
