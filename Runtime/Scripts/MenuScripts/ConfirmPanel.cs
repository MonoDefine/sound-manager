using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfirmPanel : MonoBehaviour
{
    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // P key for now since ESC takes cursor out of Game Simulation
        {
            CloseConfirmPanel();
        }
    }

    void OnEnable()
    {
        yesButton.onClick.AddListener(() => GoToMainMenu());
        noButton.onClick.AddListener(() => CloseConfirmPanel());
    }

    private void GoToMainMenu()
    {
        PausePanel.instance.ToggleTime();
        SceneManager.LoadScene((int)AnormalityScene.MainMenu);
    }

    private void CloseConfirmPanel()
    {
        this.gameObject.SetActive(false);
    }
}
