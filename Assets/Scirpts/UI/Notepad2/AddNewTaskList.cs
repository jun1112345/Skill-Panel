using UnityEngine;
using UnityEngine.EventSystems;

public class AddNewTaskList : MonoBehaviour, IPointerClickHandler
{
    public event System.Action onClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("µã»÷");
            onClick?.Invoke();
        }

    }
}
