﻿

namespace Cafemobile_Cafeteria.Models.DTO
{
    public class NewMealDTO
    {
        public int MealId { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string category { get; set; }
        public string? image { get; set; }
        public string description { get; set; }
    }
}
