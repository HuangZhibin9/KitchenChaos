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
            //如果玩家手里有物品
            else
            {
                //如果玩家手里的物品是盘子
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //把柜台上的物品放在盘子里
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroyKitchenObject();
                    }
                }
                else
                {
                    //如果玩家手上的是食材，并且桌上的物品是盘子，尝试把食材放在盘子里
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroyKitchenObject();
                        }
                    }
                }
            }
        }
    }

}
