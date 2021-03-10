using TMPro;

using UnityEngine;

public class CollectableText : MonoBehaviour
{
    public TextMeshProUGUI display;
    public string textFormat;
    
    public void Initialize(int totalCoinAmount)
    {
        SetCoinText(0, totalCoinAmount);
    }

    public void SetCoinText(int coinAmount, int totalCoinAmount)
    {
        display.text = string.Format(textFormat, coinAmount, totalCoinAmount);
    }
}
