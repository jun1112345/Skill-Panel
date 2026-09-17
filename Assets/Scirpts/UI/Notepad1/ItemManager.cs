using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour
{
    private ItemList itemList;
    [SerializeField] private UI_ItemManager UI_itemList;
    [SerializeField] private AddNewItem addClick;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemList = new ItemList();
        itemList.itemList = SaveSystem.LoadItems();
        UI_itemList.SetItemList(itemList);
        addClick.onClick += AddClick_onClick;
    }

    private void AddClick_onClick()
    {
        itemList.AddList(new Item { name = "ÐÂÒ³Ãæ", detail = "" });
    }



}
