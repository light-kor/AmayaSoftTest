using System.Collections.Generic;
using UnityEngine;

public class DataSelector : MonoBehaviour
{
    public string TargetValue => _targetValue;

    [SerializeField] private List<GameKit> _listOfSets;
    private List<string> _usedTargetValues = new List<string>();
    private System.Random _random = new System.Random();
    private string _targetValue;

    public List<Character> SelectUniqueElements(int count)
    {        
        if (_listOfSets.Count > 0)
        {
            GameKit gameKit = _listOfSets[_random.Next(0, _listOfSets.Count)];
            List<Character> buffer = new List<Character>();
            List<string> usedValues = new List<string>();

            while (buffer.Count < count)
            {
                Character character = gameKit.GetRandomElement();

                if (buffer.Count == 0)
                {
                    string usedTarget = _usedTargetValues.Find(c => c == character.Value);
                    if (usedTarget == null)
                        _usedTargetValues.Add(character.Value);
                    else
                        continue;
                }

                Character usedItem = buffer.Find(c => c.Value == character.Value);

                if (usedItem == null)
                {
                    buffer.Add(character);
                }
            }

            SetTargetElement(buffer);
            return buffer;
        }
        else return null;
    }

    public void ClearTargetList()
    {
        _usedTargetValues.Clear();
    }

    private void SetTargetElement(List<Character> list)
    {
        int num = _random.Next(0, list.Count);
        _targetValue = list[0].Value;

        Character buffer = list[0];
        list[0] = list[num];
        list[num] = buffer;
    }
}
