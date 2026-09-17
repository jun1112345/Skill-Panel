using UnityEngine;

public class InitUI : MonoBehaviour
{
    [SerializeField] private GameObject StartPanl;
    [SerializeField] private GameObject WorkPanl;
    private void Awake()
    {
        StartPanl.SetActive(true);
        WorkPanl.SetActive(false);
    }
}
