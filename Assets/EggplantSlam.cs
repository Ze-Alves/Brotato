using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggplantSlam : MonoBehaviour
{

    public AnimationCurve AscendingCurve,DecendingCurve;
    [SerializeField] float _slamRadius = 10f;
    [SerializeField] float _slamForce = 100f;

    int SlamState;

    public int damage;

    public float timer;

    public GameObject Particles;

  
    void Update()
    {
        if (SlamState == 1)
        {
            //if (AscendingCurve.Evaluate(timer) < 0)
            //    Particles.SetActive(true);
            timer += Time.deltaTime;

            transform.position += Vector3.up * Time.deltaTime * AscendingCurve.Evaluate(timer/2)  * 50;
            Debug.Log(AscendingCurve.Evaluate(.5f));
            if (transform.position.y > 30) 
                SlamState = 2;
        }
        else if (SlamState == 2)
        {
            //if(AscendingCurve.Evaluate(timer)<0)
            //Particles.SetActive(true);
            timer += Time.deltaTime;

            transform.position += Vector3.up * Time.deltaTime * AscendingCurve.Evaluate(timer/2) * 50;
            if (transform.position.y < 0)
            {   
                SlamState = 0;
                PushEnemies();
                transform.position -= Vector3.up * transform.position.y;
                timer = 0;
                //Particles.SetActive(false);
            }

        }


    }

    public void Slam()
    {
        SlamState = 1;
    }

    private void PushEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _slamRadius);

        foreach (Collider collider in colliders)
        {
            var rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Debug.Log("Pushing: " + collider.name);
                Vector3 Dir = collider.transform.position - transform.position;
                Dir.y = 10;
                Dir.Normalize();
                rb.AddForce(Dir * _slamForce, ForceMode.Impulse);
                if (rb.gameObject.layer == 7)
                {
                    rb.gameObject.GetComponent<PlayerHP>().HP -= damage;
                }
            }
        }
    }
}
