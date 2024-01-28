using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carrot : MonoBehaviour
{
    // Start is called before the first frame update
    int attackPhase = 0;
    Transform target;
    Vector3 Dire;

    public int Damage;

    public GameObject Particles;

    float Speed=1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attackPhase == 1)
        {

            transform.position += Vector3.up * .02f;
            if (transform.position.y > 30)
                StartCoroutine(AttackMotion());
        }
        else if (attackPhase == 2)
        {

            transform.position += Dire * Time.deltaTime*Speed/3;
            Speed *= 1.01f;
            
        }
        Debug.Log(GetComponent<Rigidbody>().IsSleeping());

    }

    public void Attack(Transform tar)
    {
        target = tar;
        attackPhase = 1;
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody>().isKinematic=true;

    }

    public IEnumerator AttackMotion()
    {
        transform.LookAt(target, Vector3.up);
        attackPhase = 0;
        yield return new WaitForSeconds(1);
        Dire = target.position - transform.position;
        Dire.Normalize();
        transform.rotation = Quaternion.LookRotation(-transform.up,- Dire);
        transform.rotation = Quaternion.LookRotation( -transform.forward,transform.up);

        attackPhase = 2;

        yield return new WaitForSeconds(.5f);
        Particles.SetActive(true);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Debug.Log("CarrotHit");
            GetComponent<Rigidbody>().isKinematic = false;
            collision.gameObject.GetComponent<PlayerHP>().HP -= Damage;

        }


        if (attackPhase == 2)
        {
            attackPhase = 0;
            Speed = 1;

            Particles.SetActive(false);
        }
    }


    }
