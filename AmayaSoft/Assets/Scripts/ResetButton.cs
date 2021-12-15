using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    [SerializeField] private LevelSwitch _level;
    private Image _image;
    private Button _button;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnEnable()
    {
        SetZeroAlpha();
        _image.DOFade(1f, 0.3f);
    }

    private void HideButton()
    {
        _image.DOFade(0f, 0.5f);
        gameObject.SetActive(false);
    }

    private void OnClick()
    {
        _level.ResetLevel();
        HideButton();
    }

    private void SetZeroAlpha()
    {
        Color buffer = _image.color;
        buffer.a = 0f;
        _image.color = buffer;
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
    }
}
