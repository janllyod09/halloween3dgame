using UnityEngine;
using UnityEngine.UI;

public class KeysCounterInScreen : MonoBehaviour
{
    private Text itemCounterText;
    private int itemCount = 0;

    private void Start()
    {
        itemCounterText = GetComponent<Text>();
        UpdateItemCounterText();
    }

    public void IncrementItemCount()
    {
        itemCount++;
        UpdateItemCounterText();
    }

    public void DecrementItemCount()
    {
        itemCount--;
        UpdateItemCounterText();
    }

    private void UpdateItemCounterText()
    {
        itemCounterText.text = "Keys: " + itemCount.ToString();
    }
}