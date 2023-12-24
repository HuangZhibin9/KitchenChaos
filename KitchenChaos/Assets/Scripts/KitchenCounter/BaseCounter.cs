using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    //CounterTopPoint位置
    [SerializeField] private Transform counterTopPoint;

    // 桌上的物体
    private KitchenObject kitchenObject = null;

    //放下物品的事件
    public static event Action<BaseCounter> anyObjectPlacedHere;

    public virtual void Interact(Player player)
    {
        Debug.LogError("Interact with BaseCounter!");
    }
    public virtual void InteractAltmate(Player player)
    {
        //Debug.LogError("InteractAltmate with BaseCounter!");
    }


    //获取柜台上的物体
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    //设置柜台上的物体
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            anyObjectPlacedHere?.Invoke(this);
        }
    }
    //是否已经有物体
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
    //清除柜台上的物体
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    //获取柜台的counterTopPoint
    public Transform GetThekitchenFollowTransform()
    {
        return counterTopPoint;
    }
}
