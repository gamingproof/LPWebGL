using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    public int Gold = 100;
    public Text text;

    private void Start()
    {
        ChangeText();
    }

    public void ChangeText()
    {
        text.text = "Gold: " + Gold.ToString();
    }
    
    public bool TryToBuy(int gold)
    {
        if (gold <= Gold)
        {
            Gold -= gold;
            ChangeText();
            return true;
        }
        Debug.Log("No monney"); // добавить выключение кнопок?
        return false;
    }

    public void AddGold(int gold)
    {
        Gold += gold;
        ChangeText();
    }
}
