using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    //获取所要产生的物体配置
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    //柜台的交互方法
    public override void Interact(Player player)
    {
        //如果柜台上没有物品
        if (!HasKitchenObject())
        {
            //如果玩家手上有物品
            if (player.HasKitchenObject())
            {
                //把物品放在柜台上
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        //如果柜台上有物品
        else
        {
            //如果玩家手里没有物品
            if (!player.HasKitchenObject())
            {
                //把柜台上的物品放在手上
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}
