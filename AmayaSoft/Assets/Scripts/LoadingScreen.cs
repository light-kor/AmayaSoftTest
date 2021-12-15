using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.Events;

public class LoadingScreen : MonoBehaviour
{
    public UnityAction FinishReset;

    [SerializeField] private ResetButton _resetButton;
    private Image _image;
    private Sequence _sequence;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void ShowFadeTransition(Action setInFade)
    {
        _sequence = DOTween.Sequence();

        _sequence.Append(_image.DOFade(1f, 1f));
        _sequence.AppendCallback(() => setInFade());
        _sequence.AppendInterval(0.5f);
        _sequence.Append(_image.DOFade(0f, 1f));
        _sequence.AppendInterval(1f);
        _sequence.AppendCallback(() => FinishReset?.Invoke());
    }

    public void ShowResetScreen()
    {
        _sequence = DOTween.Sequence();

        _sequence.Append(_image.DOFade(0.3f, 0.5f));
        _sequence.AppendCallback(() => ActivateResetButton());       
    }

    private void ActivateResetButton()
    {
        _resetButton.gameObject.SetActive(true);
    }
}
