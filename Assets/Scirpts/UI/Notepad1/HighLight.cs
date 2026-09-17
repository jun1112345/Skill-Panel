using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighLight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [Header("hightLight")]
    [SerializeField] private Image borderImage;
    [SerializeField] private Image hightLightImage1;
    [SerializeField] private Image hightLightImage2;
    [SerializeField] private Color hightLightColor = Color.white;
    private Color curColor1;
    private Color curColor2;

    void Start()
    {
        curColor1 = hightLightImage1.color;
        curColor2 = hightLightImage2.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (borderImage != null)
        {
            borderImage.enabled = true;
            hightLightImage2.enabled = true;
            hightLightImage1.color = hightLightColor;
            hightLightImage2.color = hightLightColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (borderImage != null)
        {
            borderImage.enabled = false;
            hightLightImage2.enabled = false;
            hightLightImage1.color = curColor1;
            hightLightImage2.color = curColor2;
        }
    }
}
