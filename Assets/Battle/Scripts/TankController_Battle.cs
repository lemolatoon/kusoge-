using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TankController_Battle : MonoBehaviour
{

    private Vector3 velocity;

    private GameObject masterObj;

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
    
    // Start is called before the first frame update
    void Start()
    {   
        masterObj = GameObject.Find("GameMaster");
        rb = GetComponent<Rigidbody>();
        tower = GameObject.FindWithTag("tower");
    }

    // Update is called once per frame
    void Update()
    {

        // Camera cam = Camera.main; //camera取得
        // Vector3 mousePos = Input.mousePosition;
        // Debug.Log(mousePos);
        // Ray ray = cam.ScreenPointToRay(mousePos); //cameraからマウスの場所に向かってのrayをつくる
        // RaycastHit[] raycastHitList =  Physics.RaycastAll(ray, Mathf.Infinity, mask);
        // if(raycastHitList.Length != 0) {
        //     RaycastHit raycastHit = raycastHitList[0]; //当たった物体(plane)を取得

        //     float distance = Vector3.Distance(cam.transform.position, raycastHit.point); //距離をはかる
        //     Vector3 Pos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, distance)); //ワールド座標に変換
        //     Pos.y = tower.transform.position.y; //yをtowerに合わせる
        //     currentMousePos = Pos;
        //     tower.transform.LookAt(Pos);
        // }

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

    public void shoot(Vector3 shotPos) {
        GameObject bull = Instantiate(BallBullet, tower.transform.position + 1.0f * tower.transform.forward, BallBullet.transform.rotation) as GameObject;
        bull.transform.LookAt(shotPos);
        bull.GetComponent<BulletController>().shoot(shotPos);
    }

}
