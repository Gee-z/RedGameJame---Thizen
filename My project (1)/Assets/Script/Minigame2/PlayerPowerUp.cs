using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    public IEnumerator PlayerBigger()
    {
        SavedGameData.instance.isBig = true;
        yield return new WaitForSeconds(2f);
        SavedGameData.instance.isBig = false;
    }
    public IEnumerator ActivateShield()
    {
        SavedGameData.instance.haveShield = true;
        yield return null;
    }
    public IEnumerator RestoreHP()
    {
        SavedGameData.instance.playerHp += 1;
        yield return null;
    }
    public IEnumerator ChangeMult()
    {
        SavedGameData.instance.currentMult += 1.5f;
        yield return new WaitForSeconds(5f);
        SavedGameData.instance.currentMult -= 1.5f;
    }
    public IEnumerator ExtraCoin()
    {
        SavedGameData.instance.AddCoin(50);
        yield return null;
    }
    
}
