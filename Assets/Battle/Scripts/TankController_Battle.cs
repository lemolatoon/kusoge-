using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TankController_Battle : MonoBehaviour
{

    private Vector3 velocity;
    public float forwardSpeed = 1.0f;

    public float sideSpeed = 1.0f;

    // public float backSpeed = 2.0f;
    // public float rotateSpeed = 2.0f;

    public GameObject master;
    private VariableJoystick joystick;

    private float v;
    private float h;

    public Vector3 centerOfMass; 
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        joystick = master.GetComponent<GameMaster_Battle>().joystick.GetComponent<VariableJoystick>();
        
        rb = GetComponent<Rigidbody>();
        // rb.centerOfMass = this.centerOfMass;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.v = readVertical(); //前なら+後ろなら-で取得(-1~+1)
        this.h = readHorizontal(); //右なら+左なら-で取得

        Debug.Log("Vertical:" + this.v);
        Debug.Log("Horizontal:" + this.h);

        //↑-z
        //+→-x    軸の説明

        Vector3 delta_r = new Vector3(this.h * forwardSpeed * Time.fixedDeltaTime, 0, this.v * sideSpeed * Time.fixedDeltaTime);

        Vector3 r = this.transform.position + delta_r;

        this.transform.LookAt(r);

        this.transform.position = r;

    }

    private float readVertical() {
        float v = Input.GetAxis("Vertical");
        if(Math.Abs(joystick.Vertical) >= Math.Abs(v)) { //もしjoystickの入力の方が大きいのなら
            return joystick.Vertical;
        } else {
            return v;
        }
    }

    private float readHorizontal() {
        float h = Input.GetAxis("Horizontal");
        if(Math.Abs(joystick.Horizontal) >= Math.Abs(h)) { //もしjoystickの入力の方が大きいのなら
            return joystick.Horizontal;
        } else {
            return h;
        }
    }

    public void setVertical(float v) {
        this.v = v;
    }

    public void setHorizontal(float h) {
        this.h = h;
    }

}
