using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour

{
    [SerializeField] Button noButton;

    void OnEnable()
    {
        noButton.onClick.AddListener(() => CloseWindow());
    }

    void CloseWindow()
    {
        this.gameObject.SetActive(false);
    }
};