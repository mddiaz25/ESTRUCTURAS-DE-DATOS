namespace ESTRUCTURAS_DE_DATOS;

/// <summary>
/// Represents a generic stack (LIFO: Last In, First Out).
/// </summary>
/// <typeparam name="T">Type of the elements in the stack.</typeparam>
public class Stack<T>
{
    private readonly List<T> _items = [];

    /// <summary>
    /// Gets the number of elements in the stack.
    /// </summary>
    public int Count => _items.Count;

    /// <summary>
    /// Adds an element to the top of the stack.
    /// </summary>
    /// <param name="item">Element to add.</param>
    public void Push(T item)
    {
        _items.Add(item);
    }

    /// <summary>
    /// Removes and returns the top element of the stack.
    /// </summary>
    /// <returns>The top element.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the stack is empty.</exception>
    public T Pop()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Cannot pop from an empty stack.");
        }

        var index = _items.Count - 1;
        var item = _items[index];
        _items.RemoveAt(index);
        return item;
    }

    /// <summary>
    /// Returns the top element without removing it.
    /// </summary>
    /// <returns>The top element.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the stack is empty.</exception>
    public T Peek()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Cannot peek an empty stack.");
        }

        return _items[_items.Count - 1];
    }

    /// <summary>
    /// Determines whether the stack is empty.
    /// </summary>
    /// <returns><see langword="true"/> when the stack has no elements; otherwise, <see langword="false"/>.</returns>
    public bool IsEmpty()
    {
        return _items.Count == 0;
    }

    /// <summary>
    /// Removes all elements from the stack.
    /// </summary>
    public void Clear()
    {
        _items.Clear();
    }
}
