using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Dash : MonoBehaviour
{
    [SerializeField] private float _dashDistance;
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _dashCooldown;

    [Header("Animation")]
    [SerializeField] private float _height;
    [SerializeField] private AudioClip _dashSFX;

    private Rigidbody _rb;
    private float _timeSinceLastDash = 0;
    private IndividualAudioPlayer _audioPlayer;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _timeSinceLastDash = _dashCooldown;
        _audioPlayer = GetComponent<IndividualAudioPlayer>();
    }

    private void Update()
    {
        _timeSinceLastDash += Time.deltaTime;
    }
    public void DoDash(MonoBehaviour disableMe )
    {
        if(_timeSinceLastDash <= _dashCooldown) return;
        Debug.Log("Dash");
        disableMe.enabled = false;

        _audioPlayer.PlayAudio(_dashSFX, 0.5f);
        transform.GetComponent<Animator>().SetTrigger("Dash");
        _timeSinceLastDash = 0;
        _rb.DOMove(transform.position + transform.forward * _dashDistance, _dashDuration)
            .OnComplete(() => disableMe.enabled = true);
    }

    public void DoDash(MonoBehaviour disableMe, float dashDistance, float dashDuration)
    {
        disableMe.enabled = false;
        _rb.DOMove(transform.position + transform.forward * dashDistance, dashDuration)
            .OnComplete(() => disableMe.enabled = true);
    }
}
