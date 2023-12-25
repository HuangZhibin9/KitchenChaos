using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoSingleton<DeliveryManager>
{
    //所有的食谱
    [SerializeField] private RecipeListSO recipeListSO;
    //等待送餐的食谱
    private List<RecipeSO> waitingRecipeSOList;
    //等待送餐的食谱的最大数量
    [SerializeField] private int waitingRecipeMax = 4;
    //成功送餐的数量
    private int recipeFinishedCount = 0;

    //产生食谱的计时器
    private float spawnRecipeTimer;
    //计时器的最大值
    [SerializeField] private float spawnRecipeTimerMax = 4f;

    //菜谱产生事件和完成事件
    public event Action recipeSpawned;
    public event Action recipeListCompleted;
    //提交菜品成功或失败事件
    public event Action recipeListSucceeded;
    public event Action recipeListFailed;

    private void Awake()
    {
        spawnRecipeTimer = spawnRecipeTimerMax;
        waitingRecipeSOList = new List<RecipeSO>();
    }
    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            SpawnRecipe();
        }
    }

    private void SpawnRecipe()
    {
        //如果等待送餐的食谱数量已经达到最大值
        if (waitingRecipeSOList.Count >= waitingRecipeMax)
        {
            return;
        }
        //随机产生一个食谱
        RecipeSO recipeSO = recipeListSO.allRecipes[UnityEngine.Random.Range(0, recipeListSO.allRecipes.Count)];
        //将食谱加入等待送餐的食谱列表
        waitingRecipeSOList.Add(recipeSO);
        recipeSpawned?.Invoke();
    }

    //提交食物
    public void DeliverFood(PlateKitchenObject plateKitchenObject)
    {
        foreach (RecipeSO recipeSO in waitingRecipeSOList)
        {
            //如果食谱和盘子里的食材一样
            if (CompareRecipeAndPlate(recipeSO, plateKitchenObject))
            {
                //将食谱从等待送餐的食谱列表中移除
                waitingRecipeSOList.Remove(recipeSO);
                recipeFinishedCount++;
                recipeListCompleted?.Invoke();
                recipeListSucceeded?.Invoke();
                return;
            }
        }
        recipeListFailed?.Invoke();
        Debug.Log("送餐失败");
    }

    private bool CompareRecipeAndPlate(RecipeSO recipeSO, PlateKitchenObject plateKitchenObject)
    {
        //如果食谱和盘子里的食材数量不一样,返回false
        if (recipeSO.kitchenObjectSOList.Count != plateKitchenObject.GetKitchenObjectSOList().Count)
        {
            return false;
        }
        //如果食谱和盘子里的食材不一样,返回false
        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            if (!plateKitchenObject.GetKitchenObjectSOList().Contains(kitchenObjectSO))
            {
                return false;
            }
        }
        return true;
    }

    //获取等待送餐的食谱列表
    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }
    public int GetRecipeFinishedCount()
    {
        return recipeFinishedCount;
    }
}
