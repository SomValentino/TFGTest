using TFG.Domain.Entities;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;
namespace TFG.Unit.Tests.Setup;

public static class TestCustomer {
    public static IEnumerable<Customer> GetCustomers () {
        return new [] {
               new Customer{
                    Id = "62b212b7aa7678cee7faad0d",
                    UserName = "aiyanda",
                    Password = "UIthNMn8o2eSz01poj8tEmOLyTSljreDxhLfb9WqPb2smtVI",
                    FirstName = "Ahmed",
                    Role = new Role {
                        Id= "62b211baaa7678cee7faad0c",
                        Name = "User",
                        CreatedAt= DateTime.Now
                    },
                    LastName="Iyanda",
                    Email="ahmed.iyanda@example.com",
                    PhoneNumber="01234567",
                    CreatedAt = DateTime.Now
                },
                new Customer{
                    Id = "62b215d400367909032d88ab",
                    UserName = "john.doe",
                    Password = "UIthNMn8o2eSz01poj8tEmOLyTSljreDxhLfb9WqPb2smtVI",
                    FirstName = "John",
                    Role = new Role {
                        Id= "62b211baaa7678cee7faad0c",
                        Name = "Administrator",
                        CreatedAt= DateTime.Now
                    },
                    LastName="Doe",
                    Email="john.doe@example.com",
                    PhoneNumber="01234567",
                    CreatedAt = DateTime.Now
                } 
            };
        }
    }