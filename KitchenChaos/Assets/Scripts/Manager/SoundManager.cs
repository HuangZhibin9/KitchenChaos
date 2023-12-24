using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    //音效SO
    [SerializeField] private AudioClipSO audioClipSO;


    protected override void OnStart()
    {
        DeliveryManager.Instance.recipeListSucceeded += PlaySucessSound;
        DeliveryManager.Instance.recipeListFailed += PlayFailedSound;

        CuttingCounter.anyCuttingAction += PlayCuttingSound;
        Player.Instance.pickUpSomething += PlayPickUpSound;
        BaseCounter.anyObjectPlacedHere += PlayDropSound;
        TrashCounter.anyObjectTrashed += PlayTrashSound;
    }

    private void PlayTrashSound(TrashCounter counter)
    {
        PlaySound(audioClipSO.trash, counter.transform.position);
    }

    private void PlayDropSound(BaseCounter counter)
    {
        PlaySound(audioClipSO.objectDrop, counter.transform.position);
    }

    private void PlayPickUpSound()
    {
        PlaySound(audioClipSO.objectPickup, Player.Instance.transform.position);
    }

    private void PlayCuttingSound(CuttingCounter cuttingCounter)
    {
        //播放切菜的声音
        PlaySound(audioClipSO.chop, cuttingCounter.transform.position);
    }

    private void PlayFailedSound()
    {
        //播放失败的声音
        PlaySound(audioClipSO.deliveryFailed, DeliveryCounter.Instance.transform.position);
    }

    private void PlaySucessSound()
    {
        PlaySound(audioClipSO.deliverySucceeded, DeliveryCounter.Instance.transform.position);
    }


    //播放声音
    public void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volume);
    }
    //播放声音
    public void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void PlayStepSound(Vector3 position, float volume)
    {
        PlaySound(audioClipSO.footStep, position, volume);
    }
}
