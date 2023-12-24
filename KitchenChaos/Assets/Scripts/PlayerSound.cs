using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Player player;
    [SerializeField] private float footStepTimerMax = 0.15f;

    private float footStepTimer;

    private void Awake()
    {
        player = GetComponent<Player>();
        footStepTimer = footStepTimerMax;
    }

    private void Update()
    {
        footStepTimer -= Time.deltaTime;
        if (footStepTimer <= 0f)
        {
            footStepTimer = footStepTimerMax;
            if (player.IsWalking())
            {
                float volume = 1f;
                SoundManager.Instance.PlayStepSound(player.transform.position, volume);
            }

        }
    }
}
