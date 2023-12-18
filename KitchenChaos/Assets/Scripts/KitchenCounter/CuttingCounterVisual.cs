using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingVisual : MonoBehaviour
{
    //字符串常量 animator中的trigger变量名字
    private const string CUT = "Cut";
    //获取柜台的Animator
    [SerializeField] private Animator animator;
    //获取ContainerCounter
    [SerializeField] private CuttingCounter cuttingCounter;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        //订阅cuttingCounter的cuttingAction事件
        cuttingCounter.cuttingAction += CuttingCounter_OnCutAction;
    }

    private void CuttingCounter_OnCutAction(object sender, EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}
