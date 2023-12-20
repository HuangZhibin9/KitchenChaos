using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    //碟子的SO
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    //碟子的数量
    private int plateCount;
    //碟子的最大数量
    private int plateCountMax = 5;

    //产生碟子的计时器
    private float plateSpawnTimer;
    //产生碟子的最大时间
    private float plateSpawnTimerMax = 5;

    public event Action plateSpawned;
    public event Action plateRemoved;

    private void Update()
    {
        plateSpawnTimer += Time.deltaTime;
        if (plateSpawnTimer > plateSpawnTimerMax)
        {
            plateSpawnTimer = 0f;
            if (plateCount < plateCountMax)
            {
                plateCount++;
                plateSpawned?.Invoke();
            }
        }
    }

    public override void Interact(Player player)
    {
        if (plateCount > 0 && !player.HasKitchenObject())
        {
            plateCount--;
            plateRemoved?.Invoke();
            KitchenObject.InstantiateKitchenObject(plateKitchenObjectSO, player);
        }
    }
}
