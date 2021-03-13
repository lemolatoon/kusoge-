using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TankController : MonoBehaviour
{

    private Vector3 velocity;
    public float forwardSpeed = 7.0f;
    public float backSpeed = 2.0f;
    public float rotateSpeed = 2.0f;

    public GameObject master;
    private VariableJoystick joystick;

    private float v;
    private float h;

    public Vector3 centerOfMass; 
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        joystick = master.GetComponent<GameMaster>().joystick.GetComponent<VariableJoystick>();
        
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = this.centerOfMass;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.v = readVertical(); //前なら+後ろなら-で取得(-1~+1)
        this.h = readHorizontal(); //右なら+左なら-で取得

        velocity = new Vector3(0, 0, v); //移動量ベクトル
        if(v > 0.1) {
            velocity *= forwardSpeed;  //speedの分だけ掛ける
        } else if(v < -0.1) {
            velocity *= backSpeed;
        }

        velocity = transform.TransformDirection(velocity); //進行方向にベクトルをセット

        transform.localPosition += velocity * Time.fixedDeltaTime;

        transform.Rotate(0, h * rotateSpeed, 0);

        // Debug.Log("絶対回転量" + transform.rotation);
        
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
