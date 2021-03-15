using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TankController_Battle : MonoBehaviour
{

    private Vector3 velocity;

    public GameObject master;

    public GameObject bullet;

    public float forwardSpeed = 1.0f;
    public float sideSpeed = 1.0f;

     public Vector3 centerOfMass; 
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {   
        rb = GetComponent<Rigidbody>();
        // rb.centerOfMass = this.centerOfMass;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void thirdMove(float vertical, float horizontal, float deltaTime) {
        //↑-z
        //+→-x    軸の説明
        Vector3 delta_r = new Vector3(horizontal * forwardSpeed * deltaTime, 0, vertical * sideSpeed * deltaTime);

        Vector3 r = this.transform.position + delta_r;

        this.transform.LookAt(r);

        this.transform.position = r;
    }

    public void shoot(Vector3 shotPos) {
        GameObject bull = Instantiate(bullet, this.transform.position + 1.0f * this.transform.forward, bullet.transform.rotation) as GameObject;
        bull.transform.LookAt(shotPos);
        this.transform.LookAt(shotPos);
    }

}
