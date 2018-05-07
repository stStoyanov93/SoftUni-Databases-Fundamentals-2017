using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

using ProductsShop.Data;
using ProductsShop.Models;
using System.Collections.Generic;

namespace ProductsShop.App
{
    class StartUp
    {
        static void Main(string[] args)
        {
            //ResetDatabase();

            //Json Import
            //Console.WriteLine(ImportUsersFromJson());
            //Console.WriteLine(ImportCategoriesFromJson());
            //Console.WriteLine(ImportProductsFromJson());
            //Console.WriteLine(SetCategories());

            //JsonE xport
            //GetProductsInRange();
            //GetSuccessfullySolfProducts();
            //GetCategoriesByProductCount();
            //GetUsersAndProducts();

            //XML Import
            //Console.WriteLine(ImportUsersFromXML());
            //Console.WriteLine(ImportCategoriesFromXML());
            //Console.WriteLine(ImportProductsFromXml());

            //XML Export
            //GetProductsInRangeXml();
            //GetSoldProducts();
            //GetCategoriesByProductsCountXml();
            //GetUsersAndProductsXml();
        }

        private static void ResetDatabase()
        {
            using (var context = new ProductsShopContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }

        // Json Import

        static T[] ImportJason<T>(string path)
        {
            string jsonString = File.ReadAllText(path);

            T[] objects = JsonConvert.DeserializeObject<T[]>(jsonString);

            return objects;
        }

        static string ImportUsersFromJson()
        {
            User[] users = ImportJason<User>("Files/users.json");

            using (var context = new ProductsShopContext())
            {
                context.Users.AddRange(users);
                context.SaveChanges();
            }

            return $"{users.Length} users were imported";
        }
        
        static string ImportCategoriesFromJson()
        {
            Category[] categories = ImportJason<Category>("Files/categories.json");

            using (var context = new ProductsShopContext())
            {
                context.AddRange(categories);
                context.SaveChanges();
            }

            return $"{categories.Length} categories were imported";
        }

        static string ImportProductsFromJson()
        {
            Product[] products = ImportJason<Product>("Files/products.json");

            Random rnd = new Random();

            using (var context = new ProductsShopContext())
            {
                int[] userIds = context.Users.Select(u => u.Id).ToArray();       

                foreach (var p in products)
                {
                    int sellerId = userIds[rnd.Next(0, userIds.Length)];

                    int? buyerId = sellerId;

                    while (buyerId == sellerId)
                    {
                        int buyerIndex = rnd.Next(0, userIds.Length);
                        buyerId = userIds[buyerIndex];
                    }

                    if (buyerId - sellerId < 4)
                    {
                        buyerId = null;
                    }

                    p.SellerId = sellerId;
                    p.BuyerId = buyerId;
                }

                context.AddRange(products);
                context.SaveChanges();
            }

            return $"{products.Length} products were imported";
        }

        static string SetCategories()
        {
            int categoryProductsCount = 0;

            using (var context = new ProductsShopContext())
            {
                var productIds = context.Products.Select(p => p.Id).ToArray();
                var categoryIds = context.Categories.Select(c => c.Id).ToArray();

                Random random = new Random();

                var categoryProducts = new List<CategoryProduct>();

                foreach (var productId in productIds)
                {
                    var index = random.Next(0, categoryIds.Length);
                    var categoryId = categoryIds[index];

                    var currentCategoryProduct = new CategoryProduct()
                    {
                        ProductId = productId,
                        CategoryId = categoryId
                    };

                    categoryProducts.Add(currentCategoryProduct);
                }

                categoryProductsCount = categoryProducts.Count();

                context.CategoriesProducts.AddRange(categoryProducts);
                context.SaveChanges();
            }

            return $"{categoryProductsCount} categories were added to products.";
        }


        //Json Export

        static void GetProductsInRange()
        {
            using (var context = new ProductsShopContext())
            {
                var products = context.Products
                    .Where(p => p.Price >= 500 && p.Price <= 1000)
                    .OrderBy(p => p.Price)
                    .Select(p => new
                    {
                        p.Name,
                        p.Price,
                        Seller = $"{p.Seller.FirstName} {p.Seller.LastName}"
                    })
                    .ToArray();

                var jsonString = JsonConvert.SerializeObject(products, 
                    Formatting.Indented, 
                    new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore});

                File.WriteAllText("JsonOutputFiles/PricesInRange.json", jsonString);
            }
        }

        static void GetSuccessfullySolfProducts()
        {
            using (var context = new ProductsShopContext())
            {
                var users = context.Users
                    .Where(u => u.SoldProducts
                        .Any(p => p.BuyerId != null))
                    .OrderBy(u => u.LastName)
                    .ThenBy(u => u.FirstName)
                    .Select(u => new
                    {
                        u.FirstName,
                        u.LastName,
                        SoldProducts = u.SoldProducts
                            .Select(p => new
                            {
                                p.Name,
                                p.Price,
                                p.Buyer.FirstName,
                                p.Buyer.LastName
                            })
                    })
                    .ToArray();

                var jsonString = JsonConvert.SerializeObject(users, 
                    Formatting.Indented,
                    new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });

                File.WriteAllText("JsonOutputFiles/SoldProducts.json", jsonString);
            }
        }

