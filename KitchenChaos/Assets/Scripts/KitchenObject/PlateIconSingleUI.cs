using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour
{
    //Image组件
    [SerializeField] private Image image;

    //设置Icon图片
    public void SetIconImage(KitchenObjectSO kitchenObjectSO)
    {
        image.sprite = kitchenObjectSO.icon;
    }
}
