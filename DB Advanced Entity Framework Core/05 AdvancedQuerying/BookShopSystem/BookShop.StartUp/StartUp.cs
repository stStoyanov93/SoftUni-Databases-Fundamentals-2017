namespace BookShop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using BookShop.Data;
    using BookShop.Initializer;
    using System.Text.RegularExpressions;

    public class StartUp
    {
        static void Main()
        {
            //var input = int.Parse(Console.ReadLine());
            //var input = Console.ReadLine();           

            using (var db = new BookShopContext())
            {
                //DbInitializer.ResetDatabase(db);
                Console.WriteLine(GetMostRecentBooks(db));
            }
        }

        //1
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            
            int type = -1;

            switch (command.ToLower())
            {
                case "minor":
                    type = 0;
                    break;
                case "teen":
                    type = 1;
                    break;
                case "adult":
                    type = 2;
                    break;
            }

            var books = context.Books
                .Where(b => (int)b.AgeRestriction == type)
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToArray();

            return string.Join(Environment.NewLine, books);
        }

        //2
        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.EditionType == Models.EditionType.Gold
                && b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToArray();

            return string.Join(Environment.NewLine, books);
        }

        //3
        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Price > 40M)
                .OrderByDescending(b => b.Price)
                .Select(b => $"{b.Title} - ${b.Price:F2}")
                .ToArray();


            return string.Join(Environment.NewLine, books);
        }

        //4
        public static string GetBooksNotRealeasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToArray();

            return string.Join(Environment.NewLine, books);
        }

        //5
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var cathegories = input.ToLower().Split(new[] { "\t", " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var books = context.Books
                .Where(b => b.BookCategories.Any(bc => cathegories.Contains(bc.Category.Name.ToLower())))
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToArray();

            return string.Join(Environment.NewLine, books);
        }

        //6
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime parsedDate = DateTime.ParseExact(date, "dd-MM-yyyy", null);

            var books = context.Books
                .Where(b => b.ReleaseDate < parsedDate)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => $"{b.Title} - {b.EditionType} - ${b.Price:F2}")
                .ToArray();

            return string.Join(Environment.NewLine, books);
        }

        //7
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            string pattern = $"^.*{input.ToLower()}$";

            var books = context.Books
                .Where(b => Regex.Match(b.Author.FirstName.ToLower(), pattern).Success)
                .Select(b => $"{b.Author.FirstName} {b.Author.LastName}")  
                .OrderBy(a => a)
                .ToHashSet();

            return string.Join(Environment.NewLine, books);
        }

        //8
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            string pattern = $"^.*{input.ToLower()}.*$";

            var books = context.Books
                .Where(b => Regex.Match(b.Title.ToLower(), pattern).Success)
                .OrderBy(b => b.BookId)
                .Select(b => $"{b.Title}")
                .OrderBy(b => b)
                .ToArray();

            return string.Join(Environment.NewLine, books);
        }

        //9
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            string pattern = $"^{input.ToLower()}.*$";

            var books = context.Books
                .Where(b => Regex.Match(b.Author.LastName.ToLower(), pattern).Success)
                .OrderBy(b => b.BookId)
                .Select(b => $"{b.Title} ({b.Author.FirstName} {b.Author.LastName})")              
                .ToArray();

            return string.Join(Environment.NewLine, books);
        }

        //10
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var books = context.Books
                .Where(b => b.Title.Length > lengthCheck)
                .Count();

            return books;
        }

        //11
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var books = context.Authors
                .Select(a => new
                {
                    Name = $"{a.FirstName} {a.LastName}",
                    CopiesCount = a.Books.Select(b => b.Copies).Sum()
                })
                .OrderByDescending(c => c.CopiesCount)
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var author in books)
            {
                string strToAppend = $"{author.Name} - {author.CopiesCount}";
                sb.AppendLine(strToAppend);
            }

            return string.Join(Environment.NewLine, sb);
        }

        //12
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var books = context.Categories
                .Select(c => new
                {
                    CathegoryName = c.Name,
                    TotalProfit = c.CategoryBooks.Select(b => b.Book.Copies * b.Book.Price).Sum()
                })
                .OrderByDescending(b => b.TotalProfit)
                .ThenBy(b => b.CathegoryName)
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var item in books)
            {
                string strToAppend = $"{item.CathegoryName} ${item.TotalProfit}";
                sb.AppendLine(strToAppend);
            }

            return string.Join(Environment.NewLine, sb);
        }

        //13
        public static string GetMostRecentBooks(BookShopContext context)
        {
            var books = context.Categories
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    CathegoryName = c.Name,
                    SelectedBooks = c.CategoryBooks.Select(cb => cb.Book)
                    .OrderByDescending(b => b.ReleaseDate).Take(3)
                })
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var cathegories in books)
            {
                sb.AppendLine($"--{cathegories.CathegoryName}");

                foreach (var book in cathegories.SelectedBooks)
                {
                    string strToAppend = $"{book.Title} ({book.ReleaseDate.Value.Year})";
                    sb.AppendLine(strToAppend);
                }
                
            }

            return string.Join(Environment.NewLine, sb);
        }

        //14
        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010);

            foreach (var item in books)
            {
                item.Price += 5M;
            }

            context.SaveChanges();
        }

        //15
        public static int RemoveBooks(BookShopContext context)
        {
            var booksToRemove = context.Books
                .Where(b => b.Copies < 4200);

            int count = booksToRemove.Count();

            context.RemoveRange(booksToRemove);
            context.SaveChanges();
            return count;
        }
    }
}
