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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ShopingCart>()
                .HasIndex(p => new { p.BasketId, p.ProductId})
                .IsUnique();
        }

        public StoreDBContext()
        {
            this.Database.EnsureCreated();

            // Mock datasource
            if (!(Users.Any() & Products.Any() & Baskets.Any() & Categories.Any()))
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

                Dictionary<string, Dictionary<string, string>> nameDescription = new Dictionary<string, Dictionary<string, string>>
                {
                    {
                        "Букеты",
                        new Dictionary<string, string>()
                {
                    { "Пламя любви", "Свадебный букет Пламя любви -  сочный и яркий элемент Вашей незабываемой свадьбы в красных тонах."},
                    { "Альстримерия Микс", "Стойкие цветы альстримерии имеют густую листву и несколько соцветий на одной ветке"},
                    { "Букет для мамы", "Мама - самый дорогой для нас человек. И так хочется иногда порадовать её чем-то необычным"},
                    { "Сборный букет №18", "Разноцветные альстромерии напоминают цветочный фейерверк в честь весеннего настроения"}
                }
                    },

                    {
                        "Растения",
                        new Dictionary<string, string>()
                {
                    { "Гардения", "Род назван в честь Александра Гардена, американского натуралиста"},
                    { "Хамедорея", "Род цветковых растений семейства Пальмовые"},
                    { "Аизовые", "Семейство цветковых растений порядка Гвоздичноцветные"},
                    { "Немезия", "Включает в себя около 50 видов однолетних и многолетних трав,"}
                }
                    },

                    {
                        "Розы",
                        new Dictionary<string, string>()
                {
                    { "Роза Дамасская", "Многолетний кустарник; вид секции Gallicanae рода Шиповник семейства Розовые"},
                    { "Роза Столистная", "Прованская роза или капустная роза или Роза де Май"},
                    { "Роза «Дабл делайт»", "Роза 'Double Delight' - это многократный отмеченный наградой сорт гибридной чайной розы красного цвета"},
                    { "Роза «Грэм томас»", "Сорт назван в честь известного садовода Грэма Стюарта Томаса"}
                }
                    },

                    {
                        "Тюльпаны",
                        new Dictionary<string, string>()
                {
                    { "Тюльпан Остроконечный", "Занесён в Красную книгу России под названием Тюльпан Шренка"},
                    { "Тюльпан Клузиуса", "Распространён в горных районах Ирана, в Северо-Западной Индии"},
                    { "Тюльпан Льнолистный", "Многолетнее травянистое растение; вид подрода Leiostemones рода Тюльпан семейства Лилейные"},
                    { "Тюльпан Систола", "Tulipa systola, пустынный тюльпан, является разновидностью тюльпана, произрастающей на Ближнем Востоке"}
                }
                    }
                };


                foreach (DirectoryInfo category in allCategoriesDirectories)
                {
                    var categoryName = category.Name;
                    var dictionaryNameDescription = nameDescription[categoryName];
                    var index = 0;
                    decimal price = 100.00M;
                    foreach (var image in category.GetFiles())
                    {
                        products.Add(new Product()
                        {
                            ProductId = Guid.NewGuid(),
                            Image = File.ReadAllBytes(image.FullName),
                            CategoryId = categories.FirstOrDefault(f => f.FlowersType == categoryName).CategoryId,
                            Name = dictionaryNameDescription.ElementAt(index).Key, 
                            Description = dictionaryNameDescription.ElementAt(index).Value,
                            Price = price
                        });
                        index++;
                        price += 100.00M;
                    }
                }

                var userId0 = Guid.NewGuid();
                var basketId0 = Guid.NewGuid();
                var userId1 = Guid.NewGuid();
                var basketId1 = Guid.NewGuid();
                var userId2 = Guid.NewGuid();
                var basketId2 = Guid.NewGuid();

                var users = new User[]
                {
                 new User() 
                 {
                     UserId = userId0,
                     Name = "UserOne",
                     SecondName = "SecondName1", 
                     Email = "111@gmail.com", 
                     Password = "admin", 
                     DateCreated = DateTime.Now,
                     Phone = 11111111111
                 },
                 new User()
                 {
                     UserId = userId1,
                     Name = "UserTwo",
                     SecondName = "SecondName2",
                     Email = "222@gmail.com",
                     Password = "admin",
                     DateCreated = DateTime.Now,
                     Phone = 22222222222
                 },
                 new User()
                 {
                     UserId = userId2,
                     Name = "UserTree",
                     SecondName = "SecondName3",
                     Email = "333@gmail.com",
                     Password = "admin",
                     DateCreated = DateTime.Now,
                     Phone = 33333333333
                 }
               
                };

                var baskets = new Basket[]
                {
                    new Basket() { BasketId = basketId0, UserId = userId0, DateCreated = DateTime.Now },
                    new Basket() { BasketId = basketId1, UserId = userId1, DateCreated = DateTime.Now },
                    new Basket() { BasketId = basketId2, UserId = userId2, DateCreated = DateTime.Now }
                };

                Categories.AddRange(categories);
                Products.AddRange(products);
                Users.AddRange(users);
                Baskets.AddRange(baskets);

                SaveChanges();
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

