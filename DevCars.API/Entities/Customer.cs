﻿using System;
using System.Collections.Generic;

namespace DevCars.API.Entities
{
    public class Customer : BaseEntity
    {
        protected Customer() : base() { }
        public Customer(string fullName, string document, DateTime birthDate)
        {
            FullName = fullName;
            Document = document;
            BirthDate = birthDate;

            Orders = new List<Order>();
        }

        public string FullName { get; private set; }
        public string Document { get; private set; }
        public DateTime BirthDate { get; private set; }
        public List<Order> Orders { get; private set; }

        public void Purchase(Order order)
        {
            Orders.Add(order);
        }
    }

}
