using UnityEngine;
using UnityEngine.UIElements;

public class ButtonTest : MonoBehaviour
{
    Button _playBtn;
    Button _optionsBtn;
    Button _quitBtn;
    Button _closeSettingsBtn;
    [SerializeField] SoundName _clickSound;

    VisualElement _settingsPage;

    private void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        _playBtn = root.Q<Button>("PlayBtn");
        _optionsBtn = root.Q<Button>("OptionsBtn");
        _quitBtn = root.Q<Button>("QuitBtn");
        _settingsPage = root.Q<VisualElement>("OptionsPage");
        _closeSettingsBtn = _settingsPage.Q<Button>("CloseBtn");
        _settingsPage.style.display = DisplayStyle.None;

        _playBtn.RegisterCallback<ClickEvent>(OnPlayBtnClicked);
        _optionsBtn.RegisterCallback<ClickEvent>(OnOptionsClicked);
        _closeSettingsBtn.RegisterCallback<ClickEvent>(OnOptionsCloseBtnClicked);
    }

    void OnPlayBtnClicked(ClickEvent clickEvent)
    {
        Debug.Log("Play!");

        SoundManager.Instance.PlaySound(_clickSound, pitch: Random.Range(.8f,1.5f));
    }

    void OnOptionsClicked(ClickEvent clickEvent)
    {
        Debug.Log("Options!");
        _settingsPage.style.display = DisplayStyle.Flex;

    }

    void OnOptionsCloseBtnClicked(ClickEvent clickEvent)
    {
        _settingsPage.style.display = DisplayStyle.None;

    }
}
