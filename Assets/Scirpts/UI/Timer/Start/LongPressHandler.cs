using UnityEngine;
using UnityEngine.EventSystems;

public class LongPressHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public System.Action OnLongPress;  // 长按回调
    public System.Action OnClick;      // 点击回调

    private float holdTime = 0f;
    [SerializeField] private float holdInterval = 0.15f;
    private bool isHolding = false;

    private bool isPressing = false;
    private float holdDelay = 0.7f;

    void Update()
    {
        if (isHolding && !isPressing)
        {
            holdTime += Time.deltaTime;
            if (holdTime >= holdDelay)
            {
                holdTime = 0f;
                isPressing = true;
            }
            
        }
        else if(isHolding && isPressing)
        {
            holdTime += Time.deltaTime;
            if (holdTime >= holdInterval)
            {
                holdTime = 0f;
                OnLongPress?.Invoke();  // 触发长按
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
        holdTime = 0f;
        //OnClick?.Invoke();  // 先执行一次点击
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
        isPressing = false;
        holdTime = 0f;
    }
}