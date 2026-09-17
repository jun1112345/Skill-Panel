using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    // 默认路径：Application.persistentDataPath + "/items.json"
    private const string DefaultFileName = "items.json";
    private const string DefaultFileNameTask = "taskList.json";

    // 保存方法：可传入自定义路径，默认使用默认路径
    public static void SaveItems(List<Item> itemList, string filePath = null)
    {
        if (string.IsNullOrEmpty(filePath))
            filePath = Path.Combine(Application.persistentDataPath, DefaultFileName);

        ItemListData data = new ItemListData { items = itemList };
        string json = JsonUtility.ToJson(data, prettyPrint: true);
        File.WriteAllText(filePath, json);
        Debug.Log("保存成功：" + filePath);
    }

    // 加载方法：可传入自定义路径，默认使用默认路径
    public static List<Item> LoadItems(string filePath = null)
    {
        if (string.IsNullOrEmpty(filePath))
            filePath = Path.Combine(Application.persistentDataPath, DefaultFileName);

        if (!File.Exists(filePath)) return new List<Item>();

        string json = File.ReadAllText(filePath);
        ItemListData data = JsonUtility.FromJson<ItemListData>(json);
        return data?.items ?? new List<Item>();
    }

    public static void SaveTaskList(List<TaskList> taskList, string filePath = null)
    {
        if (string.IsNullOrEmpty(filePath))
            filePath = Path.Combine(Application.persistentDataPath, DefaultFileNameTask);

        TaskListData data = new TaskListData { taskLists = taskList };
        string json = JsonUtility.ToJson(data, prettyPrint: true);
        File.WriteAllText(filePath, json);
        Debug.Log("保存成功：" + filePath);
    }

    public static List<TaskList> LoadTaskList(string filePath = null)
    {
        if (string.IsNullOrEmpty(filePath))
            filePath = Path.Combine(Application.persistentDataPath, DefaultFileNameTask);

        if (!File.Exists(filePath)) return new List<TaskList>();

        string json = File.ReadAllText(filePath);
        TaskListData data = JsonUtility.FromJson<TaskListData>(json);
        return data?.taskLists ?? new List<TaskList>();
    }
}