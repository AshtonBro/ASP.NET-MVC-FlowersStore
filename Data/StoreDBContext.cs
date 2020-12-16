using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FlowersStore.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowersStore.Data
{
    public class StoreDBContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }  
        public virtual DbSet<Basket> Baskets { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ShopingCart> ShopingCarts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public StoreDBContext()
        {
            this.Database.EnsureCreated();

            // Mock datasource
            if (!(Users.Any() & Products.Any() & ShopingCarts.Any() & Baskets.Any() & Categories.Any()))
            {

                var categories = new Category[]
                {
                    new Category() { CategoryId = Guid.NewGuid(), FlowersType = "Букеты" },
                    new Category() { CategoryId = Guid.NewGuid(), FlowersType = "Растения" },
                    new Category() { CategoryId = Guid.NewGuid(), FlowersType = "Розы" },
                    new Category() { CategoryId = Guid.NewGuid(), FlowersType = "Тюльпаны" }
                };

                var allCategoriesDirectories = Directory.GetDirectories(@"wwwroot\Image\").Select(f => new DirectoryInfo(f));

                List<Product> products = new List<Product>();

                Dictionary<string, Dictionary<string, string>> nameDescription = new Dictionary<string, Dictionary<string, string>>();
                nameDescription.Add("Букеты", new Dictionary<string, string>()
                {
                    { "Пламя любви", "сезонные цветы"},
                    { "Альстримерия Микс", "Стойкие цветы альстримерии имеют густую листву и несколько соцветий на одной ветке"},
                    { "Букет для мамы", "сезонные цветы"},
                    { "Сборный букет №18", "сезонные цветы"}
                });

                nameDescription.Add("Растения", new Dictionary<string, string>()
                {
                    { "Пламя любви", "сезонные цветы"},
                    { "Альстримерия Микс", "Стойкие цветы альстримерии имеют густую листву и несколько соцветий на одной ветке"},
                    { "Букет для мамы", "сезонные цветы"},
                    { "Сборный букет №18", "сезонные цветы"}
                });

                nameDescription.Add("Розы", new Dictionary<string, string>()
                {
                    { "Пламя любви", "сезонные цветы"},
                    { "Альстримерия Микс", "Стойкие цветы альстримерии имеют густую листву и несколько соцветий на одной ветке"},
                    { "Букет для мамы", "сезонные цветы"},
                    { "Сборный букет №18", "сезонные цветы"}
                });

                nameDescription.Add("Тюльпаны", new Dictionary<string, string>()
                {
                    { "Пламя любви", "сезонные цветы"},
                    { "Альстримерия Микс", "Стойкие цветы альстримерии имеют густую листву и несколько соцветий на одной ветке"},
                    { "Букет для мамы", "сезонные цветы"},
                    { "Сборный букет №18", "сезонные цветы"}
                });


                //var Bouquets = new string[] { };
                //var Plants = new string[] { };
                //var Roses = new string[] { };
                //var Tulips = new string[] { };

                foreach (DirectoryInfo category in allCategoriesDirectories)
                {
                    var categoryName = category.Name;
                    foreach (var image in category.GetFiles())
                    {
                        products.Add(new Product()
                        {
                            ProductId = Guid.NewGuid(),
                            Image = File.ReadAllBytes(image.FullName),
                            CategoryId = categories.FirstOrDefault(f => f.FlowersType == categoryName).CategoryId,
                            
                        });
                    }
                }


                //var products = new Product[]
                //{
                //     new Product() { ProductId = Guid.NewGuid(), Image = byteImage[0], Name = "Роза кастовая", Description = "Большая часть сортов роз получена в результате длительной...", Color = "Красный", Price = 78, Category = categories[1]}
                //};

                var users = new User[]
                {
                 new User() { UserId = Guid.NewGuid(), Name = "Alan", SecondName = "Rikman", Email = "rikmanAl@gmail.com", Password = "admin", DateCreated = DateTime.Now, Phone = 89226664433 }
                };

                var baskets = new Basket[]
                {
                new Basket() { UserId = users[0].UserId}
                };


            }


        }

        public StoreDBContext(DbContextOptions<StoreDBContext> options)
           : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //default connection
            optionsBuilder.UseSqlServer(@"Data Source=ASHTON\ASHTON;Initial Catalog=FlowersStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");


        }

    }
}

/*
 
 public SchoolDbContext()
        {
            this.Database.EnsureCreated();
            if (!(Students.Any() & Groups.Any() & StudentGroupLinks.Any()))
            {
                //create mock data
                var students = new Student[] 
                {
                    new Student() {StudentId = Guid.NewGuid(), Gender = Student.eGender.Female, Name = "Sophia",Surname = "Lopez"},
                };
                Students.AddRange(students);

                var groups = new Group[]
                {
                    new Group(){GroupId = Guid.NewGuid(), Name = "Engineering"},
                };

                Groups.AddRange(groups);

                Random rand = new Random();

                foreach (var student in students)
                {
                    StudentGroupLinks.Add(new StudentGroup() { Id = Guid.NewGuid(), StudentId = student.StudentId, GroupId = groups[rand.Next(0, 1)].GroupId });
                    StudentGroupLinks.Add(new StudentGroup() { Id = Guid.NewGuid(), StudentId = student.StudentId, GroupId = groups[rand.Next(2, 3)].GroupId });
                    StudentGroupLinks.Add(new StudentGroup() { Id = Guid.NewGuid(), StudentId = student.StudentId, GroupId = groups[rand.Next(4, 5)].GroupId });
                }

                SaveChanges();

            }
        }
 */