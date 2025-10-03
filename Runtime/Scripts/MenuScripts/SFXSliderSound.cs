using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SliderDrag : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] AudioSource sfxSource;

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Sliding finished");
        SoundManager.Instance.PlaySound(sfxSource, "PlayerKill3");
    }
}