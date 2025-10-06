using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button exitButton;
    [SerializeField] Button creditButton;

    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject creditsPanel;

    private bool creditsActive = false;

    private void Awake()
    {
        // this is to make sure the slider actually matches what the player has at the moment
        /*foreach (AudioSetting setting in SoundManager.Instance.audioSettings)
        {
            setting.Initialize();
        }*/
    }

    private void Start()
    {
        /*foreach (AudioSetting setting in SoundManager.Instance.audioSettings)
        {
            setting.SetExposedParam(setting.slider.value);
        }*/
    }

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
        startButton.onClick.AddListener(() => OpenGame());
        settingsButton.onClick.AddListener(() => ShowSettingsPanel());
        exitButton.onClick.AddListener(() => ExitGame());

        creditButton.onClick.AddListener(() => ToggleCreditsPanel());

    }

    //! Opens the main game scene
    private void OpenGame()
    {
        SceneManager.LoadScene((int)GameScene.LiveGame);
    }
    //! Enables settings panel
    private void ShowSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }
    //! Closes the game application
    private void ExitGame()
    {
        Application.Quit();
    }
    //! Toggles the credits panel on and off
    private void ToggleCreditsPanel()
    {
        creditsActive = !creditsActive;

        creditsPanel.SetActive(creditsActive);
    }
}
