using System;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    private List<Cube> _poolOfCubes = new List<Cube>();
    private DataSelector _dataSelector;
    private LevelSwitch _level;
    private float _cubeSize;

    private void Awake()
    {
        _dataSelector = GetComponent<DataSelector>();
        _level = GetComponent<LevelSwitch>();
        _level.HideObjects += DeactivateAllCubes;
        _cubeSize = _cubePrefab.transform.localScale.x;
    }

    private void InstantiateNewCubes(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Cube spawned = Instantiate(_cubePrefab, transform);
            spawned.Init(_level);
            _poolOfCubes.Add(spawned);
        }
    }

    public void InitializeSetOfCubes(int count, int maxInRow)
    {
        if (count > _poolOfCubes.Count)
            InstantiateNewCubes(count - _poolOfCubes.Count);

        DeactivateAllCubes();
        SetCubesData(count);

        float rowsCount = (float)Math.Ceiling((double)count / (double)maxInRow);
        float startPosX = -_cubeSize / 2 * (maxInRow - 1);
        float startPosY = _cubeSize / 2 * (rowsCount - 1);

        float xPos = startPosX, yPos = startPosY;
        for (int i = 0; i < count; i++)
        {
            _poolOfCubes[i].transform.position = new Vector3(xPos, yPos, 0f);
            _poolOfCubes[i].gameObject.SetActive(true);

            xPos += _cubeSize;
            if (i != 0 && (i + 1) % maxInRow == 0)
            {
                yPos -= _cubeSize;
                xPos = startPosX;
            }
        }
    }

    private void SetCubesData(int count)
    {
        List<Character> _lvlElements = _dataSelector.SelectUniqueElements(count);

        for (int i = 0; i < count; i++)
        {
            _poolOfCubes[i].SetCubeData(_lvlElements[i]);
        }
    }

    private void DeactivateAllCubes()
    {
        for (int i = 0; i < _poolOfCubes.Count; i++)
        {
            _poolOfCubes[i].gameObject.SetActive(false);
        }
    }
   
    private void OnDestroy()
    {
        _level.HideObjects -= DeactivateAllCubes;
    }
}
