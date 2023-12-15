using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class KitchenObjectSO : ScriptableObject
{
    public Transform prefab;
    public Sprite icon;
    public string ObjectName;
}
