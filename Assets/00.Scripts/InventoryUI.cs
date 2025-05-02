using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;      
    public Transform slotParent;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI countText;
    public Button useBtn;
    public Button discardBtn;
    public GameObject inventoryScreenUI;
    public GameObject logPrefab;
    public Transform dropPoint;
    
    private Item selectedItem;

    private void Start()
    {
        inventoryScreenUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        bool isActive = inventoryScreenUI.activeSelf;
        inventoryScreenUI.SetActive(!isActive);

        if (!isActive)
            ShowInventory();
    }

    public void CloseBtn()
    {
        inventoryScreenUI.SetActive(false);
    }

    public void ShowInventory()
    {
        ClearItemDetail();

        List<Item> items = GameDataManager.Instance.playerInventory;
        int totalSlotCount = 14;

        foreach (Transform child in slotParent)
            Destroy(child.gameObject);

        for (int i = 0; i < totalSlotCount; i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);

            var icon = slot.transform.Find("Icon")?.GetComponent<Image>();
            var countText = slot.transform.Find("Count")?.GetComponent<Text>();

            if (i < items.Count)
            {
                var item = items[i];
                if (icon != null)
                {
                    Sprite iconSprite = Resources.Load<Sprite>($"Icons/{item.Name.ToLower()}");
                    if (iconSprite != null)
                    {
                        icon.sprite = iconSprite;
                        icon.color = Color.white;

                        icon.preserveAspect = true;
                        icon.rectTransform.sizeDelta = new Vector2(60, 60);
                    }
                    else
                    {
                        Debug.LogWarning($"{item.Name} �������� ã�� �� �����ϴ�.");
                        icon.sprite = null;
                        icon.color = new Color(1, 1, 1, 0); 
                    }
                }

                if (countText != null)
                {
                    countText.text = item.Count > 1 ? $"x {item.Count}" : "";
                }

                var slotScript = slot.GetComponent<InventorySlot>();
                if (slotScript != null)
                {
                    slotScript.item = item;
                    slotScript.inventoryUI = this;
                }
            }
            else
            {
                // �� ���� ó��
                if (icon != null)
                {
                    icon.sprite = null;
                    icon.color = new Color(1, 1, 1, 0); 
                }
              
                if (countText != null)
                    countText.text = "";
            }
        }

        Debug.Log($"���� 14�� ���� �Ϸ�, ������ ��: {items.Count}");
    }

    public void ClearItemDetail()
    {
        nameText.text = "";
        descriptionText.text = "";
        countText.text = "";

        useBtn.gameObject.SetActive(false);
        discardBtn.gameObject.SetActive(false);
    }

    public void ShowItemDetail(Item item)
    {
        selectedItem = item;

        nameText.text = item.Name;
        descriptionText.text = item.ItemType; // ����
        countText.text = $"x{item.Count}";

        useBtn.onClick.RemoveAllListeners();
        discardBtn.onClick.RemoveAllListeners();

        useBtn.onClick.AddListener(UseItem);
        discardBtn.onClick.AddListener(DiscardItem);

        useBtn.gameObject.SetActive(true);
        discardBtn.gameObject.SetActive(true);
    }

    public void UseItem()
    {
        if (selectedItem == null)
        {
            Debug.LogWarning("���õ� �������� �����ϴ�.");
            return;
        }

        selectedItem.Count--;

        Debug.Log($"{selectedItem.Name} ���! ���� ����: {selectedItem.Count}");

        if (selectedItem.Count <= 0)
        {
            GameDataManager.Instance.playerInventory.Remove(selectedItem);
            ClearItemDetail();
        }
        else
        {
            ShowItemDetail(selectedItem);
        }
            GameDataManager.Instance.inventoryUI.ShowInventory();
    }

    public void DiscardItem()
    {
        if (selectedItem == null)
        {
            Debug.LogWarning("���õ� �������� �����ϴ�.");
            return;
        }

        GameObject prefabToDrop = logPrefab;

        Vector3 randomOffset = new Vector3(Random.Range(-2.5f, 2.5f), 0f, Random.Range(-2.5f, 2.5f));
        Vector3 dropPos = dropPoint.position + transform.forward * 1.5f + randomOffset;
        GameObject dropped = Instantiate(prefabToDrop, dropPos, Quaternion.identity);

        if (dropped.TryGetComponent(out DroppedItem drop))
        {
            drop.itemName = selectedItem.Name;
            drop.itemType = selectedItem.ItemType;
            drop.count = 1;
        }

        selectedItem.Count--;

        Debug.Log($"{selectedItem.Name} ����! ���� ����: {selectedItem.Count}");

        if (selectedItem.Count <= 0)
        {
            GameDataManager.Instance.playerInventory.Remove(selectedItem);
            ClearItemDetail();
        }
        else
        {
            ShowItemDetail(selectedItem);
        }

        GameDataManager.Instance.inventoryUI.ShowInventory();
    }

}
