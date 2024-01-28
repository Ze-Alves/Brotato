using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corn : MonoBehaviour
{
    Transform target;

    public List<CornBullet> activeBullets;
    public List<CornBullet> inactiveBullets;

    public bool active;

    public float FireRate;
    public int Rounds;
    public float offsetRange;

    int attackPhase;

    public int HeightFloat;

    public GameObject Eyes, XEyes;
    void Start()
    {
       foreach(CornBullet bullet in inactiveBullets)
        {
            bullet.daddyCorn = this;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if (attackPhase == 1)
        {
            transform.position += Vector3.up * .02f;
            if (transform.position.y > HeightFloat)
            {
                active = true;
                transform.LookAt(target);
                StartCoroutine(ShootCorn());
                attackPhase = 2;
            }
        }
        if (active)
        {
            Debug.Log("Roda");
            transform.LookAt(target);
            transform.rotation = Quaternion.LookRotation(-transform.up, transform.forward);
        }

    }


    public void Attack(Transform tar)
    {
        target = tar;
        attackPhase = 1;

        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<NPCRandomMovement>().enabled = false;
        Debug.Log("RAU");
    }

   
    IEnumerator ShootCorn()
    {
        int remaniningShots = Rounds;
        while (remaniningShots > 0)
        {
            yield return new WaitForSeconds(1);
            inactiveBullets[0].transform.position = transform.position+ transform.up*1.1f;
            inactiveBullets[0].gameObject.SetActive(true);
            Vector3 offset = new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), Random.Range(-.1f, .1f));
            
            inactiveBullets[0].Direction = (transform.up*1.1f+offset*offsetRange).normalized;
            activeBullets.Add(inactiveBullets[0]);
            inactiveBullets.Remove(inactiveBullets[0]);

            remaniningShots--;
            
        }
        active = false;
        attackPhase = 0;

        StartCoroutine(WakeUP());
        yield return null;
    }

    IEnumerator WakeUP()
    {
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody>().isKinematic = false;
        XEyes.SetActive(true);
        Eyes.SetActive(false);
        yield return new WaitForSeconds(2);
        GetComponent<NPCRandomMovement>().enabled = true;

        XEyes.SetActive(false);
        Eyes.SetActive(true);
    }

}
