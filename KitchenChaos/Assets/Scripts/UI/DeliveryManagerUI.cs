using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;



    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        DeliveryManager.Instance.recipeSpawned += UpdateVisual;
        DeliveryManager.Instance.recipeListCompleted += UpdateVisual;
    }

    private void OnDisable()
    {
        Debug.Log(DeliveryManager.Instance);
        if (DeliveryManager.Instance != null)
        {
            DeliveryManager.Instance.recipeSpawned -= UpdateVisual;
            DeliveryManager.Instance.recipeListCompleted -= UpdateVisual;
        }
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
            recipeTransform.gameObject.SetActive(true);
        }
    }
}
