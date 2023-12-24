using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.StoveStateChanged += PlayStoveSound;
    }

    private void PlayStoveSound(StoveCounter.StoveState state)
    {
        if (state == StoveCounter.StoveState.Frying || state == StoveCounter.StoveState.Fried)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
