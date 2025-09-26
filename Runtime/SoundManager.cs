using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance;
	public AudioMixer mixer;

	public enum AudioGroups : byte
	{
		Master,
		Music,
		Sfx
	}

	public AudioSetting[] audioSettings;

	[SerializeField] private AudioClip[] audioClips;
	List<AudioSource> sounds = new List<AudioSource>();

	void Awake()
	{
		if (Instance != null)
		{
			Destroy(this);
		}
		Instance = this;
	}

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

	public void StopLoop(AudioSource audiosource)
	{
		audiosource.Stop();
		audiosource.clip = null;
	}

	public void PauseAllSounds()
	{
		sounds.AddRange(FindObjectsByType<AudioSource>(FindObjectsSortMode.None));  //AddRange adds all at once, so foreach not necessary
		foreach (AudioSource audio in sounds)
				if (audio.isPlaying)
					audio.Pause();
	}

	public void ResumeAllSounds()
	{
		foreach (AudioSource audio in sounds)
		{
			if (audio.isPlaying) continue;
			audio.UnPause();
		}
	}

	public void SetMasterVolume(float volume)
	{
		audioSettings[(int)AudioGroups.Master].SetExposedParam(volume);
	}

	public void SetSfxVolume(float volume)
	{
		audioSettings[(int)AudioGroups.Sfx].SetExposedParam(volume);
	}

	public void SetMusicVolume(float volume)
	{
		audioSettings[(int)AudioGroups.Music].SetExposedParam(volume);
	}

}

[System.Serializable]
public class AudioSetting
{
	public Slider slider;
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

	public void SetExposedParam(float value)
	{
		// this one too
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