using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameKit", menuName = "MyObjects/GameKit")]
public class GameKit : ScriptableObject
{
    [SerializeField] private List<Character> _kit = new List<Character>();
    private System.Random _random = new System.Random();

    public Character GetRandomElement()
    {
        int randomNumber = _random.Next(0, _kit.Count);
        return _kit[randomNumber];
    }
}

[Serializable]
public class Character
{
    [SerializeField] private string _value;
    [SerializeField] private Sprite _sprite;

    public string Value => _value;
    public Sprite ValueSprite => _sprite;
}
