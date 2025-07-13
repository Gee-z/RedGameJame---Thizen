using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HpManager : MonoBehaviour
{
    public TMP_Text hpText;
    void Update()
    {
        hpText.text = SavedGameData.instance.playerHp.ToString();
    }
}
