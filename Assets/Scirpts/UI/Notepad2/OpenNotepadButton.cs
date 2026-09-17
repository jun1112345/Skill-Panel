using UnityEngine;
using UnityEngine.EventSystems;

public class OpenNotepadButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject notepad2;
    [SerializeField] private UITaskManager uiTaskManager;
    private bool init = true;

    public void OnPointerClick(PointerEventData eventData)
    {
        ToggleNotepad();
    }

    public void ToggleNotepad()
    {
        bool isActive = !notepad2.activeSelf;
        notepad2.SetActive(isActive);
        if (isActive && init)
        {
            init = false;
            // ✅ 打开时刷新 UI 并选中输入框
            uiTaskManager.RefreshTaskList();
            uiTaskManager.SelectFirstInputField(); // 新增的公开方法
        }
    }
}
