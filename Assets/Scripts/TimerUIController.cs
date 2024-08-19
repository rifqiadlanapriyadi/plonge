using TMPro;
using UnityEngine;

public class TimerUIController : MonoBehaviour
{
    public GameManager gm;

    TextMeshProUGUI tmp;

    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        tmp.text = GetTimeString();
    }

    void Update()
    {
        tmp.text = GetTimeString();
    }

    string GetTimeString()
    {
        int timeSeconds = (int) gm.GetRemainingTime();
        int minutes = timeSeconds / 60;
        int seconds = timeSeconds % 60;
        return minutes + ":" + seconds;
    }
}
