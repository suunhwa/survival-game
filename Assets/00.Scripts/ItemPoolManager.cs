using System.Collections.Generic;
using UnityEngine;

public class ItemPoolManager : MonoBehaviour
{
    public static ItemPoolManager Instance { get; private set; }

    [SerializeField] private DroppedItem woodPrefab;
    [SerializeField] private DroppedItem stonePrefab;
    [SerializeField] private GameObject iconPrefab;

    private Dictionary<string, ObjectPool<DroppedItem>> pools;

    void Awake()
    {
        if(Instance == null) 
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        pools = new Dictionary<string, ObjectPool<DroppedItem>>
        {
            {"Wood", new ObjectPool<DroppedItem> (woodPrefab, 20) },
            {"Stone", new ObjectPool<DroppedItem> (stonePrefab, 20) }
        };
    }

    public DroppedItem Spawn(string name, string type, int count, Vector3 pos)
    {
        if (!pools.TryGetValue(name, out var pool))
        {
            Debug.LogWarning($"[ItemPoolManager] '{name}'에 대한 Pool 없음");
            return null;
        }

        DroppedItem item = pool.Get();
        item.transform.position = pos;
        item.Setup(name, type, count, iconPrefab);
        return item;
    }

    public void Return(DroppedItem item)
    {
        if (pools.TryGetValue(item.itemName, out var pool))
            pool.Release(item);
        else
            Destroy(item.gameObject);
    }
}
