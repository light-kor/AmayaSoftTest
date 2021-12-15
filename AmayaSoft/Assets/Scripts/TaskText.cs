using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TaskText : MonoBehaviour
{
    [SerializeField] private LevelSwitch _level;
    [SerializeField] private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
        _level.HideObjects += SetZeroAlpha;
    }

    public void SetTaskText(string value)
    {
        _text.text = $"Find {value}";
    }

    public void FadeIn()
    {
        SetZeroAlpha();
        _text.DOFade(1f, 2f);
    }

    private void SetZeroAlpha()
    {
        Color buffer = _text.color;
        buffer.a = 0f;
        _text.color = buffer;
    }

    private void OnDestroy()
    {
        _level.HideObjects -= SetZeroAlpha;
    }
}
