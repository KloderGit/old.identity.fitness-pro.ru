using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace identity.fitness_pro.ru.Configuration.Models
{
    public class TestUsers
    {
        public List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password",

                    Claims = new []
                    {
                        new Claim("name", "Alice"),
                        new Claim("website", "https://alice.com")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password",

                    Claims = new []
                    {
                        new Claim("name", "Bob"),
                        new Claim("website", "https://bob.com"),
                        new Claim("Role", "Student"),
                        new Claim("Dogovor", "Common"),
                        new Claim("Discount", "false")
                    }
                }
            };
        }
    }
}
