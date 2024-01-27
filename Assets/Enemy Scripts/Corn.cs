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
        if(active)
        transform.LookAt(target);

    }


    public void Attack(Transform tar)
    {
        target = tar;
        active = true;
        transform.LookAt(target);
        StartCoroutine(ShootCorn());

    }

   
    IEnumerator ShootCorn()
    {
        int remaniningShots = Rounds;
        while (remaniningShots > 0)
        {
            inactiveBullets[0].transform.position = transform.position;
            inactiveBullets[0].gameObject.SetActive(true);
            Vector3 offset = new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), Random.Range(-.1f, .1f));
            
            inactiveBullets[0].Direction = (transform.forward+offset*offsetRange).normalized;
            activeBullets.Add(inactiveBullets[0]);
            inactiveBullets.Remove(inactiveBullets[0]);

            remaniningShots--;
            yield return new WaitForSeconds(1);
        }

        yield return null;
    }

}
