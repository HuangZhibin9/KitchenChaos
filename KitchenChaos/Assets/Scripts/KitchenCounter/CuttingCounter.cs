using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    //切之后的生成物体的SO
    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;
    //切菜的配方
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    //重写柜台的交互方法
    public override void Interact(Player player)
    {
        //如果柜台上没有物品
        if (!HasKitchenObject())
        {
            //如果玩家手上有物品
            if (player.HasKitchenObject())
            {
                //如果有对应的菜谱
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //把物品放在柜台上
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
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
    //重写柜台的第二交互方法
    public override void InteractAltmate(Player player)
    {
        //如果柜台上有物品
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            KitchenObjectSO output = GetOutputKitchenObjectSO(GetKitchenObject().GetKitchenObjectSO());
            //销毁原来物体
            GetKitchenObject().DestroyKitchenObject();
            //生成切之后的物体
            KitchenObject.InstantiateKitchenObject(output, this);
        }
    }

    //返回菜谱对应的生成KitchenObjectSO
    private KitchenObjectSO GetOutputKitchenObjectSO(KitchenObjectSO input)
    {
        //遍历菜谱
        foreach (CuttingRecipeSO recipeSO in cuttingRecipeSOArray)
        {
            //找到对应输入的菜谱
            if (recipeSO.input == input)
            {
                //返回输出
                return recipeSO.output;
            }
        }
        //没找到返回null
        return null;
    }

    //是否有对应的菜谱
    private bool HasRecipeWithInput(KitchenObjectSO input)
    {
        //遍历菜谱
        foreach (CuttingRecipeSO recipeSO in cuttingRecipeSOArray)
        {
            //找到对应输入的菜谱
            if (recipeSO.input == input)
            {
                //返回true
                return true;
            }
        }
        //没找到返回false
        return false;
    }
}
