using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuttingCounterUI : MonoBehaviour
{
    //进度条
    [SerializeField] private Image barImage;
    //所属的柜台
    [SerializeField] private CuttingCounter cuttingCounter;

    private void Start()
    {
        //隐藏进度条
        HideProgress();
        barImage.fillAmount = 0;
    }

    //订阅事件
    private void OnEnable()
    {
        cuttingCounter.CuttingProgressChanged += OnCuttingProgressChanged;
    }

    //当进度条改变时触发，改变进度条的值
    private void OnCuttingProgressChanged(object sender, CuttingCounter.CuttingCounterEventArgs e)
    {
        //如果进度条的值为0或者1，隐藏进度条
        if (e.cuttingProgressPercent == 0 || e.cuttingProgressPercent == 1)
        {
            HideProgress();
        }
        //否则显示进度条
        else
        {
            ShowProgress();
            barImage.fillAmount = e.cuttingProgressPercent;
        }
    }
    //显示进度条
    private void ShowProgress()
    {
        gameObject.SetActive(true);
    }
    //隐藏进度条
    private void HideProgress()
    {
        gameObject.SetActive(false);
    }
}
