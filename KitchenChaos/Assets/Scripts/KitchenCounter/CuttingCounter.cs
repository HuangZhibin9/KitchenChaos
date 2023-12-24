using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    //切之后的生成物体的SO
    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;
    //切菜的配方
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    //切菜的进度
    private int cuttingProgress;
    //当前对应的菜谱
    private CuttingRecipeSO recipeSO;

    //进度更改事件
    public event EventHandler<IHasProgress.IHasProgressEventArgs> progressChanged;

    //切菜动作事件
    public event EventHandler cuttingAction;
    //切菜静态事件
    public static event Action<CuttingCounter> anyCuttingAction;

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
                    //切菜进度归零
                    cuttingProgress = 0;
                    //获取菜谱
                    recipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
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
            }
        }
    }
    //重写柜台的第二交互方法
    public override void InteractAltmate(Player player)
    {
        //如果柜台上有物品
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            //切菜进度+1
            cuttingProgress++;
            anyCuttingAction?.Invoke(this);
            cuttingAction?.Invoke(this, EventArgs.Empty);
            //触发进度更改事件
            progressChanged?.Invoke(this,
            new IHasProgress.IHasProgressEventArgs
            {
                progressPercent = (float)cuttingProgress / recipeSO.cuttingProgressMax
            });
            //如果切菜进度达到最大值
            if (cuttingProgress >= recipeSO.cuttingProgressMax)
            {
                //销毁原来物体
                GetKitchenObject().DestroyKitchenObject();
                //生成切之后的物体
                KitchenObject.InstantiateKitchenObject(recipeSO.output, this);
                recipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            }
        }
    }

    //是否有对应的菜谱
    private bool HasRecipeWithInput(KitchenObjectSO input)
    {
        return GetCuttingRecipeSOWithInput(input) != null;
    }

    //获取对应菜谱的输出KitchenObjectSO
    private KitchenObjectSO GetKitchenObjectSOWithInput(KitchenObjectSO input)
    {
        CuttingRecipeSO recipeSO = GetCuttingRecipeSOWithInput(input);
        return recipeSO != null ? recipeSO.output : null;
    }

    //获取对应的菜谱
    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO input)
    {
        //遍历菜谱
        foreach (CuttingRecipeSO recipeSO in cuttingRecipeSOArray)
        {
            //找到对应输入的菜谱
            if (recipeSO.input == input)
            {
                //返回输出
                return recipeSO;
            }
        }
        //没找到返回null
        return null;
    }
}
