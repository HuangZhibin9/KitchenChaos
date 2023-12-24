using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType<T>();
                //instance = FindObjectOfType<T>();
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this.gameObject.GetComponent<T>())
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        instance = this.gameObject.GetComponent<T>();
        this.OnStart();
    }

    protected virtual void OnStart()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
