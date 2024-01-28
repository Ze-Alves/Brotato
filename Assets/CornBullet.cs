using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornBullet : MonoBehaviour
{
    public Vector3 Direction;
    public float Velocity;
    public float Lifetime;
    float LifetimeTrack;
    public Corn daddyCorn;
    public int Damage;
    void Start()
    {
        
    }

    private void OnEnable()
    {
        LifetimeTrack = Lifetime;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += Direction * Velocity * Time.deltaTime;
        if (LifetimeTrack < 0)
            Dead();
        LifetimeTrack -= Time.deltaTime;
    }


    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer ==7)
        {
            Debug.Log("CornHit");
            collision.gameObject.GetComponent<PlayerHP>().HP -= Damage;
        }

        Dead();

    }

    void Dead()
    {

        daddyCorn.activeBullets.Remove(this);
        daddyCorn.inactiveBullets.Add(this);
        LifetimeTrack = Lifetime;
        gameObject.SetActive(false);

    }
}
