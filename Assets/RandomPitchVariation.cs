using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPitchVariation : MonoBehaviour
{
    [SerializeField] private float _minPitch = 0.8f;
    [SerializeField] private float _maxPitch = 1.2f;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _audioSource.pitch = Random.Range(_minPitch, _maxPitch);
    }
}
