using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SliderDragSound : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] AudioSource sfxSource;

    //! Checks for release of audio slider and plays a sound at the slider's current audio level
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Sliding finished");
        SoundManager.Instance.PlaySound(sfxSource, "PlayerKill3");
    }
}