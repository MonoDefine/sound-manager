using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsPanel : MonoBehaviour
{
    [SerializeField] private Button exitButton;

    void OnEnable()
    {
        exitButton.onClick.AddListener(() => GoToMainMenu());
    }

    private void GoToMainMenu()
    {
        PausePanel.instance.ToggleTime();
        SceneManager.LoadScene((int)GameScene.MainMenu);
    }
}
