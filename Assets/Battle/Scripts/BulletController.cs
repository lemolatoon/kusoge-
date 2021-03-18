using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float forwardSpeed = 3.0f;
    // private Rigidbody rb;
    public float power = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        // rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void shoot(Vector3 shotPos) {
        var p = power * this.transform.forward;
        this.GetComponent<Rigidbody>().AddForce(p);
    } 

}
