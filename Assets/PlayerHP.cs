using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHP : MonoBehaviour
{
    public float HP;
    float maxHP;

    public Slider HPSlider;
    public GameObject Lost;
    void Start()
    {
        maxHP = HP;
    }

    // Update is called once per frame
    void Update()
    {
        HPSlider.value = HP / maxHP;

        if (HP <= 0)
        {
            Lost.SetActive(true);
            StartCoroutine(WaitASec());
        }
    }

    IEnumerator WaitASec()
    {
        yield return new WaitForSeconds(2);
        Time.timeScale = 0;
    }
}
