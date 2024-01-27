using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggPlant : MonoBehaviour
{
    public List<carrot> carrots;
    public Transform target;
    public bool activate=false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activate)
        {
            foreach (carrot car in carrots)
            {
                car.Attack(target);
            }
            activate = false;
        }
    }
}
