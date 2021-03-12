using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{

    private Vector3 velocity;
    public float forwardSpeed = 7.0f;
    public float backSpeed = 2.0f;
    public float rotateSpeed = 2.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical"); //前なら+後ろなら-で取得(-1~+1)
        float h = Input.GetAxis("Horizontal"); //右なら+左なら-で取得

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

}
