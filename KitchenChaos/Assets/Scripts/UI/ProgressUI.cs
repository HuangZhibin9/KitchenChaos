using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    //进度条
    [SerializeField] private Image barImage;
    //所属的柜台
    [SerializeField] private GameObject counterWithProgress;
    //该柜台的接口脚本
    private IHasProgress iHasProgress;

    private void Start()
    {
        //隐藏进度条
        HideProgress();
        barImage.fillAmount = 0;
    }
    private void Awake()
    {
        iHasProgress = counterWithProgress.GetComponent<IHasProgress>();
        if (iHasProgress == null)
        {
            Debug.LogError("This counter has no IHasProgree!");
        }
    }

    //订阅事件
    private void OnEnable()
    {
        iHasProgress.progressChanged += OnCuttingProgressChanged;
    }

    //当进度条改变时触发，改变进度条的值
    private void OnCuttingProgressChanged(object sender, IHasProgress.IHasProgressEventArgs e)
    {
        //如果进度条的值为0或者1，隐藏进度条
        if (e.progressPercent == 0 || e.progressPercent == 1)
        {
            HideProgress();
        }
        //否则显示进度条
        else
        {
            ShowProgress();
            barImage.fillAmount = e.progressPercent;
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
