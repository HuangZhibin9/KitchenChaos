using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public enum StoveState
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }


    //Frying Recipe
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    //Burned Recipe
    [SerializeField] private BurningRecipeSO[] burnedRecipeSOArray;

    //当前对应的菜谱
    private FryingRecipeSO currentFryingRecipeSO;
    //炒菜的进度
    private float fryingTimer;
    //当前对应烧焦的菜谱
    private BurningRecipeSO currentBurningRecipeSO;
    //烧焦的进度
    private float burningTimer;
    //当前的状态
    private StoveState state;
    //状态改变的事件
    public event Action<StoveState> StoveStateChanged;
    public event EventHandler<IHasProgress.IHasProgressEventArgs> progressChanged;

    private void Update()
    {
        switch (state)
        {
            case StoveState.Idle:
                break;
            case StoveState.Frying:
                if (HasKitchenObject())
                {
                    fryingTimer += Time.deltaTime;
                    if (fryingTimer > currentFryingRecipeSO.fryingTimerMax)
                    {
                        //炒菜完成
                        KitchenObjectSO output = currentFryingRecipeSO.output;
                        GetKitchenObject().DestroyKitchenObject();
                        KitchenObject.InstantiateKitchenObject(output, this);
                        fryingTimer = 0;
                        burningTimer = 0;
                        currentBurningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        state = StoveState.Fried;
                        StoveStateChanged?.Invoke(state);
                    }
                    progressChanged?.Invoke(this,
                    new IHasProgress.IHasProgressEventArgs { progressPercent = fryingTimer / currentFryingRecipeSO.fryingTimerMax });
                }
                break;
            case StoveState.Fried:
                if (HasKitchenObject())
                {
                    burningTimer += Time.deltaTime;
                    if (burningTimer > currentBurningRecipeSO.BurningTimerMax)
                    {
                        //炒菜完成
                        KitchenObjectSO output = currentBurningRecipeSO.output;
                        GetKitchenObject().DestroyKitchenObject();
                        KitchenObject.InstantiateKitchenObject(output, this);
                        burningTimer = 0;
                        state = StoveState.Burned;
                        StoveStateChanged?.Invoke(state);
                    }
                    progressChanged?.Invoke(this,
                    new IHasProgress.IHasProgressEventArgs
                    {
                        progressPercent = burningTimer / currentBurningRecipeSO.BurningTimerMax
                    });
                }
                break;
            case StoveState.Burned:
                break;
        }

    }

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
                    //进度归零
                    fryingTimer = 0;
                    //获取菜谱
                    currentFryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    //转换为StoveState.Frying
                    state = StoveState.Frying;
                    StoveStateChanged?.Invoke(state);
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
                state = StoveState.Idle;
                StoveStateChanged?.Invoke(state);
                progressChanged?.Invoke(this,
                    new IHasProgress.IHasProgressEventArgs
                    {
                        progressPercent = 0f
                    });
            }
        }
    }

    //是否有对应的菜谱
    private bool HasRecipeWithInput(KitchenObjectSO input)
    {
        return GetFryingRecipeSOWithInput(input) != null;
    }

    //获取对应菜谱的输出KitchenObjectSO
    private KitchenObjectSO GetKitchenObjectSOWithInput(KitchenObjectSO input)
    {
        FryingRecipeSO recipeSO = GetFryingRecipeSOWithInput(input);
        return recipeSO != null ? recipeSO.output : null;
    }

    //获取对应的菜谱
    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO input)
    {
        //遍历菜谱
        foreach (FryingRecipeSO recipeSO in fryingRecipeSOArray)
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

    //获取对应烧焦的菜谱
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO input)
    {
        //遍历菜谱
        foreach (BurningRecipeSO recipeSO in burnedRecipeSOArray)
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
