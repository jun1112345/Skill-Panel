using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Highlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("hightLight")]
    [SerializeField] private Image borderImage;
    [SerializeField] private Image DarkImage;
    [SerializeField] private Image hightLightImage;
    private Color curColor;
    [SerializeField] private Color hightLightColor = Color.white;
    [Header("click")]
    private Color darkCurColor;
    [SerializeField] private Color darkColor = Color.grey;
    private RectTransform parentRect;
    [SerializeField] private float scaling = 0.9f;
    [SerializeField] private float scaleTime = 0.1f;
    private Vector3 originalScale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        curColor = hightLightImage.color;
        darkCurColor = DarkImage.color;
        parentRect = transform.parent.GetComponent<RectTransform>();
        originalScale = parentRect.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(borderImage != null)
        {
            borderImage.enabled = true;
            hightLightImage.color = hightLightColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (borderImage != null)
        {
            borderImage.enabled = false;
            hightLightImage.color = curColor;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        DarkImage.color = darkColor;
        //ScaleOverTime(originalScale, originalScale * scaling, scaleTime);
        StopAllCoroutines();
        StartCoroutine(ScaleOverTime(originalScale, originalScale * scaling, scaleTime));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        DarkImage.color = darkCurColor;
        //ScaleOverTime(parentRect.localScale, originalScale, scaleTime);
        StopAllCoroutines();
        StartCoroutine(ScaleOverTime(transform.localScale, originalScale, scaleTime));
    }

    private System.Collections.IEnumerator ScaleOverTime(Vector3 start, Vector3 end, float time)
    {
        float elapsed = 0f;
        while (elapsed < time)
        {
            parentRect.localScale = Vector3.Lerp(start, end, elapsed / time);
            elapsed += Time.deltaTime;
            yield return null;
        }
        parentRect.localScale = end;
    }
}
