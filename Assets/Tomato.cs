using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomato : MonoBehaviour
{
    public float Initial_velocity;
    public float Falling_Speed;
    public bool Activate=false;
    Vector3 Current_speed=Vector3.zero;
    float timer=0;
    public Transform target;
    public GameObject area;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Activate)
        {
            Vector3 speed = Vector3.zero;
            speed.y = Initial_velocity;
            Current_speed = speed;
            Activate = false;
            timer = 0;

            Vector3 targetPos = target.position;
            targetPos.y = transform.position.y;

            Vector3 targetDir = targetPos - transform.position;

            Current_speed += targetDir.normalized * (targetDir.magnitude/2);

            Instantiate(area, targetPos, Quaternion.identity);
        }

        timer += Time.deltaTime;

        if (transform.position.y > 0)
        {
            Current_speed.y -= Falling_Speed * Time.deltaTime;
            Debug.Log(timer);
        }

        transform.position += Current_speed * Time.deltaTime;

        

        

    }


}
