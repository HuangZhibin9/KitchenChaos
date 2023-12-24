using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AudioClipSO : ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] deliverySucceeded;
    public AudioClip[] deliveryFailed;
    public AudioClip[] footStep;
    public AudioClip[] objectDrop;
    public AudioClip[] objectPickup;
    public AudioClip[] trash;
    public AudioClip[] warning;
    public AudioClip stoveSizzle;

}
