using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LocalizationWindow : MonoBehaviour
{
    [SerializeField] private Button _ruButton;
    [SerializeField] private Button _enButton;
    [SerializeField] private Button _frButton;

    private void Start()
    {
        _ruButton.onClick.AddListener(() => ChangeLocale(2));
        _enButton.onClick.AddListener(() => ChangeLocale(0));
        _frButton.onClick.AddListener(() => ChangeLocale(1));
    }

    private void ChangeLocale(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }
}
