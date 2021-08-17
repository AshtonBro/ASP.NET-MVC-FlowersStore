using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using FlowersStore.DataAccess.MSSQL.Entities;

namespace FlowersStore.DataAccess.MSSQL.SeedDb
{
    public static class SeedingRelatedData
    {
        public static void Seed(FlowersStoreDbContext context)
        {

            if (!(context.Categories.Any() & context.Products.Any()))
            {
                var categories = new Category[]
                {
                new Category() { Id = Guid.NewGuid(), FlowersType = "Букеты" },
                new Category() { Id = Guid.NewGuid(), FlowersType = "Растения" },
                new Category() { Id = Guid.NewGuid(), FlowersType = "Розы" },
                new Category() { Id = Guid.NewGuid(), FlowersType = "Тюльпаны" }
                };

                var allCategoriesDirectories = Directory.GetDirectories(@"wwwroot\Image\Store\")
                                                        .Select(f => new DirectoryInfo(f));

                List<Product> products = new List<Product>();

                Dictionary<string, Dictionary<string, string>> nameDescription = new Dictionary<string, Dictionary<string, string>>
                {
                    {
                        "Букеты", new Dictionary<string, string>()
                        {
                            { "Пламя любви", "Свадебный букет Пламя любви -  сочный и яркий элемент Вашей незабываемой свадьбы в красных тонах."},
                            { "Альстримерия Микс", "Стойкие цветы альстримерии имеют густую листву и несколько соцветий на одной ветке"},
                            { "Букет для мамы", "Мама - самый дорогой для нас человек. И так хочется иногда порадовать её чем-то необычным"},
                            { "Сборный букет №18", "Разноцветные альстромерии напоминают цветочный фейерверк в честь весеннего настроения"}
                        }
                    },

                    {
                        "Растения", new Dictionary<string, string>()
                        {
                            { "Гардения", "Род назван в честь Александра Гардена, американского натуралиста"},
                            { "Хамедорея", "Род цветковых растений семейства Пальмовые"},
                            { "Аизовые", "Семейство цветковых растений порядка Гвоздичноцветные"},
                            { "Немезия", "Включает в себя около 50 видов однолетних и многолетних трав,"}
                        }
                    },

                    {
                        "Розы", new Dictionary<string, string>()
                        {
                            { "Роза Дамасская", "Многолетний кустарник; вид секции Gallicanae рода Шиповник семейства Розовые"},
                            { "Роза Столистная", "Прованская роза или капустная роза или Роза де Май"},
                            { "Роза «Дабл делайт»", "Роза 'Double Delight' - это многократный отмеченный наградой сорт гибридной чайной розы красного цвета"},
                            { "Роза «Грэм томас»", "Сорт назван в честь известного садовода Грэма Стюарта Томаса"}
                        }
                    },

                    {
                        "Тюльпаны", new Dictionary<string, string>()
                    {
                        { "Тюльпан Остроконечный", "Занесён в Красную книгу России под названием Тюльпан Шренка"},
                        { "Тюльпан Клузиуса", "Распространён в горных районах Ирана, в Северо-Западной Индии"},
                        { "Тюльпан Льнолистный", "Многолетнее травянистое растение; вид подрода Leiostemones рода Тюльпан семейства Лилейные"},
                        { "Тюльпан Систола", "Tulipa systola, пустынный тюльпан, является разновидностью тюльпана, произрастающей на Ближнем Востоке"}
                        }
                    }
                };

                var colorArray = new string[] { "Зелёный", "Жёлтый", "Красный", "Фиолетовый" };

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
                            Id = Guid.NewGuid(),
                            Image = File.ReadAllBytes(image.FullName),
                            CategoryId = categories.FirstOrDefault(f => f.FlowersType == categoryName).Id,
                            Name = dictionaryNameDescription.ElementAt(index).Key,
                            Description = dictionaryNameDescription.ElementAt(index).Value,
                            Color = colorArray[new Random().Next(0, colorArray.Length)],
                            Price = price
                        });

                        index++;

                        price += 100.00M;
                    }
                }

                context.AddRange(categories);
                context.AddRange(products);

                context.SaveChanges();
            }
        }
    }
}
