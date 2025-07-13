using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    public GameObject Shield;
    private bool lastFrameSizeState = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        SavedGameData.instance.onHpReduced.AddListener(TriggerDamageAnimation);
    }
    void Update()
    {
        if(SavedGameData.instance.isBig != lastFrameSizeState)
        {
            if (SavedGameData.instance.isBig)
            {
                animator.SetTrigger("ChangeSize");
            }
            else
            {
                Debug.Log("Resetting size animation");
                animator.SetTrigger("ResetSize");
            }
        }
        if (SavedGameData.instance.haveShield)
        {
            Shield.SetActive(true);
        }
        else
        {
            Shield.SetActive(false);
        } 
        lastFrameSizeState = SavedGameData.instance.isBig;
    }

    
    void TriggerDamageAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Damaged");
        }
    }
}
