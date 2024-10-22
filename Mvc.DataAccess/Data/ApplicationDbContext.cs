using Microsoft.DiaSymReader;
using Microsoft.EntityFrameworkCore;
using Mvc.Model;
using System.Security.Policy;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Mvc.DataAccess.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id =1, Name="Action", DisplayOrder=1 },
                new Category { Id =2, Name="SciFi", DisplayOrder=2 },
                new Category { Id=3, Name ="History", DisplayOrder=3}
                ) ;
            modelBuilder.Entity<Company>().HasData(
                new Company { Id=1, Name="HASBC", CurrentAddress="Chandni Chowk",City="Delhi", State="Delhi", PostalCode="123456", PhoneNumber="212332323" },
                new Company { Id=2, Name="YTFD", CurrentAddress="BLR chowk", City="Bangalore", State="Karnaataka", PostalCode="765432", PhoneNumber="123456774" },
                new Company { Id=3, Name="ETEED", CurrentAddress="MUM Chowk", City="Mumbai", State="Maharashtra", PostalCode="867654", PhoneNumber="234567" },
                new Company { Id=4, Name="LTRD", CurrentAddress="IND Chowk", City="Indore", State="MadhyaPradesh", PostalCode="234432", PhoneNumber="2345654323" },
                new Company { Id=5, Name="UJRD", CurrentAddress="KLK Chowk", City="Kolkata", State="WestBengal", PostalCode="345433", PhoneNumber="7787234342" }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id=1,
                    Title="Fortue Of Time",
                    Author="Billy Spark",
                    Description="Praesent vitae sodales libero.Praesent molestie orci augue, vitae euismod velit sollicitudin ac.Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN="SWD9999001",
                    LastPrice=99,
                    Price=90,
                    Price50=85,
                    Price100=80,
                    CategoryId=1,
                    imageUrl=""

                },
                new Product
                {
                    Id=2,
                    Title="Dark Skies",
                    Author="Nancy Hoover",
                    Description="Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN="CAW777777701",
                    LastPrice=40,
                    Price=30,
                    Price50=25,
                    Price100=20,
                    CategoryId=2,
                    imageUrl=""

                },
                new Product
                {
                    Id=3,
                    Title="Vanish in the sunset",
                    Author="Julian Button",
                    Description="Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN="RITO5555501",
                    LastPrice=55,
                    Price=50,
                    Price50=40,
                    Price100=35,
                    CategoryId=2,
                    imageUrl=""
                },
                new Product
                {
                    Id=4,
                    Title="Cottone Candy",
                    Author= "Abby Muscles",
                    Description ="Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN="WS3333333301",
                    LastPrice=70,
                    Price=65,
                    Price50=60,
                    Price100=55,
                    CategoryId=2,
                    imageUrl=""

                },
                new Product
                {
                    Id=5,
                    Title="Rock in the ocean",
                    Author="Ron Parker",
                    Description="Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN="SOTJ1111111101",
                    LastPrice=30,
                    Price=27,
                    Price50=25,
                    Price100=20,
                    CategoryId=2,
                    imageUrl=""
                },
                new Product
                {
                    Id=6,
                    Title="Leaves and Wonders",
                    Author="laura  Phantom",
                    Description="Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN="FOT000000001",
                    LastPrice=25,
                    Price=23,
                    Price50=22,
                    Price100=20,
                    CategoryId=2,
                    imageUrl=""
                }
            
            );
        }
    }
}
