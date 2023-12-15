using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainCounterVisual : MonoBehaviour
{
    //字符串常量 animator中的trigger变量名字
    private const string OPEN_CLOSE = "OpenClose";
    //获取柜台的Animator
    [SerializeField] private Animator animator;
    //获取ContainerCounter
    [SerializeField] private ContainerCounter containerCounter;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        //订阅ContainerCounter的OnKitchenObjectInstantiate事件
        containerCounter.OnKitchenObjectInstantiate += ContainerCounter_OnKitchenObjectInstantiate;
    }

    private void ContainerCounter_OnKitchenObjectInstantiate(object sender, EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
