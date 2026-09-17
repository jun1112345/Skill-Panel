using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChangeStatus : MonoBehaviour
{
    [SerializeField] private Image doneStatus;
    [SerializeField] private Image doneStatusCom;
    [SerializeField] private RectTransform taskItemPanl;
    [SerializeField] private float movePosition = 350f;
    [SerializeField] private float duration = 0.4f;
    [SerializeField] private Button changeStatus;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        changeStatus.onClick.AddListener(delegate { DoneStatusClick(); });
    }
    public void DoneStatusClick()
    {
        Debug.Log("11111");
        if (doneStatus != null)
        {
            doneStatusCom.enabled = true;
            taskItemPanl.DOKill();
            taskItemPanl.DOAnchorPosX(taskItemPanl.anchoredPosition.x + movePosition, duration);
        }
    }

}
