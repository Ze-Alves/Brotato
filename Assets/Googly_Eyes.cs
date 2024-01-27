using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Googly_Eyes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 EyesPos = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            EyesPos.y += transform.parent.localScale.y * 0.125f;
        if (Input.GetKey(KeyCode.S))
            EyesPos.y -= transform.parent.localScale.y * 0.125f;
        if (Input.GetKey(KeyCode.A))
            EyesPos.x -= transform.parent.localScale.x * 0.125f;
        if (Input.GetKey(KeyCode.D))
            EyesPos.x += transform.parent.localScale.x * 0.125f;
        EyesPos.Normalize();
        EyesPos *= transform.parent.localScale.x * 0.125f;
        EyesPos.z -= .5f;

        transform.localPosition = EyesPos;
    }
}
