using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    //有效的，能装进盘子的食材
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
    //装进食材时触发的事件
    public event Action<KitchenObjectSO> IngredientAdded;

    //盘子里的食材
    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        //如果食材不是有效的
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }
        //如果盘子里已经有这个物品
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }
        else
        {
            //盘子里的食材增加
            kitchenObjectSOList.Add(kitchenObjectSO);
            IngredientAdded?.Invoke(kitchenObjectSO);
            return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
