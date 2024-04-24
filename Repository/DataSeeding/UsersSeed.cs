using Core.Models.User;
using Microsoft.AspNetCore.Identity;
using Repository.Context;

namespace Repository.DataSeeding
{
    public static class UsersSeed
    {
        public static async Task SeedUsers(UserManager<ApplicationUser> manager)
        {
            if(!manager.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    UserName = "eslammahmoud",
                    Email = "eslammahmoud@gmail.com",
                    DisplayName = "Eslam",
                    UserAddress = new Address() { City = "cairo" , Country = "egypt" ,PostalCode="12345" , State = "EGYPT" , Street="st" }
                };
                await manager.CreateAsync(user,"P@ssword1234");
            }
        }
    }
}
