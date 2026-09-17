using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI workCycleTimesText;
    [SerializeField] private TextMeshProUGUI statusTime;
    [SerializeField] private TextMeshProUGUI workStatusText;

    [SerializeField] private TextMeshProUGUI workTimeText;
    [SerializeField] private TextMeshProUGUI restTimeText;
    [Header("Panl")]
    [SerializeField] private GameObject startPanl;
    [SerializeField] private GameObject workPanl;
    [Header("OperationBtn")]
    [SerializeField] private Button ReStartBtn;
    [SerializeField] private Button StopBtn;
    [SerializeField] private Button SkipBtn;

    private bool isWorking = true;
    private string workTimes;
    private string[] workTime;
    private int minutes;
    private int seconds;
    private float turnoverTime = 1f;
    private float timer;
    private bool isTimerInitialized = false;

    private string workCycleTimes;
    private string[] workCycleTime;
    private int numbers;
    private int allNumbers;
    private bool isRest = false;

    private string initworkCycleTimes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = turnoverTime;
        initworkCycleTimes = workCycleTimesText.text;
        ReStartBtn.onClick.AddListener(delegate { ReStart(); });
        StopBtn.onClick.AddListener(delegate { Stop(); });
        SkipBtn.onClick.AddListener(delegate { Skip(); });
    }

    // Update is called once per frame
    void Update()
    {
        CountDownTimer();
    }
    #region 计时器
    void CountDownTimer()
    {
        if (gameObject.activeSelf && isWorking)
        {
            timer -=Time.deltaTime;
            if (timer < 0)
            {
                timer = turnoverTime;
                GetMituteSeconds();
                if (minutes >= 0)
                {
                    if (seconds <= 0 && minutes > 0)
                    {
                        minutes--;
                        seconds = 59;
                    }
                    else if (seconds > 0)
                    {
                        seconds--;
                    }
                    else if (minutes == 0 && seconds == 0)
                    {
                        NumberOfRounds();
                    }
                    statusTime.text = $"00:{minutes:d2}:{seconds:d2}";
                }
               
                
            }

        }
    }

    void NumberOfRounds()
    {
        workCycleTimes = workCycleTimesText.text;
        workCycleTime = workCycleTimes.Split("/");
        numbers = int.Parse(workCycleTime[0]);
        allNumbers= int.Parse(workCycleTime[1]);
        if(numbers< allNumbers)
        {         
            if (!isRest)
            {
                isRest = true;
                workStatusText.text = "休息中";
                minutes = int.Parse(restTimeText.text);
            }
            else
            {
                numbers++;
                workStatusText.text = "正在工作";
                minutes = int.Parse(workTimeText.text);
                workCycleTimesText.text = $"{numbers}/{allNumbers}";
                isRest = false;
            }
        }
        else
        {
            isTimerInitialized = false;
            Debug.Log("项目已结束");
            startPanl.SetActive(true);
            workPanl.SetActive(false);

        }

    }

    void GetMituteSeconds()
    {
        if (!isTimerInitialized)
        {
            workTimes = statusTime.text;
            workTime = workTimes.Split(":");
            minutes = int.Parse(workTime[1]);
            seconds = int.Parse(workTime[2]);
            isTimerInitialized = true;
        }
    }
    #endregion
    #region 暂停跳过重启
    void ReStart()
    {
        isTimerInitialized = false;
        isRest = false;
        workStatusText.text = "正在工作";
        startPanl.SetActive(true);
        workPanl.SetActive(false);
    }

    void Stop()
    {
        isWorking = !isWorking;
    }

    void Skip()
    {
        NumberOfRounds();
        seconds = 0;
        statusTime.text = $"00:{minutes:d2}:00";
    }

    #endregion
}
