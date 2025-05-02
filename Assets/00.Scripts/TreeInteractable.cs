using UnityEngine;

public class TreeInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject logPrefab;

    public void Interact()
    {
        int logCount = Random.Range(2, 5);
        for (int i = 0; i < logCount; i++)
        {
            Vector3 offset = Random.insideUnitSphere * 2f;
            offset.y = 0.3f;
            
            GameObject dropped = Instantiate(logPrefab, transform.position + offset, Quaternion.identity);

            if (dropped.TryGetComponent(out DroppedItem drop))
            {
                drop.itemName = "Wood";
                drop.itemType = "Material";
                drop.count = 1;
            }
        }
        Debug.Log($"{logCount}개의 통나무 드롭");
        Destroy(gameObject);
    }
}
