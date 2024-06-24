﻿namespace WebApplication1.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}
