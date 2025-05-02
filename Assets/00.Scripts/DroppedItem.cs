using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    public string itemName;
    public string itemType;
    public int count = 1;

    private bool isPlayerNear = false;

    [SerializeField] private Canvas uiCanvas;
    [SerializeField] private GameObject iconPrefab;
    private GameObject iconInstance;

    private void Start()
    {
        uiCanvas = GameObject.Find("Canvas")?.GetComponent<Canvas>();
        if (uiCanvas == null)
        {
            Debug.LogWarning("Canvas 찾을 수 없음!");
            return;
        }

        if (iconPrefab != null)
        {
            iconInstance = Instantiate(iconPrefab, uiCanvas.transform);
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
                Count = count,
            });

            GameDataManager.Instance.inventoryUI.ShowInventory();
            Destroy(gameObject);
        }

        if (iconInstance != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2.5f);
            iconInstance.transform.position = screenPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
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

    private void OnDestroy()
    {
        if (iconInstance != null)
            Destroy(iconInstance);
    }
}
