using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartWorking : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Button startBtn;
    [SerializeField] private GameObject StartPanl;
    [Header("Start")]
    [SerializeField] private TextMeshProUGUI cycleIndexText;
    [SerializeField] private TextMeshProUGUI workTimeText;
    [SerializeField] private TextMeshProUGUI restTimeText;

    [Header("Work")]
    [SerializeField] private GameObject WorkPanl;
    [SerializeField] private TextMeshProUGUI workCycleTimesText;
    [SerializeField] private TextMeshProUGUI statusTime;

    void Start()
    {
        startBtn.onClick.AddListener(delegate { SetStartToWorking();});
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetStartToWorking()
    {
        StartPanl.SetActive(false);
        WorkPanl.SetActive(true);
        workCycleTimesText.text = $"1/{cycleIndexText.text}";
        statusTime.text = $"00:{int.Parse(workTimeText.text):d2}:00";
    }
}
