using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TaskList
{
    public string taskListName;
    public List<Task> taskList;

    public TaskList()
    {
        taskList = new List<Task>();
    }

    public void AddTask(Task task)
    {
        taskList.Add(task);
    }

    public void RemoveTask(Task task)
    {
        taskList.Remove(task);
    }

    public void SetTaskStatus(Task task, TaskStatus taskStatus)
    {
        task.status = taskStatus;
    }

    public List<Task> GetItemList()
    {
        return taskList;
    }
}
