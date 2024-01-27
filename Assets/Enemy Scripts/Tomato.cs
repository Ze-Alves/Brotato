using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomato : MonoBehaviour
{
    public float TimeinAir;
    public float Falling_Speed;
    public bool Activate=false;
    Vector3 Current_speed=Vector3.zero;
    float timer=0;
    public Transform target;
    public GameObject area;

    public float OffsetRange;

    bool Flying=false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Activate)
        {
            Vector3 speed = Vector3.zero;
            speed.y =Falling_Speed*TimeinAir/2;
            Current_speed = speed;
            Activate = false;
            timer = 0;

            Vector3 targetPos = target.position;
            targetPos.y = 0;

            Vector3 targetOffset = new Vector3(Random.Range(-OffsetRange, OffsetRange),0, Random.Range(-OffsetRange, OffsetRange));
            targetPos+=targetOffset;

            Debug.Log(targetPos);

            Vector3 targetDir = targetPos - transform.position;

            Current_speed += targetDir.normalized * (targetDir.magnitude/2);

            Instantiate(area, targetPos, Quaternion.identity);

            Flying = true;
            GetComponent<Rigidbody>().Sleep();
            StartCoroutine(AttackFlying());
        }

        timer += Time.deltaTime;

        
        if (Flying)
        {
            Current_speed.y -= Falling_Speed * Time.deltaTime;
            transform.position += Current_speed * Time.deltaTime;


        }






    }

    IEnumerator AttackFlying()
    {
        yield return new WaitForSeconds(TimeinAir);

        Flying = false;
        GetComponent<Rigidbody>().WakeUp();
    }
}
