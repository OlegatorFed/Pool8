using TMPro;
using UnityEngine;

public class TimerText : MonoBehaviour
{
    public TextMeshProUGUI display;
    public string textFormat;

    public void Initialize()
    {
        SetTimerText(0, 0);
    }

    public void SetTimerText(int minutes, int seconds)
    {
        display.text = string.Format(textFormat, minutes, seconds);
    }
}
