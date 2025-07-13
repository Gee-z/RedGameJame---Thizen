using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    public TMP_Text coinText;

    void Update()
    {
        if (SavedGameData.instance != null)
        {
            coinText.text = SavedGameData.instance.playerCoin.ToString();
        }
    }
}
