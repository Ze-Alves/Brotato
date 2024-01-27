using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggplantSlam : MonoBehaviour
{

    public AnimationCurve AscendingCurve,DecendingCurve;

    int SlamState;

    public int damage;

    public float timer;

  
    void Update()
    {
        if (SlamState == 1)
        {
            timer += Time.deltaTime;

            transform.position += Vector3.up * Time.deltaTime * AscendingCurve.Evaluate(timer/2)  * 50;
            Debug.Log(AscendingCurve.Evaluate(.5f));
            if (transform.position.y > 30)
                SlamState = 2;
        }
        else if (SlamState == 2)
        {
            timer += Time.deltaTime;

            transform.position += Vector3.up * Time.deltaTime * AscendingCurve.Evaluate(timer/2) * 50;
            if (transform.position.y < 0)
            {
                SlamState = 0;
                transform.position -= Vector3.up * transform.position.y;
                timer = 0;
            }

        }

    }

    public void Slam()
    {
        SlamState = 1;
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("SLAM");

        if (other.gameObject.layer != 6 && SlamState==2)
        {

            Vector3 dir = other.transform.position - transform.position;
            dir.y = 20;
            dir.Normalize();
            other.attachedRigidbody.AddForce(dir * 200, ForceMode.Impulse);     
        }
    }
}
