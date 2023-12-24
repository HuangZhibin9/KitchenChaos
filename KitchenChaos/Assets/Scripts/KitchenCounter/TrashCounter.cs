using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    //垃圾丢弃事件
    public static event Action<TrashCounter> anyObjectTrashed;
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroyKitchenObject();
            anyObjectTrashed?.Invoke(this);
        }
    }
}
