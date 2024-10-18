using System.Globalization;
using System.Xml.Linq;

string currentDirectory = Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName;
var filePath = Path.Combine(currentDirectory, "books.xml");


XElement db = XElement.Load(filePath);
var books = db.Elements("book");

// Select All Book Titles
var bookTitles = books.Select(b => b.Element("title")?.Value).ToList();

Console.WriteLine("---- All Book Titles:\n\t" + string.Join("\n\t", bookTitles));

//Get All Books Priced Under $10
var booksUnder10Dollars = books.Where(b => double.Parse((b.Element("price")?.Value) ?? "0.0") < 10);
Console.WriteLine("---- Books under $10:");
foreach (var book in booksUnder10Dollars)
{
    Console.WriteLine($"\t Title: {book.Element("title")?.Value,-20} - Price: {book.Element("price")?.Value}");
}

//Count the Number of Books by Genre
var booksGrouped = from book in books
    group book by book.Element("genre")?.Value
    into grouped
    select new
    {
        Genre = grouped.Key,
        Books = grouped.Select(b => b.Element("title")?.Value),
        Count = grouped.Count()
    };
Console.WriteLine("---- Books group:");
foreach (var item in booksGrouped)
{
    Console.WriteLine(
        $"\tGenre: {item.Genre,-18} - Count: {item.Count} - Collection: {string.Join(" || ", item.Books)}");
}

//Find all books that were published after January 1, 2001.
var firstDate2001 = new DateTime(2001, 1, 1, 0, 0, 0, DateTimeKind.Utc);
var booksPublishedAfter20210101 = from book in books
    where DateTime.ParseExact(
        book.Element("publish_date")!.Value,
        "yyyy-MM-dd",
        CultureInfo.InvariantCulture
    ).CompareTo(firstDate2001) > 0
    select new
    {
        title = book.Element("title").Value,
        publish_date = book.Element("publish_date").Value
    };
Console.WriteLine("---- All books were published after 2001-01-01:");
foreach (var item in booksPublishedAfter20210101)
{
    Console.WriteLine($"Title: {item.title,-40} - Publish Date: {item.publish_date}");
}

//Find the Most Expensive Book in Each Genre
var mostExpensiveBookGenreGrouped = books.GroupBy(book => book.Element("genre")?.Value).Select(grouped => new
{
    Genre = grouped.Key,
    MostExpensiveBook = grouped.Select(book => new
    {
        Title = book.Element("title")?.Value,
        Price = double.Parse(book.Element("price")?.Value ?? "0.0")
    }).OrderByDescending(arg => arg.Price).First()
});
Console.WriteLine("---- Most expensive book by genre");
foreach (var grouped in mostExpensiveBookGenreGrouped)
{
    Console.WriteLine(
        $"Genre: {grouped.Genre,-20} " + $"- Title: {grouped.MostExpensiveBook.Title,-40} " +
        $"- Price: {grouped.MostExpensiveBook.Price}"
    );
}

//Find Books With Specific Keywords in the Description
var keywords = new[] { "technology", "programmer", "love" };
var searchedBooks = from book in books
    where keywords.Any(s => book.Element("description")?.Value.ToLower().Contains(s.ToLower()) ?? false)
    select book;

Console.WriteLine("---- Search books:");
foreach (var (element, i) in searchedBooks.Select((element, i) => (element, i)))
{
    Console.WriteLine(
        $"\t{i + 1,-3}: {element.Element("title")?.Value,-40} - Desc: {element.Element("description")?.Value}");
}

//Find Authors Who Wrote Multiple Books Across Genres
var authorsWithMultipleGenres =
    books.GroupBy(book => book.Element("author")?.Value).Where(elements =>
        elements.Count() > 1 && elements.Select(book => book.Element("genre")?.Value).Distinct().Count() > 1).Select(
        (grouped, index) =>
            new
            {
                Index = index + 1,
                Author = grouped.Key,
                Genres = grouped.Select(book => book.Element("genre")?.Value).Distinct(),
                Books = grouped.Select(s => s.Element("title")?.Value),
            }
    );
Console.WriteLine($"Authors Who Wrote Multiple Books Across Genres:");
foreach (var grouped in authorsWithMultipleGenres)
{
    Console.WriteLine($"\t{grouped.Index, -3} - Author: {grouped.Author, -15} - Genres: {string.Join(", ",grouped.Genres)} - Books:");
    foreach (var titleBook in grouped.Books)
    {
        Console.WriteLine($"\t\t + {titleBook}");
    }
}