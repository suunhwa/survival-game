using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    private Dictionary<string, ItemData> itemDataDic = new();
    private Dictionary<string, ObjectData> objectDataDic = new();
    private Dictionary<string, RewardData> rewardDataDic = new();

    public List<Item> playerInventory = new();
    public InventoryUI inventoryUI;

    void Start()
    {
        LoadItemData();
        LoadObjectData();
        LoadRewardData();

        //InteractWithObject("Tree");

        //PrintInventory();

        if (inventoryUI != null)
        {
            inventoryUI.SetInventory(playerInventory);
            inventoryUI.ShowInventory(playerInventory);
        }
            
    }

    void LoadItemData()
    {
        var csv = Resources.Load<TextAsset>("Items");
        Debug.Log("�ҷ��� CSV ����:\n" + csv.text);

        using var reader = new StringReader(csv.text);
        var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
        {
            MemberTypes = CsvHelper.Configuration.MemberTypes.Fields, 
        };
        using var parser = new CsvReader(reader, config);

        foreach (var item in parser.GetRecords<ItemData>())
        {
            itemDataDic[item.Name] = item;
        }
    }

    void LoadObjectData()
    {
        var csv = Resources.Load<TextAsset>("Objects");
        using var reader = new StringReader(csv.text);

        var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
        {
            MemberTypes = CsvHelper.Configuration.MemberTypes.Fields
        };

        using var parser = new CsvReader(reader, config);

        foreach (var obj in parser.GetRecords<ObjectData>())
        {
            objectDataDic[obj.Name] = obj;
        }
    }

    void LoadRewardData()
    {
        var csv = Resources.Load<TextAsset>("Rewards");
        using var reader = new StringReader(csv.text);

        var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
        {
            MemberTypes = CsvHelper.Configuration.MemberTypes.Fields 
        };

        using var parser = new CsvReader(reader, config);
        foreach (var reward in parser.GetRecords<RewardData>())
        {
            reward.SplitRewards();
            rewardDataDic[reward.Name] = reward;
        }
    }

    public void InteractWithObject(string objectName)
    {
        if (!objectDataDic.ContainsKey(objectName))
        {
            Debug.LogWarning($"������Ʈ {objectName} ����");
            return;
        }

        if (!rewardDataDic.ContainsKey(objectName))
        {
            Debug.LogWarning($"���� ����: {objectName}");
            return;
        }

        var reward = rewardDataDic[objectName];
        foreach (var rewardName in reward.RewardListParsed) 
        {
            if (itemDataDic.TryGetValue(rewardName, out var itemData))
            {
                // ���� ������ ���� ��� ���� ����
                var existingItem = playerInventory.Find(i => i.Name == itemData.Name);
                if (existingItem != null)
                {
                    existingItem.Count++;
                }
                else
                {
                    playerInventory.Add(new Item
                    {
                        Name = itemData.Name,
                        ItemType = itemData.ItemType,
                        Count = 1
                    });
                }

            }
        }

        Debug.Log($"{objectName}�� �ı��ϰ� �������� ȹ����!");
    }

    void PrintInventory()
    {
        Debug.Log("�κ��丮:");
        foreach (var item in playerInventory)
        {
            Debug.Log($"- {item.Name} ({item.ItemType}) x{item.Count}");
        }
    }
}


