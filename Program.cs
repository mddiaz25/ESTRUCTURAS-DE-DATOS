namespace ESTRUCTURAS_DE_DATOS;

internal class Program
{
    private static void Main(string[] args)
    {
        var stack = new Stack<int>();
        stack.Push(10);
        stack.Push(20);

        Console.WriteLine("=== Stack ===");
        Console.WriteLine($"Count: {stack.Count}");
        Console.WriteLine($"Top: {stack.Peek()}");
        Console.WriteLine($"Pop: {stack.Pop()}");
        Console.WriteLine($"Empty: {stack.IsEmpty()}");

        var hashTable = new HashTable<string, int>();
        hashTable.Add("uno", 1);
        hashTable.Add("dos", 2);

        Console.WriteLine(Environment.NewLine + "=== HashTable ===");
        Console.WriteLine($"Count: {hashTable.Count}");
        Console.WriteLine($"Get('uno'): {hashTable.Get("uno")}");
        Console.WriteLine($"ContainsKey('dos'): {hashTable.ContainsKey("dos")}");
        Console.WriteLine($"Remove('dos'): {hashTable.Remove("dos")}");
    }
}
