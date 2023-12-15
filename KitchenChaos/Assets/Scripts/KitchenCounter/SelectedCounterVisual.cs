using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    //SelectedCounterVisual GameObject
    [SerializeField] private GameObject[] selectedCounterVisuals;
    //父对象的ClearCounter组件
    [SerializeField] private BaseCounter baseCounter;
    void Start()
    {
        Player.Instance.OnSelectedCounterChanged += OnSelected;
    }

    //当被选择的时候触发回调函数，使被选择的柜台变白
    public void OnSelected(object sender, Player.OnSelectedCounterChangedArgs e)
    {
        //如果当前被选择的柜台是自己则显示SelectedCounterVisual
        if (e._selectedCounter == this.baseCounter)
        {
            ShowSelectedCounterVisual();
        }
        else
        {
            HideSelectedCounterVisual();
        }
    }

    //显示SelectedCounterVisual
    public void ShowSelectedCounterVisual()
    {
        foreach (var counter in this.selectedCounterVisuals)
        {
            counter.SetActive(true);
        }
    }
    //隐藏SelectedCounterVisual
    public void HideSelectedCounterVisual()
    {
        foreach (var counter in this.selectedCounterVisuals)
        {
            counter.SetActive(false);
        }
    }

}
