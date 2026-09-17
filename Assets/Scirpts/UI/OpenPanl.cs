using UnityEngine;
using UnityEngine.EventSystems;

public class OpenPanl : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private GameObject openObject;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (openObject != null)
        {
            openObject.SetActive(!openObject.activeSelf);
        }
    }


}
