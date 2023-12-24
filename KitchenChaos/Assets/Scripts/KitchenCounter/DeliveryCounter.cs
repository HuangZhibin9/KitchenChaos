using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        //如果玩家手上有物品
        if (player.HasKitchenObject())
        {
            //如果玩家手上的物品是盘子，把盘子销毁
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                //提交食物
                DeliveryManager.Instance.DeliverFood(plateKitchenObject);
                plateKitchenObject.DestroyKitchenObject();
            }
        }
    }
}
