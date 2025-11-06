using System.Collections.Generic;
using UnityEngine;

public abstract class RuntimeSetSO<T> : ScriptableObject where T : ScriptableObject
{
    private List<T> items = new List<T>();

    public IReadOnlyList<T> Items => items;

    public void Clear()
    {
        items.Clear();
    }

    public void Add(T item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
        }
    }

    public void Remove(T item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
        }
    }
}
