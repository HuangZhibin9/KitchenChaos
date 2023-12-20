using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgress
{
    //进度更改事件
    public event EventHandler<IHasProgressEventArgs> progressChanged;
    public class IHasProgressEventArgs : EventArgs
    {
        public float progressPercent;
    }
}
