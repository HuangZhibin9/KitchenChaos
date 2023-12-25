using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayerClockUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;
    private void Start()
    {
        GameManager.Instance.GamePlayingTimerChange += Instance_GamePlayingTimerChange;
        //GameManager.Instance.GameStateChanged += GamePlayingClockStart;
        //Hide();
    }

    private void Instance_GamePlayingTimerChange(float timePercentage)
    {
        timerImage.fillAmount = timePercentage;
        if (timePercentage < 0.2f)
        {
            timerImage.color = Color.red;
        }
    }


    private void GamePlayingClockStart(GameManager.GameState state)
    {
        if (state == GameManager.GameState.CountDown)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
