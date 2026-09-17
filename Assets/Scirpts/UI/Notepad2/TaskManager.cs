using UnityEngine;
using System.Collections.Generic;

public class TaskManager : MonoBehaviour
{
    public List<TaskList> taskLists;
    public event System.Action taskListChange;
    public event System.Action taskChange;

    private void Awake()
    {
        taskLists = SaveSystem.LoadTaskList();
        //monidata();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
                                //模拟输入数据
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddTaskList(TaskList taskList)
    {
        taskLists.Add(taskList);
        SaveSystem.SaveTaskList(taskLists);
        taskListChange?.Invoke();
    }

    public void RemoveTaskList(TaskList taskList)
    {
        taskLists.Remove(taskList);
        SaveSystem.SaveTaskList(taskLists);
        taskListChange?.Invoke();
    }

    public void AddTask(TaskList taskList, Task task)
    {
        taskList.AddTask(task);
        SaveSystem.SaveTaskList(taskLists);
        taskChange?.Invoke();
    }

    public void RemoveTask(TaskList taskList,Task task)
    {
        taskList.RemoveTask(task);
        SaveSystem.SaveTaskList(taskLists);
        taskChange?.Invoke();
    }

    public void SetTaskStatus(Task task, TaskStatus taskStatus)
    {
        task.status = taskStatus;
        SaveSystem.SaveTaskList(taskLists);
    }

    public void SaveTaskList()
    {
        SaveSystem.SaveTaskList(taskLists);
    }

    public bool DetermineCompleted(Task task)
    {
        if (task.status == TaskStatus.Completed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



    public List<TaskList> GetTaskLists()
    {
        return taskLists;
    }

    void monidata()
    {
        taskLists.Add(new TaskList
        {
            taskListName = "111",
            taskList = new List<Task>
            {
                new Task
                {
                    taskName="1",
                    status=TaskStatus.unrecognised,
                    completeddate="01/17"
                },
                new Task
                {
                    taskName="22",
                    status=TaskStatus.Completed,
                    completeddate="01/18"
                }
            }
        });

        taskLists.Add(new TaskList
        {
            taskListName = "222",
            taskList = new List<Task>
            {
                new Task
                {
                    taskName="3",
                    status=TaskStatus.ongoing,
                    completeddate="01/17"
                },
                new Task
                {
                    taskName="4",
                    status=TaskStatus.Completed,
                    completeddate="01/18"
                }
            }
        });
    }
}
