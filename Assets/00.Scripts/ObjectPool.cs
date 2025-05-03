using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private readonly Stack<T> pool = new();
    private readonly T prefab;
    private readonly Transform parent;

    public ObjectPool(T prefab, int size, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < size; i++)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.SetActive(false);
            pool.Push(obj);
        }
    }

    public T Get()
    {
        return pool.Count > 0 ? Activate(pool.Pop()) : Activate(GameObject.Instantiate(prefab, parent));
    }

    public void Release(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Push(obj);
    }

    private T Activate(T obj)
    {
        obj.gameObject.SetActive(true);
        return obj;
    }
}
