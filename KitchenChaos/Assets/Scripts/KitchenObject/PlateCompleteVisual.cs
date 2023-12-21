using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    //KitchenObjectSO - GameObject
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    //所属的盘子
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    //每种食材对应的游戏物体
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;

    private void OnEnable()
    {
        plateKitchenObject.IngredientAdded += OnIngredientAdded;
    }
    private void OnDisable()
    {
        plateKitchenObject.IngredientAdded -= OnIngredientAdded;
    }

    private void Start()
    {

        foreach (var kitchenObjectGameObject in kitchenObjectSOGameObjectList)
        {
            kitchenObjectGameObject.gameObject.SetActive(false);
        }
    }

    private void OnIngredientAdded(KitchenObjectSO kitchenObjectSO)
    {
        foreach (var kitchenObjectGameObject in kitchenObjectSOGameObjectList)
        {
            if (kitchenObjectGameObject.kitchenObjectSO == kitchenObjectSO)
            {
                kitchenObjectGameObject.gameObject.SetActive(true);
            }
        }
    }
}
