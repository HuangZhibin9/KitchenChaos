using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    //发光物体
    [SerializeField] private GameObject bloomGameObject;
    //粒子效果
    [SerializeField] private GameObject particle;
    //StoveCounter
    [SerializeField] private StoveCounter stoveCounter;

    private void OnEnable()
    {
        stoveCounter.StoveStateChanged += OnStoveStateChanged;
    }

    private void OnStoveStateChanged(StoveCounter.StoveState state)
    {
        bool active = state == StoveCounter.StoveState.Frying || state == StoveCounter.StoveState.Fried;
        SetEffectActive(active);
    }

    private void SetEffectActive(bool active)
    {
        bloomGameObject.SetActive(active);
        particle.SetActive(active);
    }
}
