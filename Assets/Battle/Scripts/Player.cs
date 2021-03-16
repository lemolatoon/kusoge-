using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{   

    private TankController_Battle tankController;
    public float forwardSpeed = 1.0f;

    public float sideSpeed = 1.0f;

    // public float backSpeed = 2.0f;
    // public float rotateSpeed = 2.0f;

    public GameObject master;
    private VariableJoystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        tankController = this.GetComponent<TankController_Battle>();
        joystick = this.master.GetComponent<GameMaster_Battle>().joystick.GetComponent<VariableJoystick>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float vertical = readVertical(); //前なら+後ろなら-で取得(-1~+1)
        float horizontal = readHorizontal(); //右なら+左なら-で取得

        Debug.Log("Vertical:" + vertical);
        Debug.Log("Horizontal:" + horizontal);

        tankController.thirdMove(vertical, horizontal, Time.fixedDeltaTime);

        if(Input.GetKeyDown(KeyCode.C)) {
            tankController.shoot(new Vector3(0, 1.89f, 0));
        }
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

}
