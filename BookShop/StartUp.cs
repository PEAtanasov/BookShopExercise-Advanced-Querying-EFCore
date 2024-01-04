namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using System.Runtime.Serialization;
    using System.Text;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            DbInitializer.ResetDatabase(db);

            //Task 1
            /*string command = Console.ReadLine();
            string resultTask1 = GetBooksByAgeRestriction(db, command);
            Console.WriteLine(resultTask1);*/

            //Task 2
            /*string resultTask2 = GetGoldenBooks(db);
            Console.WriteLine(resultTask2);
*/

            //Task 3
            /*string resultTask3 = GetBooksByPrice(db);
            Console.WriteLine(resultTask3);*/

            //Task 4
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
    }
}


