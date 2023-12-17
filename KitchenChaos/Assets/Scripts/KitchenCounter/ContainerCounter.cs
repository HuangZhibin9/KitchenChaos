using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    //获取所要产生的物体配置
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    //柜台产生的物体的事件
    public event EventHandler OnKitchenObjectInstantiate;

    //柜台的交互方法
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //生成物品
            KitchenObject.InstantiateKitchenObject(kitchenObjectSO, player);
            //触发OnKitchenObjectInstantiate事件
            OnKitchenObjectInstantiate?.Invoke(this, EventArgs.Empty);
        }
    }
}
