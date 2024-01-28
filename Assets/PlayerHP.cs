using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHP : MonoBehaviour
{
    public float HP;
    float maxHP;

    public Slider HPSlider;
    void Start()
    {
        maxHP = HP;
    }

    // Update is called once per frame
    void Update()
    {
        HPSlider.value = HP / maxHP;
    }
}
