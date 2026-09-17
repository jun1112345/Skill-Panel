using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemList
{
    public List<Item> itemList;

    public event EventHandler<InventoryChangedEventArgs> itemListChange;

    public ItemList()
    {
        itemList = new List<Item>();
    }

    public void AddList(Item item)
    {
        itemList.Add(item);
        itemListChange?.Invoke(this, new InventoryChangedEventArgs(InventoryChangeType.Add));
        SaveSystem.SaveItems(itemList);
    }

    public void RemoveList(Item item) 
    {
        itemList.Remove(item);
        itemListChange?.Invoke(this, new InventoryChangedEventArgs(InventoryChangeType.Remove));
        SaveSystem.SaveItems(itemList);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
