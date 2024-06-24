using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Check if any products exist.
                if (context.Products.Any())
                {
                    return;   // DB has been seeded
                }

                // Add initial products
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Product 1",
                        Price = 10.0m,
                        Category = "Category 1",
                        Availability = true
                    },
                    new Product
                    {
                        Name = "Product 2",
                        Price = 20.0m,
                        Category = "Category 2",
                        Availability = true
                    },
                    new Product
                    {
                        Name = "Product 3",
                        Price = 30.0m,
                        Category = "Category 3",
                        Availability = true
                    }
                );

                await context.SaveChangesAsync();

                // Add roles and users
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

                string[] roleNames = { "Admin", "Customer" };
                IdentityResult roleResult;

                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                // Create an admin user
                var adminUser = new IdentityUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com"
                };

                string adminPassword = "Admin@123";
                var admin = await userManager.FindByEmailAsync(adminUser.Email);

                if (admin == null)
                {
                    var createAdmin = await userManager.CreateAsync(adminUser, adminPassword);
                    if (createAdmin.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                }

                // Create a customer user
                var customerUser = new IdentityUser
                {
                    UserName = "customer@customer.com",
                    Email = "customer@customer.com"
                };

                string customerPassword = "Customer@123";
                var customer = await userManager.FindByEmailAsync(customerUser.Email);

                if (customer == null)
                {
                    var createCustomer = await userManager.CreateAsync(customerUser, customerPassword);
                    if (createCustomer.Succeeded)
                    {
                        await userManager.AddToRoleAsync(customerUser, "Customer");
                    }
                }
            }
        }
    }
}

