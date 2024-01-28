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
    public GameObject areaExplosion,Eyes,XEyes;

    public int area;

    public float OffsetRange;

    bool Flying=false;

    public int Damage;

    float timer=0;

    public ParticleSystem explosion;

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
            

            Vector3 targetOffset = new Vector3(Random.Range(-OffsetRange, OffsetRange),0, Random.Range(-OffsetRange, OffsetRange));
            targetPos+=targetOffset;

            Debug.Log(targetPos);

            Vector3 targetDir = targetPos - transform.position;

            targetDir.y = 0;

            Current_speed += targetDir.normalized * (targetDir.magnitude/TimeinAir);

            targetPos.y = 0.5f;
            Instantiate(areaExplosion, targetPos, Quaternion.identity);

            Flying = true;
            StartCoroutine(AttackFlying());
        }

        
        if (Flying)
        {
            Current_speed.y -= Falling_Speed * Time.deltaTime;
            transform.position += Current_speed * Time.deltaTime;
            timer+= Time.deltaTime;
        }

    }


    public void Attack(Transform tar)
    {
        target= tar;

        Activate = true;
        GetComponent<NPCRandomMovement>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().Sleep();

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
            StartCoroutine(WakeUP());
            GetComponent<Rigidbody>().isKinematic = false;
            Debug.Log(timer);
        }




    }

    IEnumerator AttackFlying()
    {
        yield return new WaitForSeconds(TimeinAir-0.1f);

        Collider[] coliders = Physics.OverlapSphere(transform.position, area);

        foreach (var colider in coliders)
        {
            if (colider.gameObject.layer == 7)
            {
                Debug.Log("Damage");
                colider.gameObject.GetComponent<PlayerHP>().HP -= Damage;
            }
        }

        Debug.Log(timer);
        Flying = false;
        GetComponent<Rigidbody>().isKinematic = false;
        StartCoroutine(WakeUP());
    }

    IEnumerator WakeUP()
    {
        explosion.gameObject.SetActive(true);
        explosion.Play();
        XEyes.SetActive(true);
        Eyes.SetActive(false);
        yield return new WaitForSeconds(2);
        GetComponent<NPCRandomMovement>().enabled = true;
        XEyes.SetActive(false);
        Eyes.SetActive(true);
    }
}
