using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tickalable : MonoBehaviour
{
    [SerializeField] private float _maxTickleHP;
    [SerializeField] private float _tickleStaggerAmount;
    [SerializeField] private float _tickleStaggerTime;
    private float _currentTickleHP;
    private float _currentTickleStaggerAmount;

    private void Awake()
    {
        _currentTickleHP = _maxTickleHP;
        _currentTickleStaggerAmount = _tickleStaggerAmount;
    }

    public void GetTickled(float tickleAmout)
    {
        _currentTickleHP -= tickleAmout;
        _currentTickleStaggerAmount += tickleAmout;
        Debug.Log("Tickled: " + tickleAmout + " | Current Tickle HP: " + _currentTickleHP + " | Current Tickle Stagger Amount: " + _currentTickleStaggerAmount);
    }
}
