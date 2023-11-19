﻿namespace CafeMobile.Models
{
    public class Token <U>
    {
        public U user {  get; set; }
        public string token { get; set; }
        public DateTime expiresAt { get; set; }
    }
}
