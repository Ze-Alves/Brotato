using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomato : MonoBehaviour
{
    public float TimeinAir;
    public float Falling_Speed;
    public bool Activate=false;
    Vector3 Current_speed=Vector3.zero;
    Transform target;
    public GameObject areaExplosion;

    public int area;

    public float OffsetRange;

    bool Flying=false;

    public int Damage;

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

            Vector3 targetPos = target.position;
            targetPos.y = 0.1f;

            Vector3 targetOffset = new Vector3(Random.Range(-OffsetRange, OffsetRange),0, Random.Range(-OffsetRange, OffsetRange));
            targetPos+=targetOffset;

            Debug.Log(targetPos);

            Vector3 targetDir = targetPos - transform.position;

            Current_speed += targetDir.normalized * (targetDir.magnitude/2);

            Instantiate(areaExplosion, targetPos, Quaternion.identity);

            Flying = true;
            GetComponent<Rigidbody>().Sleep();
            StartCoroutine(AttackFlying());
        }

        
        if (Flying)
        {
            Current_speed.y -= Falling_Speed * Time.deltaTime;
            transform.position += Current_speed * Time.deltaTime;
        }

    }


    public void Attack(Transform tar)
    {
        target= tar;

        Activate = true;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Flying)
        {
            Collider[] coliders = Physics.OverlapSphere(transform.position, area);

            foreach (var colider in coliders)
            {
                if (colider.gameObject.layer == 7)
                {
                    Debug.Log("Damage");
                    colider.gameObject.GetComponent<PlayerHP>().HP -= Damage;
                }
            }

            Flying = false;
        }
        GetComponent<Rigidbody>().WakeUp();

        

    }

    IEnumerator AttackFlying()
    {
        yield return new WaitForSeconds(TimeinAir-0.1f);

        Flying = false;
        GetComponent<Rigidbody>().WakeUp();
    }
}
