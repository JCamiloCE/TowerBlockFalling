using System.Collections.Generic;
using UnityEngine;

public class ChangeMovement_Testing : MonoBehaviour
{
    [SerializeField] private List<GameObject> _movements;
    private int _currentIndex = 0;

    public void ChangeIndex() 
    {
        _movements[_currentIndex].SetActive(false);
        _currentIndex++;
        if(_currentIndex >= _movements.Count)
            _currentIndex = 0;
        _movements[_currentIndex].SetActive(true);
    }

}
