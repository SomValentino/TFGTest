using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFG.Domain.Entities;

namespace TFG.Unit.Tests.Setup
{
    public  class TestRole
    {
        public static IEnumerable<Role> GetRoles()
        {
            return new[]
            {
                new Role {
                        Id= "62b211baaa7678cee7faad0c",
                        Name = "Administrator",
                        CreatedAt= DateTime.Now
                },
                new Role {
                        Id= "62b211baaa7678cee7faad0c",
                        Name = "User",
                        CreatedAt= DateTime.Now
                }
            };
        }
    }
}
