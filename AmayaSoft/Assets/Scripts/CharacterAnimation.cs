using DG.Tweening;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private ParticleSystem _starsParticle;
    [SerializeField] private Cube _cube;

    private SpriteRenderer _sprite;
    private Vector3 _normalScale;
    private Sequence _sequence;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _normalScale = transform.localScale;
    }

    public void AnimateWrongAnswer()
    {
        _sequence = DOTween.Sequence();

        _sequence.Append(transform.DOShakePosition(1f, 0.08f, vibrato: 5, randomness: 0, fadeOut: false).SetEase(Ease.InBounce));
        _sequence.Append(transform.DOLocalMove(Vector3.zero, 0.3f, snapping: false));
    }

    public void AnimateCorrectAnswer()
    {
        _sequence = DOTween.Sequence();
        _starsParticle.Play();

        _sequence.Append(transform.DOScale(_normalScale * 1.3f, 0.3f).SetEase(Ease.OutExpo));
        _sequence.Append(transform.DOScale(_normalScale, 1f).SetEase(Ease.OutBounce));
        _sequence.AppendInterval(0.5f);
        _sequence.AppendCallback(_cube.ReportAnimationEnding);
    }

    public void SetSprite(Sprite sprite)
    {
        _sprite.sprite = sprite;
    }
}
