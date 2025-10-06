using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button exitButton;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
        retryButton.onClick.AddListener(() => RetryGame());
        exitButton.onClick.AddListener(() => ExitToMainMenu());
    }

    private void RetryGame()
    {
        SceneManager.LoadScene((int)GameScene.LiveGame);
        // SceneManager.LoadScene((int)AnormalityScene.GameOverTest);
    }

    private void ExitToMainMenu()
    {
        SceneManager.LoadScene((int)GameScene.MainMenu);
    }
}
