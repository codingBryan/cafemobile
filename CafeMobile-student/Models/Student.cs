﻿namespace CafeMobile.Models
{
    public class Student
    {
        public string adm_no { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; } = String.Empty;
        public string email { get; set; }
        public string phone_number { get; set; }
        public string password { get; set; }
        public double cp_balance { get; set; }
    }
}
