using UnityEngine;
using UnityEngine.Events;

public class LevelSwitch : MonoBehaviour
{
    public UnityAction<bool> ChangeInteractable;
    public UnityAction BounceCube;
    public UnityAction HideObjects;

    [SerializeField] private TaskText _task;
    [SerializeField] private LoadingScreen _loadingScreen;

    private DataSelector _dataSelector;
    private CubeSpawner _spawner;
    private int _levelNumber = 0;

    private const int MaxElementsInRow = 3;
    private const int LevelAdditive = 3;
    private const int LevelCount = 3;

    private void Awake()
    {
        _dataSelector = GetComponent<DataSelector>();
        _spawner = GetComponent<CubeSpawner>();
        _loadingScreen.FinishReset += StartNewGame;
        StartNewGame();
    }

    private void StartNewGame()
    {
        _levelNumber = 1;
        CreateLevel();
        BounceCube?.Invoke();
        _task.FadeIn();
    }

    private void CreateLevel()
    {
        int elementsCount = _levelNumber * LevelAdditive;
        _spawner.InitializeSetOfCubes(elementsCount, MaxElementsInRow);
        _task.SetTaskText(_dataSelector.TargetValue);
        ChangeInteractable?.Invoke(true);
    }

    public bool CheckAnswer(string value)
    {
        if (value == _dataSelector.TargetValue)
        {
            ChangeInteractable?.Invoke(false);
            return true;
        }
        else
            return false;
    }

    public void FinishLevel()
    {
        if (_levelNumber < LevelCount)
        {
            _levelNumber++;
            CreateLevel();
        }
        else
        {
            _loadingScreen.ShowResetScreen();
        }
    }

    public void ResetLevel()
    {
        _dataSelector.ClearTargetList();
        _loadingScreen.ShowFadeTransition(() => HideObjects?.Invoke());
    }

    private void OnDestroy()
    {
        _loadingScreen.FinishReset -= StartNewGame;
    }
}
