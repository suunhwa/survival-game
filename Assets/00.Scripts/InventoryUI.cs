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
                        Debug.LogWarning($"{item.Name} 아이콘을 찾을 수 없습니다.");
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
                // 빈 슬롯 처리
                if (icon != null)
                {
                    icon.sprite = null;
                    icon.color = new Color(1, 1, 1, 0); 
                }
              
                if (countText != null)
                    countText.text = "";
            }
        }

        Debug.Log($"슬롯 14개 생성 완료, 아이템 수: {items.Count}");
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
        descriptionText.text = item.ItemType; // 설명
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
            Debug.LogWarning("선택된 아이템이 없습니다.");
            return;
        }

        selectedItem.Count--;

        Debug.Log($"{selectedItem.Name} 사용! 남은 수량: {selectedItem.Count}");

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
            Debug.LogWarning("선택된 아이템이 없습니다.");
            return;
        }

        int unitSize = selectedItem.Name == "Stone" ? 6 : 4;
        int dropCount = Mathf.Min(unitSize, selectedItem.Count);

        if (dropCount <= 0)
            return;

        int prefabCount = dropCount / unitSize;

        Vector3 offset = transform.right * Random.Range(-2f, 2f) + transform.forward * Random.Range(1f, 2f);
        Vector3 dropPos = dropPoint.position + transform.forward * 1.5f + offset;

        var dropped = ItemPoolManager.Instance.Spawn(
                selectedItem.Name,
                selectedItem.ItemType,
                unitSize,
                dropPos
            );

        Debug.Log($"[Drop] 프리팹 → ID: {dropped.GetInstanceID()} / 위치: {dropPos}");


        selectedItem.Count -= dropCount;

        Debug.Log($"{selectedItem.Name} {dropCount}개 버림! 남은 수량: {selectedItem.Count}");

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
