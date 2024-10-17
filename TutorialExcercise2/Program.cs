List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
List<string> fruits = new List<string> { "apple", "banana", "cherry", "date", "elderberry" };
List<Person> people = new List<Person>
{
    new("Alice", 25),
    new("Bob", 30),
    new("Charlie", 35),
    new("David", 40),
    new("Eve", 45)
};

// 1. **Filtering**: Use LINQ to find all even numbers in the `numbers` list.
var evenNumbers = numbers.Where(n => n % 2 == 0);
Console.WriteLine($"Even numbers: {string.Join(", ", evenNumbers)}");

// 2. **First Match**: Use LINQ to find the first fruit in the `fruits` list that starts with the letter 'c'.
var findFruit = fruits.FirstOrDefault(f => f.StartsWith('c'));
Console.WriteLine($"First fruit that starts with 'c': {findFruit}");

// 3. **Aggregation**: Use LINQ to calculate the average age of all people in the `people` list.
// double ageAverage = people.Sum(p => p.Age) / people.Count;
double ageAverage = people.Average(p => p.Age);
Console.WriteLine($"Average age: {ageAverage}");

// 4. **Sorting**: Use LINQ to sort the `people` list by name in descending order.
List<Person> sortedPersons = people.OrderByDescending(p => p.Name).ToList();
Console.WriteLine("Sorted by name in descending order:");
foreach (var person in sortedPersons)
{
    Console.WriteLine($"\tName: {person.Name}, Age: {person.Age}");
}

// 5. **Grouping and Counting**: Use LINQ to group the numbers in the `numbers` list by even/odd and count how many are in each group.
// var groupedNumber = from number in numbers
//     group number by (number % 2 == 0)
//     into grouped
//     select new
//     {
//         IsEven = grouped.Key,
//         Count = grouped.Count()
//     };
var groupedNumber = numbers.GroupBy(c => c % 2 == 0).Select(item => new
{
    IsEven = item.Key,
    Count = item.Count()
});
Console.WriteLine("In number list: ");
foreach (var item in groupedNumber)
{
    Console.WriteLine($"\tNumber of {(item.IsEven ? "even" : "odd")} number is {item.Count}");
}

// 6. **Transformation**: Use LINQ to create a new list of strings from the `people` list, where each string is in the format "Name is Age years old".
List<string> stringPeopleInfoList = people.Select(item => $"{item.Name} is {item.Age} years old").ToList();
Console.WriteLine("People information:");
foreach (var info in stringPeopleInfoList)
{
    Console.WriteLine("\t" + info);
}

// 7. **Multiple Operations**: Use LINQ to find the names of all people in the `people` list who are older than the average age, sorted alphabetically.
IEnumerable<string> olderThanAvgPersons =
    people.Where(p => p.Age > ageAverage).OrderBy(p => p.Name).Select(p => p.Name);
Console.WriteLine(
    $"These are all people, who are older than the average age, in the list: \n\t{string.Join(", ", olderThanAvgPersons)}");

// 8. **Quantifiers**: Use LINQ to check if all fruits in the `fruits` list have more than 3 characters.
bool isAllFruitsHaveMore3Chars = fruits.All(s => s.Length > 3);
Console.WriteLine($"Do all fruits have more than 3 characters? {isAllFruitsHaveMore3Chars}");

// 9. **Set Operations**: Create another list of fruits and use LINQ to find the fruits that are in both lists.
List<string> anotherFruits = new List<string> { "apple", "banana", "cherry", "mango", "orange" };
var commonFruitsIn2List = from item1 in fruits join item2 in anotherFruits on item1 equals item2 select item1;
// var commonFruitsIn2List = fruits.Intersect(anotherFruits);
Console.WriteLine($"Common fruits in 2 list: {string.Join(", ", commonFruitsIn2List)}");

// 10. **Pagination**: Use LINQ to get the second page of results from the `numbers` list, assuming each page has 3 items.
List<IEnumerable<int>> pagingNumbersSol0 = new List<IEnumerable<int>>();
for (int count = 0; count < numbers.Count; count += 3)
{
    pagingNumbersSol0.Add(numbers.Skip(count).Take(3));
}

Console.WriteLine("Paging numbers solution 0:");
for (int i = 0; i < pagingNumbersSol0.Count; i++)
{
    Console.WriteLine($"\tPage {i + 1}: {string.Join(", ", pagingNumbersSol0[i])}");
}

var pagingNumbersSol0_1 =
    Enumerable.Range(0, (int)Math.Ceiling(1.0 * numbers.Count / 3))
        .Select(index => new { index = index + 1, items = numbers.Skip(index * 3).Take(3) });
Console.WriteLine("Paging numbers solution 0_1:");
foreach (var page in pagingNumbersSol0_1)
{
    Console.WriteLine($"\tPage {page.index}: {string.Join(", ", page.items)}");
}

var pagingNumbers1 = numbers.Select((number, index) => (number, index)).GroupBy(item => item.index / 3).Select(
    grouped =>
        new
        {
            PageIndex = grouped.Key + 1,
            Items = grouped.Select(item => item.number)
        });
Console.WriteLine("Paging numbers solution 1:");
foreach (var page in pagingNumbers1)
{
    Console.WriteLine($"\tPage {page.PageIndex}: {string.Join(", ", page.Items)}");
}

var pagingNumbersSol2 = numbers.Chunk(3).Select((chunk, index) => (chunk, index));
Console.WriteLine("Paging numbers solution 2:");
foreach (var (page, index) in pagingNumbersSol2)
{
    Console.WriteLine($"\tPage {index + 1}: {string.Join(", ", page)}");
}

class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}