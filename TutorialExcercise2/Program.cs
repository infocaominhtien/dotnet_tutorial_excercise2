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

// 2. **First Match**: Use LINQ to find the first fruit in the `fruits` list that starts with the letter 'c'.

// 3. **Aggregation**: Use LINQ to calculate the average age of all people in the `people` list.

// 4. **Sorting**: Use LINQ to sort the `people` list by name in descending order.

// 5. **Grouping and Counting**: Use LINQ to group the numbers in the `numbers` list by even/odd and count how many are in each group.

// 6. **Transformation**: Use LINQ to create a new list of strings from the `people` list, where each string is in the format "Name is Age years old".

// 7. **Multiple Operations**: Use LINQ to find the names of all people in the `people` list who are older than the average age, sorted alphabetically.

// 8. **Quantifiers**: Use LINQ to check if all fruits in the `fruits` list have more than 3 characters.

// 9. **Set Operations**: Create another list of fruits and use LINQ to find the fruits that are in both lists.

// 10. **Pagination**: Use LINQ to get the second page of results from the `numbers` list, assuming each page has 3 items.

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
