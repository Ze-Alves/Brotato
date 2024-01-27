using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShakeController : MonoBehaviour
{
    public static CameraShakeController Instance { get; private set; }
    private CinemachineFreeLook cmFreeCam;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);

        cmFreeCam = GetComponent<CinemachineFreeLook>();
    }

    public void DoShake(float amplitudeGain, float frequencyGain, float shakeDuration)
    {
        StartCoroutine(Shake(amplitudeGain, frequencyGain, shakeDuration));
    }

    public IEnumerator Shake(float amplitudeGain, float frequencyGain,float shakeDuration)
    {
        Noise(amplitudeGain, frequencyGain);
        yield return new WaitForSeconds(shakeDuration);
        Noise(0, 0);
    }

    void Noise(float amplitude, float frequency)
    {
        cmFreeCam.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
        cmFreeCam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
        cmFreeCam.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;

        cmFreeCam.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;
        cmFreeCam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;
        cmFreeCam.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;

    }
}
