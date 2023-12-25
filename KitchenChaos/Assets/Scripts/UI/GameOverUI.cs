using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeDelieverdNumber;

    private void Start()
    {
        GameManager.Instance.GameStateChanged += GameOverPanel;
        Hide();
    }


    private void GameOverPanel(GameManager.GameState state)
    {
        if (state == GameManager.GameState.GameOver)
        {
            recipeDelieverdNumber.text = DeliveryManager.Instance.GetRecipeFinishedCount().ToString();
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
