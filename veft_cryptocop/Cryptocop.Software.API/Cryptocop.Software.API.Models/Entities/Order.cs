﻿using System;
using System.Collections.Generic;

namespace Cryptocop.Software.API.Models.Entities
{
    public class Order
    {
        public int Id { get; set; }
        
        public string FullName { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string CardHolderName { get; set; }
        public string MaskedCreditCard { get; set; }
        public DateTime OrderDate { get; set; }
        public float TotalPrice { get; set; }
        
        // Navigation properties
        
        public string Email { get; set; }

        public User User { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        
        
    }
}