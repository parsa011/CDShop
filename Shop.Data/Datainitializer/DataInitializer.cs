using Microsoft.Extensions.DependencyInjection;
using Shop.Data.DatabaseContext;
using Shop.Domain.Core;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Data.Datainitializer
{
    public class DataInitializer
    {
        public static async void InitializeDataAsync(IServiceProvider services)
        {
            using (var context = services.GetRequiredService<ShopDbContext>())
            {
                if (!context.Roles.Any())
                {
                    context.Roles.Add(new Role { Name = "Admin", Title = "مدیر سایت" });
                    context.Roles.Add(new Role { Name = "User", Title = "کاربر سایت" });
                    context.SaveChanges();
                }
                if (!context.Users.Any())
                {
                    context.Users.Add(new User
                    {
                        PhoneNumber = "09116880563",
                        Name = "parsa mahmoudi",
                        RoleId = context.Roles.Where(r => r.Name == "Admin").FirstOrDefault().Id,
                        IsActive = true,
                        ActivationCode = GuidGenerator.NewGuid(),
                        PasswordHash = PasswordHash.HashWithMD5("1234"),
                        IsDeleted = false,
                        Role = context.Roles.Where(r => r.Name == "Admin").FirstOrDefault()
                    });      
                }
                if (!context.Categories.Any())
                {
                    context.Categories.Add(new Category { ParentId = 0, Title = "دسته بندی نشده" });
                }
                context.SaveChanges();
            }
        }
    }
}
