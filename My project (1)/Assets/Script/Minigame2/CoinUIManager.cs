using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinUIManager : MonoBehaviour
{
    public static CoinUIManager instance;
    public Animator coinAnimator;

    private void Awake()
    {

        instance = this;
    }

    public void PlayCollectAnimation()
    {
        if (coinAnimator != null)
        {
            coinAnimator.SetTrigger("Collect");
        }
    }
}
