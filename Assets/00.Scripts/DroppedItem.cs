using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    public string itemName;
    public string itemType;
    public int count = 1;

    private bool isPlayerNear = false;
    private GameObject iconInstance;

    public void Setup(string name, string type, int count, GameObject iconPrefab)
    {
        this.itemName = name;
        this.itemType = type;
        this.count = count;

        if (iconInstance == null && iconPrefab != null)
        {
            iconInstance = Instantiate(iconPrefab, GameObject.Find("Canvas").transform);
            iconInstance.SetActive(false);
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            GameDataManager.Instance.AddItem(new Item
            {
                Name = itemName,
                ItemType = itemType,
                Count = this.count,
            });

            GameDataManager.Instance.inventoryUI.ShowInventory();

            if (iconInstance != null)
                Destroy(iconInstance);

            ItemPoolManager.Instance.Return(this);
        }

        if (iconInstance != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2.0f);
            iconInstance.transform.position = screenPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("DroppedItem Trigger Enter: " + other.name);

        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            iconInstance?.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            iconInstance?.SetActive(false);
        }
    }

    private void OnDisable()
    {
        iconInstance?.SetActive(false);
        isPlayerNear = false;
    }
}
