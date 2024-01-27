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
