using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent
{
    //获取柜台上的物体
    public KitchenObject GetKitchenObject();
    //设置柜台上的物体
    public void SetKitchenObject(KitchenObject kitchenObject);
    //是否已经有物体
    public bool HasKitchenObject();
    //清除柜台上的物体
    public void ClearKitchenObject();
    //获取柜台的counterTopPoint
    public Transform GetThekitchenFollowTransform();
}
