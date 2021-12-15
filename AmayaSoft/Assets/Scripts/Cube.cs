using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cube : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CharacterAnimation _characterAnim;
  
    private LevelSwitch _level;
    private string _data;
    private bool _interactable = false;
    private Vector3 _normalScale;

    private void Awake()
    {
        _normalScale = transform.localScale;
    }

    public void Init(LevelSwitch levelConditions)
    {
        _level = levelConditions;
        _level.ChangeInteractable += SwitchInteractivity;
        _level.BounceCube += AnimateAppearance;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_interactable)
        {
            bool answer = _level.CheckAnswer(_data);
            if (answer == true)
                _characterAnim.AnimateCorrectAnswer();
            else
                _characterAnim.AnimateWrongAnswer();
        }       
    }

    public void SetCubeData(Character data)
    {
        _characterAnim.SetSprite(data.ValueSprite);
        _data = data.Value;
    }

    public void ReportAnimationEnding()
    {
        _level.FinishLevel();
    }

    private void SwitchInteractivity(bool interactivity)
    {
        _interactable = interactivity;
    }

    private void AnimateAppearance()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(_normalScale, 1.5f).SetEase(Ease.OutBounce);
    }

    private void OnDestroy()
    {
        _level.ChangeInteractable -= SwitchInteractivity;
    }
}
