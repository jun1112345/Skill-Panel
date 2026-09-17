using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UITaskManager : MonoBehaviour
{
    [Header("TaskList")]
    [SerializeField] private TaskManager taskManager;
    [SerializeField] private Transform taskListContent;
    [SerializeField] private GameObject taskListPrefab;
    [Header("Task")]
    [SerializeField] private Transform taskContent;
    [SerializeField] private GameObject taskPrefab;
    [Header("TaskCompleted")]
    [SerializeField] private Transform taskCompletedContent;
    [SerializeField] private GameObject taskCompletedPrefab;
    [Header("Other")]
    [SerializeField] private TMP_Text selectTaskListName;
    [SerializeField] private AddNewTaskList AddTaskListBtn;
    [SerializeField] private AddNewTask AddTasktBtn;
    public List<TaskList> taskLists;

    private TMP_InputField curInputDtail;
    private TaskList currentSelectedTaskList;
    private Task currentSelectedTask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        taskLists = new List<TaskList>();
        //taskLists = taskManager.GetTaskLists();
        taskManager.taskListChange += TaskManager_taskListChange;
        taskManager.taskChange += TaskManager_taskChange;
        
        RefreshTaskList();
        curInputDtail.Select();
        
        AddTaskListBtn.onClick += AddTaskListBtn_onClick;
        AddTasktBtn.onClick += AddTasktBtn_onClick;

    }

    public void SelectFirstInputField()
    {
        StartCoroutine(DelayedSelect());
    }

    private System.Collections.IEnumerator DelayedSelect()
    {
        yield return null;
        if (curInputDtail != null && curInputDtail.isActiveAndEnabled)
        {
            curInputDtail.Select();
            curInputDtail.ActivateInputField();
        }
    }


    private void AddTasktBtn_onClick()
    {
        if (currentSelectedTaskList != null)
        {
            taskManager.AddTask(currentSelectedTaskList, new Task());
        }
    }

    private void AddTaskListBtn_onClick()
    {
        taskManager.AddTaskList(new TaskList
        {
            taskListName = "待办事项列表"
        });
    }

    private void TaskManager_taskChange()
    {
        RefreshTask(currentSelectedTaskList);
    }

    private void TaskManager_taskListChange()
    {
        RefreshTaskList();
        curInputDtail.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshTaskList()
    {
        
        ClearTaskList();
        //var currentTaskLists = taskManager.GetTaskLists();
        foreach (TaskList taskList in taskManager.GetTaskLists()) 
        {
            var newUiItem = Instantiate(taskListPrefab, taskListContent).GetComponent<RectTransform>();
            newUiItem.name = taskList.taskListName;
            TMP_InputField inputField = newUiItem.GetComponentInChildren<TMP_InputField>();
            curInputDtail = inputField;
            inputField.text = taskList.taskListName;

            TaskList curtaskList = taskList;
            inputField.onValueChanged.AddListener((string newName) =>
            {
                curtaskList.taskListName = newName;
                // 可选：同步更新 UI 物体名称
                newUiItem.name = newName;
                selectTaskListName.text = newName;
                taskManager.SaveTaskList();
            });
            inputField.onSelect.AddListener((string _) =>
            {
                currentSelectedTaskList = curtaskList;
                selectTaskListName.text = curtaskList.taskListName;
                RefreshTask(currentSelectedTaskList);
            });

            Button deleteButton = newUiItem.GetComponentInChildren<Button>(); // 假设按钮在子物体中
            if (deleteButton != null)
            {
                deleteButton.onClick.AddListener(() =>
                {
                    // 从列表中删除
                    taskManager.RemoveTaskList(curtaskList);
                    // 可选：清除当前选中的物品（如果删除了选中的那个）
                    if (currentSelectedTaskList == curtaskList)
                        currentSelectedTaskList = null;
                    RefreshTask(currentSelectedTaskList);
                });
            }
        }
    }

    public void RefreshTask(TaskList taskList)
    {
        ClearTask();
        ClearTaskCompleted();
        if (taskList != null)
        {
            foreach (Task tasks in taskList.taskList)
            {
                if (!taskManager.DetermineCompleted(tasks))
                {
                    var newUiItem = Instantiate(taskPrefab, taskContent).GetComponent<RectTransform>();
                    newUiItem.name = tasks.taskName;
                    TMP_InputField inputField = newUiItem.GetComponentInChildren<TMP_InputField>();
                    inputField.text = tasks.taskName;
                    Task curTask = tasks;
                    inputField.onValueChanged.AddListener((string newName) =>
                    {
                        curTask.taskName = newName;
                        // 可选：同步更新 UI 物体名称
                        newUiItem.name = newName;
                        taskManager.SaveTaskList();
                    });

                    Button[] buttons = newUiItem.GetComponentsInChildren<Button>();
                    foreach (Button btn in buttons)
                    {
                        if (btn.name == "DeleteButton")
                        {
                            btn.onClick.AddListener(() =>
                            {
                                taskManager.RemoveTask(currentSelectedTaskList, curTask);
                                if (currentSelectedTaskList == taskList)
                                    currentSelectedTaskList = null;
                                RefreshTask(currentSelectedTaskList);
                            });
                        }
                        else if (btn.name == "ChangeStatus")
                        {
                            btn.onClick.AddListener(() =>
                            {
                                taskManager.SetTaskStatus(curTask,TaskStatus.Completed);
                                DOVirtual.DelayedCall(0.4f, () =>
                                {
                                    RefreshTask(currentSelectedTaskList);
                                });
                            });
                        }
                    }
                }
                else
                {
                    
                    var newUiItem = Instantiate(taskCompletedPrefab, taskCompletedContent).GetComponent<RectTransform>();
                    newUiItem.name = tasks.taskName;
                    TextMeshProUGUI inputField = newUiItem.GetComponentInChildren<TextMeshProUGUI>();
                    inputField.text = tasks.taskName;
                }
                

            }
        }
        
    }

    public void ClearTaskList()
    {
        foreach (Transform child in taskListContent)
        {
            Destroy(child.gameObject);
        }
    }

    public void ClearTask()
    {
        foreach (Transform child in taskContent)
        {
            Destroy(child.gameObject);
        }
    }

    public void ClearTaskCompleted()
    {
        foreach (Transform child in taskCompletedContent)
        {
            Destroy(child.gameObject);
        }
    }


}
