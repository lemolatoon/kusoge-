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
    private GameObject joystickObj;

    private Vector3 mousePositionInCanvas {get; set;}
    private Vector3 currentMousePos {get; set;}
    public Vector3 CurrentMousePos {
        get{return currentMousePos;}
    }

    // Start is called before the first frame update
    void Start()
    {
        tankController = this.GetComponent<TankController_Battle>();
        GameMaster_Battle gm = this.master.GetComponent<GameMaster_Battle>();
        joystickObj = gm.joystick;
        joystick = joystickObj.GetComponent<VariableJoystick>();
        mousePositionInCanvas = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = readVertical(); //前なら+後ろなら-で取得(-1~+1)
        float horizontal = readHorizontal(); //右なら+左なら-で取得

        // Debug.Log("Vertical:" + vertical);
        // Debug.Log("Horizontal:" + horizontal);

        tankController.thirdMove(vertical, horizontal, Time.fixedDeltaTime);


        //マルチタッチ処理
        //Debug.Log(joystick.backgroundPos);
        // Debug.Log(Input.mousePosition);
        if(Input.touchCount > 0 && joystickObj.activeSelf) {
            Touch[] myTouches = Input.touches;
            for(int i = 0; i < myTouches.Length; i++) {
                Touch touch = myTouches[i];
                var pos = touch.position;
                if (pos.x >= 400 || pos.y >= 400) { //joystickの範囲でないならば 400 400
                    Debug.Log("mousePos設定");
                    this.mousePositionInCanvas = pos;
                    if(touch.phase == TouchPhase.Began) { //さらに指が触れたときであれば
                        tankController.shoot(this.currentMousePos);
                    }
                }
            }
        } else {
            this.mousePositionInCanvas = Input.mousePosition;
        }



        //tankの向く方向の処理
        if(!joystickObj.activeSelf) { //joystickがオフのとき
            Camera cam = Camera.main; //camera取得
            Vector3 mousePos = this.mousePositionInCanvas;
            //Debug.Log(mousePos);
            Ray ray = cam.ScreenPointToRay(mousePos); //cameraからマウスの場所に向かってのrayをつくる
            RaycastHit[] raycastHitList =  Physics.RaycastAll(ray, Mathf.Infinity, tankController.mask);
            if(raycastHitList.Length != 0) {
                RaycastHit raycastHit = raycastHitList[0]; //当たった物体(plane)を取得

                float distance = Vector3.Distance(cam.transform.position, raycastHit.point); //距離をはかる
                Vector3 Pos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, distance)); //ワールド座標に変換
                Pos.y = tankController.tower.transform.position.y; //yをtowerに合わせる
                this.currentMousePos = Pos;
                tankController.tower.transform.LookAt(Pos);
            }
        }


        if(!joystickObj.activeSelf && Input.GetMouseButtonDown(0)) {
            tankController.shoot(this.currentMousePos);
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
