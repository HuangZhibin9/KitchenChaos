using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconUI : MonoBehaviour
{
    //所属的盘子
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    //Icon 模板
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        plateKitchenObject.IngredientAdded += OnIngredientAdded;
    }

    private void OnIngredientAdded(KitchenObjectSO sO)
    {
        UpdateVisual();
    }

    //更新视觉效果
    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate)
            {
                continue;
            }
            Destroy(child.gameObject);
        }
        foreach (var kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconSingleUI>().SetIconImage(kitchenObjectSO);
        }
    }
}
