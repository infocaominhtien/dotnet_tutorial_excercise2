using System.Diagnostics;


// Generate a list of 1 million numbers
List<int> numbers = Enumerable.Range(1, 10_000_000).ToList();

// Test Version 1: pagingNumbersSol0
Console.WriteLine("Running Version 1 (pagingNumbersSol0)...");
var stopwatch = Stopwatch.StartNew();
TestVersion1(numbers);
stopwatch.Stop();
Console.WriteLine($"Version 1 runtime: {stopwatch.ElapsedMilliseconds} ms\n");

// Test Version 2: pagingNumbersSol0_1
Console.WriteLine("Running Version 2 (pagingNumbersSol0_1)...");
stopwatch.Restart();
TestVersion2(numbers);
stopwatch.Stop();
Console.WriteLine($"Version 2 runtime: {stopwatch.ElapsedMilliseconds} ms\n");

// Test Version 3: pagingNumbers1
Console.WriteLine("Running Version 3 (pagingNumbers1)...");
stopwatch.Restart();
TestVersion3(numbers);
stopwatch.Stop();
Console.WriteLine($"Version 3 runtime: {stopwatch.ElapsedMilliseconds} ms\n");

// Test Version 4: pagingNumbersSol2
Console.WriteLine("Running Version 4 (pagingNumbersSol2)...");
stopwatch.Restart();
TestVersion4(numbers);
stopwatch.Stop();
Console.WriteLine($"Version 4 runtime: {stopwatch.ElapsedMilliseconds} ms\n");

// Version 1: pagingNumbersSol0
void TestVersion1(List<int> numbers)
{
    var pagingNumbersSol0 = new List<IEnumerable<int>>();
    for (int i = 0; i < numbers.Count; i += 3)
    {
        pagingNumbersSol0.Add(numbers.Skip(i).Take(3));
    }

    // Simulate processing of each page
    foreach (var page in pagingNumbersSol0)
    {
        var temp = string.Join(", ", page);
    }
}

// Version 2: pagingNumbersSol0_1
void TestVersion2(List<int> numbers)
{
    var pagingNumbersSol0_1 = Enumerable.Range(0, (int)Math.Ceiling(1.0 * numbers.Count / 3))
        .Select(index => new { Page = index + 1, Items = numbers.Skip(index * 3).Take(3) });

    // Simulate processing of each page
    foreach (var page in pagingNumbersSol0_1)
    {
        var temp = string.Join(", ", page.Items);
    }
}

// Version 3: pagingNumbers1
void TestVersion3(List<int> numbers)
{
    var pagingNumbers1 = numbers
        .Select((number, index) => new { number, Page = index / 3 + 1 })
        .GroupBy(item => item.Page)
        .Select(group => new
        {
            PageIndex = group.Key,
            Items = group.Select(item => item.number)
        });

    // Simulate processing of each page
    foreach (var page in pagingNumbers1)
    {
        var temp = string.Join(", ", page.Items);
    }
}

// Version 4: pagingNumbersSol2 (Using Chunk in .NET 6+)
void TestVersion4(List<int> numbers)
{
    var pagingNumbersSol2 = numbers.Chunk(3).Select((chunk, index) => (chunk, index + 1));

    // Simulate processing of each page
    foreach (var (page, index) in pagingNumbersSol2)
    {
        var temp = string.Join(", ", page);
    }
}