using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class GameCountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText;

    private void Start()
    {
        GameManager.Instance.GameStateChanged += StartCountDown;
        Hide();
    }

    private void Update()
    {
        //countDownText.text = ((int)GameManager.Instance.GetCountDownTimer() + 1).ToString();
        countDownText.text = math.ceil(GameManager.Instance.GetCountDownTimer()).ToString();
    }

    private void StartCountDown(GameManager.GameState state)
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
