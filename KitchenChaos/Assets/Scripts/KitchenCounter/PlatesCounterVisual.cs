using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    //碟子的Prefab
    [SerializeField] private Transform plateVisualPrefab;
    //碟子生成的位置
    [SerializeField] private Transform counterTopPoint;
    //PlatesCounter
    [SerializeField] private PlatesCounter platesCounter;
    //生成的每个碟子的高度差
    [SerializeField] private float plateVisualYOffset = 0.1f;

    private List<GameObject> plateVisualList;

    private void Awake()
    {
        plateVisualList = new List<GameObject>();
    }

    private void OnEnable()
    {
        platesCounter.plateSpawned += OnPlateSpawned;
        platesCounter.plateRemoved += OnPlateRemoved;
    }

    private void OnPlateRemoved()
    {
        GameObject lastPlate = plateVisualList[plateVisualList.Count - 1];
        plateVisualList.Remove(lastPlate);
        Destroy(lastPlate);
    }

    private void Disable()
    {
        platesCounter.plateRemoved += OnPlateRemoved;
    }

    private void OnPlateSpawned()
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
        plateVisualTransform.localPosition = new Vector3(0, plateVisualYOffset * plateVisualList.Count, 0);
        plateVisualList.Add(plateVisualTransform.gameObject);
    }
}
