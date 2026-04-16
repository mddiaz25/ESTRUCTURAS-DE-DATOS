namespace ESTRUCTURAS_DE_DATOS;

/// <summary>
/// Represents a generic hash table with collision handling by chaining.
/// </summary>
/// <typeparam name="TKey">Type of the key.</typeparam>
/// <typeparam name="TValue">Type of the value.</typeparam>
public class HashTable<TKey, TValue> where TKey : notnull
{
    private readonly LinkedList<KeyValuePair<TKey, TValue>>?[] _buckets;

    /// <summary>
    /// Initializes a new instance of the <see cref="HashTable{TKey, TValue}"/> class.
    /// </summary>
    /// <param name="capacity">Number of buckets to initialize.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when capacity is less than 1.</exception>
    public HashTable(int capacity = 16)
    {
        if (capacity < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be greater than zero.");
        }

        _buckets = new LinkedList<KeyValuePair<TKey, TValue>>?[capacity];
    }

    /// <summary>
    /// Gets the number of key-value pairs stored in the hash table.
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    /// Adds a key-value pair to the hash table.
    /// </summary>
    /// <param name="key">Key to add.</param>
    /// <param name="value">Value associated with the key.</param>
    /// <exception cref="ArgumentNullException">Thrown when key is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown when key already exists.</exception>
    public void Add(TKey key, TValue value)
    {
        if (key is null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        var index = GetBucketIndex(key);
        _buckets[index] ??= new LinkedList<KeyValuePair<TKey, TValue>>();

        var bucket = _buckets[index]!;
        foreach (var pair in bucket)
        {
            if (EqualityComparer<TKey>.Default.Equals(pair.Key, key))
            {
                throw new ArgumentException("An item with the same key has already been added.", nameof(key));
            }
        }

        bucket.AddLast(new KeyValuePair<TKey, TValue>(key, value));
        Count++;
    }

    /// <summary>
    /// Gets the value associated with a key.
    /// </summary>
    /// <param name="key">Key to search.</param>
    /// <returns>The value associated with the key.</returns>
    /// <exception cref="ArgumentNullException">Thrown when key is <see langword="null"/>.</exception>
    /// <exception cref="KeyNotFoundException">Thrown when key does not exist.</exception>
    public TValue Get(TKey key)
    {
        if (key is null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        var index = GetBucketIndex(key);
        var bucket = _buckets[index];
        if (bucket is null)
        {
            throw new KeyNotFoundException("The given key was not present in the hash table.");
        }

        foreach (var pair in bucket)
        {
            if (EqualityComparer<TKey>.Default.Equals(pair.Key, key))
            {
                return pair.Value;
            }
        }

        throw new KeyNotFoundException("The given key was not present in the hash table.");
    }

    /// <summary>
    /// Removes a key-value pair by key.
    /// </summary>
    /// <param name="key">Key to remove.</param>
    /// <returns><see langword="true"/> if the key existed and was removed; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when key is <see langword="null"/>.</exception>
    public bool Remove(TKey key)
    {
        if (key is null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        var index = GetBucketIndex(key);
        var bucket = _buckets[index];
        if (bucket is null)
        {
            return false;
        }

        var node = bucket.First;
        while (node is not null)
        {
            if (EqualityComparer<TKey>.Default.Equals(node.Value.Key, key))
            {
                bucket.Remove(node);
                Count--;
                return true;
            }

            node = node.Next;
        }

        return false;
    }

    /// <summary>
    /// Determines whether a key exists in the hash table.
    /// </summary>
    /// <param name="key">Key to search.</param>
    /// <returns><see langword="true"/> when key exists; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when key is <see langword="null"/>.</exception>
    public bool ContainsKey(TKey key)
    {
        if (key is null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        var index = GetBucketIndex(key);
        var bucket = _buckets[index];
        if (bucket is null)
        {
            return false;
        }

        foreach (var pair in bucket)
        {
            if (EqualityComparer<TKey>.Default.Equals(pair.Key, key))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Removes all key-value pairs from the hash table.
    /// </summary>
    public void Clear()
    {
        for (var i = 0; i < _buckets.Length; i++)
        {
            _buckets[i] = null;
        }

        Count = 0;
    }

    private int GetBucketIndex(TKey key)
    {
        return (key.GetHashCode() & int.MaxValue) % _buckets.Length;
    }
}
