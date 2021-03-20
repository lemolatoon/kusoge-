using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TankController_Battle : MonoBehaviour
{

    private Vector3 velocity;

    private GameObject masterObj;
    private GameMaster_Battle master;

    // public GameObject bullet;
    public GameObject BallBullet;

    public float forwardSpeed = 1.0f;
    public float sideSpeed = 1.0f;

    private GameObject tower;
    public GameObject Tower {
        get {return tower;}
    }
    public LayerMask mask;
    public Vector3 currentMousePos = new Vector3(0, 0, 0);
    private Rigidbody rb;
    
    void Awake() { //GameMasterに値を入れる処理
        masterObj = GameObject.Find("GameMaster");
        master = masterObj.GetComponent<GameMaster_Battle>();
        master.smallTank = gameObject;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tower = this.transform.Find("Tank").gameObject.transform.Find("SmallTank_Tower").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LookAt(Vector3 vector) {
        tower.transform.LookAt(vector);
    }

    public void thirdMove(float vertical, float horizontal, float deltaTime) {
        //↑-z
        //+→-x    軸の説明
        Vector3 delta_r = new Vector3(horizontal * forwardSpeed * deltaTime, 0, vertical * sideSpeed * deltaTime);

        Vector3 r = this.transform.position + delta_r;

        this.transform.LookAt(r);

        this.transform.position = r;
    }

    public void forwardMove(float deltaTime) {
        this.transform.position += forwardSpeed * deltaTime * this.transform.forward;
    }

    public void shoot(Vector3 shotPos) {
        if(tower == null) {
            return;
        }
        GameObject bull = Instantiate(BallBullet, tower.transform.position + 1.0f * tower.transform.forward, BallBullet.transform.rotation) as GameObject;
        bull.transform.LookAt(shotPos);
        bull.GetComponent<BulletController>().shoot(shotPos);
    }

}
