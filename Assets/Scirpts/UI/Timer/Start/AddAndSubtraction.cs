using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AddAndSubtraction : MonoBehaviour
{
    [SerializeField] private Button addBtn;
    [SerializeField] private Button subtractionBtn;
    [SerializeField] private TextMeshProUGUI valueText;

    int value = 0;
    [SerializeField] private int maxValue = 10;
    [SerializeField] private int minValue = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        addBtn.onClick.AddListener(delegate { ValueAdd(); });
        subtractionBtn.onClick.AddListener(delegate { Valuesubtraction(); });

        // 添加长按处理（在按钮上挂载 LongPressHandler 组件）
        AddLongPressHandler(addBtn.gameObject, true);
        AddLongPressHandler(subtractionBtn.gameObject, false);
    
    }

    void AddLongPressHandler(GameObject btnObj, bool isAdd)
    {
        LongPressHandler handler = btnObj.GetComponent<LongPressHandler>();
        if (handler == null)
        {
            handler = btnObj.AddComponent<LongPressHandler>();
        }

        if (isAdd)
        {
            handler.OnClick = ValueAdd;
            handler.OnLongPress = ValueAdd;
        }
        else
        {
            handler.OnClick = Valuesubtraction;
            handler.OnLongPress = Valuesubtraction;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void ValueAdd()
    {

        value = int.Parse(valueText.text);
        if (value < maxValue)
        {
            value += 1;
            valueText.text = value.ToString();
        }
        
    }

    void Valuesubtraction()
    {
        value = int.Parse(valueText.text);
        if (value > minValue)
        {
            value -= 1;
            valueText.text = value.ToString();
        }
    }
}
