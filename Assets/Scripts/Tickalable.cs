using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tickalable : MonoBehaviour
{
    [SerializeField] private float MaxTickleHP;
    [SerializeField] private float _tickleStaggerAmount;
    [SerializeField] private float _tickleStaggerTime;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Slider _tickleSlider;
    public float CurrentTickleHP;
    private float _currentTickleStaggerAmount;

    private void Awake()
    {
        CurrentTickleHP = MaxTickleHP;
        _currentTickleStaggerAmount = _tickleStaggerAmount;
    }

    public void GetTickled(float tickleAmout)
    {
        CurrentTickleHP -= tickleAmout;
        _currentTickleStaggerAmount += tickleAmout;

        if(CurrentTickleHP <= 0)
        {
            CurrentTickleHP = 0;
        }
        
        UpdateShader();
    }

    private void UpdateShader()
    {
        Debug.Log("New ratio is: " + CurrentTickleHP / MaxTickleHP);
        _tickleSlider.value =1- CurrentTickleHP / MaxTickleHP;
        _meshRenderer.materials[0].SetFloat("_PeelAmount", 1- CurrentTickleHP / MaxTickleHP);
    }
}
