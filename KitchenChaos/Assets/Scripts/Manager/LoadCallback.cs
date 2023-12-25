using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCallback : MonoBehaviour
{
    private bool LoadFlag = true;
    private void Update()
    {
        if(LoadFlag)
        {
            Loader.LoadCallback();
            LoadFlag = false;
        }
    }
}
