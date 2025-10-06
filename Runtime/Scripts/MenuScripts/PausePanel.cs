using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    public static PausePanel instance;
    public bool pauseActive;
	public AudioSource audiosource;

    [SerializeField] Button resumeButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button returnButton;

    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject confirmPanel;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject creditPanel; // credit display

    bool timePaused = false;

    public List<GameObject> list = new List<GameObject>();

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        audiosource = GetComponent<AudioSource>();
        instance = this;

        foreach (Transform child in this.transform)
        {
            list.Add(child.gameObject);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) // P key for now since ESC takes cursor out of Game Simulation
        {
            ResumeGame();
        }
    }

    void OnEnable()
    {
        resumeButton.onClick.AddListener(() => ResumeGame());
        settingsButton.onClick.AddListener(() => ShowSettingsPanel());
        returnButton.onClick.AddListener(() => ReturnToMain());
    }
    //! Toggles between stopping and starting time for pause functionality
    public void ToggleTime()
    {
        float timeScale = !timePaused ? 0.0f : 1.0f;
        timePaused = !timePaused;
        Time.timeScale = timeScale;
    }

    private void ResumeGame() // function for both the pause key and the menu button
    {
        pauseActive = !pauseActive;
        if (pauseActive)
        {
            SoundManager.Instance.PauseAllSounds();
            ToggleTime(); // for now having the timescale set to 0 works -> may need to change if we want animations running in Pause
            Cursor.lockState = CursorLockMode.Confined;
            SoundManager.Instance.PlayLoop(audiosource, "Anormality1");

            // this is to make sure the slider actually matches what the player has at the moment
            foreach (AudioSetting setting in SoundManager.Instance.audioSettings)
            {
                setting.Initialize();
            }
            pauseMenu.SetActive(pauseActive);
        }
        else
        {
            SoundManager.Instance.ResumeAllSounds();
            ToggleTime();
            Cursor.lockState = CursorLockMode.Locked;
            SoundManager.Instance.StopLoop(audiosource);

            foreach (GameObject panel in list)
            {
                panel.SetActive(false);
            }
        }
    }

    //! Displays Settings Panel
    private void ShowSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }

    //! Displays Credits Panel
    public void ShowCreditsPanel()
    {
        ResumeGame();
        creditPanel.SetActive(true);
    }

    //! Displays 'Are you sure?' panel
    private void ReturnToMain() // function to return to main menu
    {
        confirmPanel.SetActive(true);
    }

}
