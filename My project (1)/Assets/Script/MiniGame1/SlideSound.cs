using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideSound : MonoBehaviour
{
    public static SlideSound instance;

    [Header("Audio Sources")]
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip clickSound;
    public AudioClip slideSound;
    public AudioClip coinSound;
    public AudioClip birdSound;
    public AudioClip hurtSound;

    private void Awake()
    {
        instance = this;
    }

    public void PlaySound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayClick()
    {
        PlaySound(clickSound);
    }

    public void PlaySlide()
    {
        PlaySound(slideSound);
    }
    
    public void PlayCoin()
    {
        PlaySound(coinSound);
    }

    public void PlayBird()
    {
        PlaySound(birdSound);
    }
    public void PlayHurt()
    {
        PlaySound(hurtSound);
    }
}
