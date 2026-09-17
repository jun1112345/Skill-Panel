using UnityEngine;

public enum TaskStatus
{
    unrecognised,
    ongoing,
    Completed
}

[System.Serializable]
public class Task
{
    public string taskName;
    public TaskStatus status;
    public string completeddate;

}
