using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{



    //所属的柜台
    private IKitchenObjectParent kitchenParent;
    //物体对应的KitchenObjectSO
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    //返回物体的KitchenObjectSO
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }


    //设置物体所属的柜台
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        //如果该物体本身已经有柜台了，就将该物体从柜台上移除
        if (this.kitchenParent != null)
        {
            this.kitchenParent.ClearKitchenObject();
        }
        //将该物体设置到柜台上
        this.kitchenParent = kitchenObjectParent;
        //如果柜台上已经有物体了，就报错
        if (kitchenObjectParent.HasKitchenObject() == true)
        {
            Debug.LogError("There is already a KitchenObject on this ClearCounter!");
        }
        //如果柜台上没有物体，就将该物体设置到柜台上
        else
        {
            kitchenObjectParent.SetKitchenObject(this);
        }
        //设置物体的位置
        this.transform.parent = kitchenObjectParent.GetThekitchenFollowTransform();
        this.transform.localPosition = Vector3.zero;
    }
    //获取物体所属的柜台
    public IKitchenObjectParent GetClearCounter()
    {
        return kitchenParent;
    }

    //销毁物体
    public void DestroyKitchenObject()
    {
        //清空父物体的kitchenObject
        kitchenParent.ClearKitchenObject();
        //销毁物体
        Destroy(this.gameObject);
    }

    //静态方法 生成物体
    public static KitchenObject InstantiateKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        //生成物体
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        //设置该物体所属的柜台
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        //返回物体
        return kitchenObject;
    }

}
