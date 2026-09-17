using UnityEngine;
using UnityEngine.EventSystems;

public class AddNewItem : MonoBehaviour,IPointerClickHandler
{
    public event System.Action onClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            onClick?.Invoke();
        }
            
    }

}
