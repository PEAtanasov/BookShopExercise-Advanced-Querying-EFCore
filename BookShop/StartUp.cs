namespace BookShop
{
    using BookShop.Models;
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            DbInitializer.ResetDatabase(db);

            //Task 2 - Age Restriction
            /*string command = Console.ReadLine();
            string resultTask2 = GetBooksByAgeRestriction(db, command);
            Console.WriteLine(resultTask2);*/

            //Task 3 - Golden Books
            /*string resultTask3 = GetGoldenBooks(db);
            Console.WriteLine(resultTask3);
*/

            //Task 4 - Books By Price
            /*string resultTask4 = GetBooksByPrice(db);
            Console.WriteLine(resultTask4);*/

            //Task 5 - Not Released In
            /*string resultTask5 = GetBooksNotReleasedIn(db, int.Parse(Console.ReadLine()));
            Console.WriteLine(resultTask5);*/

            //Task 6 - Book Titles by Category
            /*string resultTask6 = GetBooksByCategory(db,Console.ReadLine());
            Console.WriteLine(resultTask6);*/

            //Task 7 - Released Before Date
            //string date = "12-04-2000";
            //string dateinBase = "22-03-2001";
            //DateTime date1 = DateTime.ParseExact(date, "dd-MM-yyyy",CultureInfo.InvariantCulture);
           // DateTime dateinBase1 = DateTime.ParseExact(dateinBase, "dd-MM-yyyy", CultureInfo.InvariantCulture);
           // Console.WriteLine(date1>dateinBase1);
            string resultTask7 = GetBooksReleasedBefore(db, Console.ReadLine());
            Console.WriteLine(resultTask7);

        }
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var books = context.Books
                .ToArray()
                .Where(b => (b.AgeRestriction.ToString().ToLower()) == command.ToLower())
                .Select(b => b.Title)
                .ToArray();

            StringBuilder sb = new StringBuilder();
            foreach (var book in books.OrderBy(b=>b))
            {
                sb.AppendLine(book);
            }
            return sb.ToString().TrimEnd();
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books
            .ToArray()
                .Where(b => (b.EditionType.ToString().ToLower()) == "gold".ToLower())       
                .Where(b=>b.Copies<5000)
                .ToArray();

            StringBuilder sb = new StringBuilder();
            foreach (var book in books.OrderBy(b => b.BookId))
            {
                sb.AppendLine(book.Title);
            }
            return sb.ToString().TrimEnd();
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(b =>b.Price>40)
                .ToArray();

            StringBuilder sb = new StringBuilder();
            foreach (var book in books.OrderByDescending(b => b.Price))
            {
                sb.AppendLine(book.Title + " - "+ "$" + $"{book.Price:f2}");
            }
            return sb.ToString().TrimEnd();
        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                .Where(b => (b.ReleaseDate).Value.Year!=year)
                .ToArray();

            StringBuilder sb = new StringBuilder();
            foreach (var book in books.OrderBy(b => b.BookId))
            {
                sb.AppendLine(book.Title);
            }
            return sb.ToString().TrimEnd();
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[] inputCategories = input
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(c=>c.ToLower())
                .ToArray();
            var bookTitles = context.Books
                .Where(b => b.BookCategories
                       .Any(bc => inputCategories.Contains(bc.Category.Name.ToLower())))
                .OrderBy(b=>b.Title)
                .Select(b=>b.Title)
                .ToArray();
           

            StringBuilder sb = new StringBuilder();
            foreach (var book in bookTitles)
            {
                sb.AppendLine(book);
            }
            return sb.ToString().TrimEnd();
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var dateToWorkWith = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var books = context.Books
                .Where(b => b.ReleaseDate < dateToWorkWith)
                .OrderByDescending(b=>b.ReleaseDate)
                .Select(b => new
                {
                    b.Title,
                    edition = b.EditionType.ToString(),
                    b.Price,
                    b.ReleaseDate
                })
                .ToArray();
            
            StringBuilder sb = new();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.edition} - ${book.Price:f2}");
            }

            return sb .ToString().TrimEnd();
        }
    }
}


