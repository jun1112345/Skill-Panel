using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemManager : MonoBehaviour
{
    [SerializeField] private Transform ItemContent;
    [SerializeField] private GameObject ItemPrefab;
    [SerializeField] private TMP_InputField InputDtail;

    private ItemList itemList;
    private Item currentSelectedItem;                    // 当前选中的物品

    private bool isUpdatingDetail = false;               // 防止循环更新的标志

    private TMP_InputField curInputDtail;

    void Start()
    {
        // 为详情输入框绑定值改变事件（只会绑定一次）
        InputDtail.onValueChanged.AddListener(OnDetailChanged);
    }


    public void SetItemList(ItemList itemList)
    { 
        this.itemList = itemList;
        itemList.itemListChange += ItemList_itemListChange;
        RefreshItem();
    }

    private void ItemList_itemListChange(object sender, InventoryChangedEventArgs e)
    {
        RefreshItem();
        if(e.ChangeType == InventoryChangeType.Add)
        {
            curInputDtail.Select();
        }else if(e.ChangeType == InventoryChangeType.Remove)
        {
            InputDtail.text = string.Empty;
        }
    }

    public void RefreshItem()
    {
        ClearItem();
        foreach (Item item in itemList.GetItemList())
        {
            var newUiItem = Instantiate(ItemPrefab, ItemContent).GetComponent<RectTransform>();
            newUiItem.name = item.name;
            TMP_InputField inputField = newUiItem.GetComponentInChildren<TMP_InputField>();
            curInputDtail = inputField;
            inputField.text = item.name;

            Item currentItem = item;
            inputField.onSelect.AddListener((string _) => 
            { 
                currentSelectedItem = currentItem;

                // 将详情文本框显示为该物品的 detail，但不要触发 onValueChanged
                isUpdatingDetail = true;
                InputDtail.text = currentItem.detail;
                isUpdatingDetail = false;
            });

            inputField.onValueChanged.AddListener((string newName) =>
            {
                currentItem.name = newName;
                // 可选：同步更新 UI 物体名称
                newUiItem.name = newName;
                SaveSystem.SaveItems(itemList.itemList);
            });

            Button deleteButton = newUiItem.GetComponentInChildren<Button>(); // 假设按钮在子物体中
            if (deleteButton != null)
            {
                deleteButton.onClick.AddListener(() =>
                {
                    // 从列表中删除
                    itemList.RemoveList(currentItem);
                    // 可选：清除当前选中的物品（如果删除了选中的那个）
                    if (currentSelectedItem == currentItem)
                        currentSelectedItem = null;
                });
            }
        }
    }

    private void OnDetailChanged(string newDetail)
    {
        // 如果是通过代码设置文本触发的（比如点击物品时），则忽略
        if (isUpdatingDetail) return;

        // 如果有选中的物品，则更新其 detail
        if (currentSelectedItem != null)
        {
            currentSelectedItem.detail = newDetail;
            SaveSystem.SaveItems(itemList.itemList);
        }
        else
        {
            Debug.LogWarning("没有选中任何物品，详情修改不会保存！");
        }
    }

    public void ClearItem()
    {
        foreach (Transform child in ItemContent)
        {
            Destroy(child.gameObject);
        }
    }
}
