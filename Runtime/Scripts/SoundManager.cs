using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    //! Instance of SoundManager object
    public static SoundManager Instance;

    //! Game Audio Mixer
    public AudioMixer mixer;

    /*! Array of Enums used to seperate audio into different channels*/
    public enum AudioGroups : byte
    {
        Master, /*!< main game audio*/
        Music, /*!< music audio*/
        Sfx /*!< sound effect audio*/
    }
    //! Audio Settings
    public AudioSetting[] audioSettings;
    //! Array of audio clips played in the game
    [SerializeField] private AudioClip[] audioClips;
    //! List?
    List<AudioSource> sounds = new List<AudioSource>();

    private void Awake()
    {
        /*if (Instance != null)
		{
			Destroy(this);
		}*/
        Instance = this;

        // this is to make sure the slider actually matches what the player has at the moment
        foreach (AudioSetting setting in SoundManager.Instance.audioSettings)
        {
            setting.Initialize();
        }
    }

    private void Start()
    {
        foreach (AudioSetting setting in SoundManager.Instance.audioSettings)
        {
            setting.SetExposedParam(setting.slider.value);
        }
    }

    /*! Plays sound if is present in the AudioClips array*/
    public void PlaySound(AudioSource source, string clipName)
    {
        foreach (AudioClip clip in audioClips)
        {
            if (clip.name == clipName)
            {
                source.PlayOneShot(clip);
                return;
            }
        }
    }

    /*! Loops sound if it is present in AudioClips array
     
     Mostly used for music*/
    public void PlayLoop(AudioSource audiosource, string clipName)
    {
        foreach (AudioClip clip in audioClips)
        {
            if (clip.name == clipName)
            {
                audiosource.clip = clip;
                audiosource.loop = true;
                audiosource.Stop();
                audiosource.Play();
                return;
            }
        }
    }

    /*! Stops a currently playing loop*/
    public void StopLoop(AudioSource audiosource)
    {
        audiosource.Stop();
        audiosource.clip = null;
    }

    /*! Pauses all game audio*/
    public void PauseAllSounds()
    {
        sounds.AddRange(FindObjectsByType<AudioSource>(FindObjectsSortMode.None));  //AddRange adds all at once, so foreach not necessary
        foreach (AudioSource audio in sounds)
            if (audio.isPlaying)
                audio.Pause();
    }

    /*! Resumes all game audio*/
    public void ResumeAllSounds()
    {
        foreach (AudioSource audio in sounds)
        {
            if (audio.isPlaying) continue;
            audio.UnPause();
        }
    }

    //! Sets volume of Master mixer
    //! \param value: float for setting mixer volume
    public void SetMasterVolume(float volume)
    {
        Debug.Log(volume);
        audioSettings[(int)AudioGroups.Master].SetExposedParam(volume);
    }

    //! Sets volume of Sound Effect mixer
    //! \param value: float for setting mixer volume
    public void SetSfxVolume(float volume)
    {
        audioSettings[(int)AudioGroups.Sfx].SetExposedParam(volume);
    }

    //! Sets volume of Music mixer
    //! \param value: float for setting mixer volume
    public void SetMusicVolume(float volume)
    {
        audioSettings[(int)AudioGroups.Music].SetExposedParam(volume);
    }

    public void ResetVolume()
    {
        foreach (AudioSetting setting in SoundManager.Instance.audioSettings)
        {
            setting.SetExposedParam(1);
            setting.slider.value = 1;
        }
    }

}

[System.Serializable]


/*!The AudioSetting class */
public class AudioSetting
{
    //! Slider object for mixer volume
    public Slider slider;
    //! Enum list from AudioGroups class
    public SoundManager.AudioGroups group;

    public void Initialize()
    {
        // refactor this into a function later
        string param = group switch
        {
            SoundManager.AudioGroups.Master => "masterVol",
            SoundManager.AudioGroups.Music => "musicVol",
            SoundManager.AudioGroups.Sfx => "sfxVol",
            _ => ""
        };

        float savedValue = PlayerPrefs.GetFloat(param, 0.75f);
        slider.value = savedValue;
    }
    //! Changes the volume of an exposed audio mixer
    //! \param value: float for setting mixer volume
    public void SetExposedParam(float value)
    {
        // this one too (we still doing this?) - Evan
        string param = group switch
        {
            SoundManager.AudioGroups.Master => "masterVol",
            SoundManager.AudioGroups.Music => "musicVol",
            SoundManager.AudioGroups.Sfx => "sfxVol",
            _ => ""
        };

        float volume = Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20;
        SoundManager.Instance.mixer.SetFloat(param, volume);
        PlayerPrefs.SetFloat(param, value);
    }
}