        static void GetCategoriesByProductCount()
        {
            using (var context = new ProductsShopContext())
            {
                var categories = context.Categories
                    .OrderBy(c => c.Name)
                    .Select(c => new
                    {
                        c.Name,
                        c.Products.Count,
                        AveragePrice = c.Products.Select(p => p.Product.Price).Average(),
                        TotalSum = c.Products.Select(p => p.Product.Price).Sum()
                    })
                    .ToArray();

                var jsonString = JsonConvert.SerializeObject(categories,
                    Formatting.Indented,
                    new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });

                File.WriteAllText("JsonOutputFiles/CategoriesByProductCount.json", jsonString);
            }
        }

        static void GetUsersAndProducts()
        {
            using (var db = new ProductsShopContext())
            {
                var users = db.Users
                    .Where(u => u.SoldProducts.Any(b => b.BuyerId != null))
                    .Select(u => new
                    {
                        firstName = u.FirstName,
                        lastName = u.LastName,
                        age = u.Age,
                        soldProducts = u.SoldProducts.Select(ps => new
                        {
                            count = u.SoldProducts.Count(),
                            products = u.SoldProducts.Where(p => p.BuyerId != null)
                            .Select(p => new
                            {
                                name = p.Name,
                                price = p.Price
                            })
                        })
                    })
                    .ToArray()
                    .OrderByDescending(u => u.soldProducts.Count())
                    .ThenBy(u => u.lastName);

                var usersToJson = new
                {
                    usersCount = users.Count(),
                    users
                };

                var jsonString = JsonConvert.SerializeObject(usersToJson,
                    Formatting.Indented,
                    new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore });

                File.WriteAllText("JsonOutputFiles/UsersAndProducts.json", jsonString);

            }
        }

        //XML Import
        static string ImportUsersFromXML()
        {
            string xmlString = File.ReadAllText("Files/users.xml");

            var xmlDoc = XDocument.Parse(xmlString);

            var elements = xmlDoc.Root.Elements();

            var users = new List<User>();

            foreach (var e in elements)
            {
                string firstName = e.Attribute("firstName")?.Value;
                string lastName = e.Attribute("lastName").Value;

                int? age = null;

                if (e.Attribute("age") != null)
                {
                    age = int.Parse(e.Attribute("age").Value);
                }

                var user = new User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age
                };

                users.Add(user);
            }

            using (var context = new ProductsShopContext())
            {
                context.Users.AddRange(users);
                context.SaveChanges();
            }

            return $"{users.Count} users were imported";
        }

        static string ImportCategoriesFromXML()
        {
            string xmlString = File.ReadAllText("Files/categories.xml");

            var xmlDoc = XDocument.Parse(xmlString);

            var elements = xmlDoc.Root.Elements();

            var categories = new List<Category>();

            foreach (var e in elements)
            {
                var category = new Category()
                {
                    Name = e.Element("name").Value,
                };

                categories.Add(category);
            }

            using (var context = new ProductsShopContext())
            {
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            return $"{categories.Count} categories were imported";
        }

        static string ImportProductsFromXml()
        {
            string xmlString = File.ReadAllText("Files/products.xml");

            var xmlDoc = XDocument.Parse(xmlString);

            var elements = xmlDoc.Root.Elements();

            var catProducts = new List<CategoryProduct>();

            using (var db = new ProductsShopContext())
            {
                var userIds = db.Users.Select(u => u.Id).ToArray();
                var categoryIds = db.Categories.Select(c => c.Id).ToArray();

                Random random = new Random();

                foreach (var e in elements)
                {
                    string name = e.Element("name").Value;
                    decimal price = decimal.Parse(e.Element("price").Value);

                    int sellIndex = random.Next(0, userIds.Length);
                    int sellerId = userIds[sellIndex];

                    int? buyerId = sellerId;
                    while (buyerId == sellerId)
                    {
                        int buyerIndex = random.Next(0, userIds.Length);
                        buyerId = userIds[buyerIndex];

                        if (buyerId % 6 == 0 || buyerId % 5 == 0 || buyerId % 13 == 0)
                        {
                            buyerId = null;
                        }
                    }

                    var product = new Product()
                    {
                        Name = name,
                        Price = price,
                        SellerId = sellerId,
                        BuyerId = buyerId
                    };

                    int catIndex = random.Next(0, categoryIds.Length);
                    int categoryId = categoryIds[catIndex];

                    var catProduct = new CategoryProduct()
                    {
                        Product = product,
                        CategoryId = categoryId
                    };

                    catProducts.Add(catProduct);
                }

                db.AddRange(catProducts);
                db.SaveChanges();
            }
            return $"{catProducts.Count} products were imported";
        }

        //XML Export
        static void GetProductsInRangeXml()
        {
            using (var db = new ProductsShopContext())
            {
                var products = db.Products
                    .Where(p => p.Price >= 1000 && p.Price <= 2000 && p.BuyerId != null)
                    .OrderBy(p => p.Price)
                    .Select(p => new
                    {
                        productName = p.Name,
                        price = p.Price,
                        buyer = $"{p.Buyer.FirstName} {p.Buyer.LastName}"
                    }).ToArray();

                var xDoc = new XDocument();
                xDoc.Add(new XElement("products"));

                foreach (var p in products)
                {
                    xDoc.Root.Add(
                        new XElement("product",
                            new XAttribute("name", $"{p.productName}"),
                            new XAttribute("price", $"{p.price}"),
                            new XAttribute("buyer", $"{p.buyer}")));
                }


                xDoc.Save("XMLOutputFiles/ProductsInRange.xml");
            }
        }

        static void GetSoldProducts()
        {
            using (var db = new ProductsShopContext())
            {
                var users = db.Users
                    .Where(u => u.SoldProducts.Any(p => p.Buyer != null))
                    .OrderBy(u => u.LastName)
                    .ThenBy(u => u.FirstName)
                    .Select(u => new
                    {
                        u.FirstName,
                        u.LastName,
                        SoldProducts = u.SoldProducts
                                        .Where(ps => ps.BuyerId != null)
                                        .Select(p => new
                                        {
                                            p.Name,
                                            p.Price,
                                        })
                    })
                    .ToArray();

                var xDoc = new XDocument();
                xDoc.Add(new XElement("users"));

                foreach (var u in users)
                {
                    xDoc.Root.Add(
                        new XElement("user",
                            new XAttribute("first-name", $"{u.FirstName}"),
                            new XAttribute("last-name", $"{u.LastName}"),
                            new XElement("sold-products")));

                    var pElements = xDoc.Root.Elements()
                        .SingleOrDefault(e => e.Name == "user" &&
                                              e.Attribute("first-name").Value == $"{u.FirstName}" &&
                                              e.Attribute("last-name").Value == $"{u.LastName}")
                        .Element("sold-products");

                    foreach (var p in u.SoldProducts)
                    {
                        pElements.Add(new XElement("product",
                                        new XElement("name", $"{p.Name}"),
                                        new XElement("price", $"{p.Price}")));
                    }
                }

                xDoc.Save("XMLOutputFiles/SoldProducts.xml");
            }
        }

        static void GetCategoriesByProductsCountXml()
        {
            using (var db = new ProductsShopContext())
            {
                var categories = db.Categories
                    .OrderBy(c => c.Products.Count())
                    .Select(c => new
                    {
                        categoryName = c.Name,
                        productsCount = c.Products.Count(),
                        averagePrice = c.Products.Select(cp => cp.Product.Price).Average(),
                        totalRevenue = c.Products.Select(cp => cp.Product.Price).Sum()
                    })
                    .ToArray();

                var xDoc = new XDocument();
                xDoc.Add(new XElement("categories"));

                foreach (var c in categories)
                {
                    xDoc.Root.Add(
                        new XElement("category",
                            new XAttribute("name", $"{c.categoryName}"),
                                new XElement("products-count", $"{c.productsCount}"),
                                new XElement("average-price", $"{c.averagePrice}"),
                                new XElement("total-revenue", $"{c.totalRevenue}")));
                }

                xDoc.Save("XMLOutputFiles/CategoriesByProductsCount.xml");
            }
        }

        static void GetUsersAndProductsXml()
        {
            using (var db = new ProductsShopContext())
            {
                var users = db.Users
                    .Where(u => u.SoldProducts.Any(b => b.BuyerId != null))
                    .OrderByDescending(u => u.SoldProducts.Count())
                    .ThenBy(u => u.LastName)
                    .Select(u => new
                    {
                        firstName = u.FirstName,
                        lastName = u.LastName,
                        age = u.Age,
                        soldProducts = u.SoldProducts.Select(ps => new
                        {
                            count = ps.Seller.SoldProducts.Count(),
                            name = ps.Name,
                            price = ps.Price
                        })
                    }).ToList();

                XDocument xDoc = new XDocument();
                xDoc.Add(new XElement("users", new XAttribute("count", $"{users.Count}")));


                foreach (var u in users)
                {
                    xDoc.Root.Add(new XElement("user",
                        new XAttribute("first-name", $"{u.firstName}"),
                        new XAttribute("last-name", $"{u.lastName}"),
                        new XAttribute("age", $"{u.age}"),
                            new XElement("sold-products",
                            new XAttribute("count", $"{u.soldProducts.Count()}"))));

                    var element =
                        xDoc.Root.Elements()
                        .SingleOrDefault(e => e.Name == "user"
                        && e.Attribute("first-name").Value == $"{u.firstName}"
                        && e.Attribute("last-name").Value == $"{u.lastName}"
                        && e.Attribute("age").Value == $"{u.age}")
                        .Elements()
                        .SingleOrDefault(e => e.Name == "sold-products");

                    foreach (var p in u.soldProducts)
                    {
                        element.Add(new XElement("product",
                                        new XAttribute("name", $"{p.name}"),
                                        new XAttribute("price", $"{p.price}")));
                    }
                }

                xDoc.Save("XMLOutputFiles/UsersAndProducts.xml");

            }
        }
    }
}
