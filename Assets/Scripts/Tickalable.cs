using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tickalable : MonoBehaviour
{
    [SerializeField] private int _maxTickleHP;
    [SerializeField] private int _tickleStaggerAmount;
    [SerializeField] private float _tickleStaggerTime;
    private int _currentTickleHP;
    private int _currentTickleStaggerAmount;
    public void GetTickled(int tickleAmout)
    {
        _currentTickleHP -= tickleAmout;
        _currentTickleStaggerAmount += tickleAmout;
    }
}
