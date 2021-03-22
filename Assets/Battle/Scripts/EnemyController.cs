using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject masterObj;
    private GameMaster_Battle master;
    private TankController_Battle player;
    private TankController_Battle myTank;
    private TankCollision tankCollision;
    private bool walking;

    void Awake() { //GameMasterに値を入れる処理
        Debug.Log("enemyStart()");
        masterObj = GameObject.Find("GameMaster");
        Debug.Log("gameMaster取得:" + masterObj);
        master = masterObj.GetComponent<GameMaster_Battle>();
        master.tankComputer = this.gameObject;
        Debug.Log("masterのtankcomputer：" + master.tankComputer);
    }
    void Start()
    {
        //GameMasterから値を受け取る処理
        player = master.smallTank.GetComponent<TankController_Battle>();
        Debug.Log("player取得:" + player);
        myTank = GetComponent<TankController_Battle>();
        Debug.Log(myTank + "---mytank");
        tankCollision = this.GetComponent<TankCollision>();

        StartCoroutine(Attacking());
    }

    // Update is called once per frame
    void Update()
    {
        if(!master.gameEnd) {
            myTank.Tower.transform.LookAt(player.transform.position);
        }
    }

    private IEnumerator RandomWalk() {
        walking = true;
        float time = Random.Range(3.0f, 9.0f);
        Debug.Log("randomwalk開始" + time);
        float now = Time.time;
        float passedTime = 0;
        float euler = Random.Range(0, 360);
        myTank.transform.Rotate(new Vector3(0, euler, 0));
        // myTank.transform.rotation = Quaternion.Euler(bestRotate());
        myTank.forwardMove(Time.fixedDeltaTime);
        for(int i = 0; passedTime < time; i++) {
            passedTime += Time.fixedDeltaTime;
            myTank.forwardMove(Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
            if(i > 10 && tankCollision.StayWall) {
                break;
            }
        }
        walking = false;
        Debug.Log("randomwalk終了");
    }

    private IEnumerator Attacking() {
        StartCoroutine(RandomWalk());
        while(true) {
            string tag = getCollider(myTank.Tower.transform.forward).tag;
            if(!walking && UnityEngine.Random.value < 0.7f) { //確率でランダムウォーク 0,7f
                walking = true;
                StartCoroutine(RandomWalk());
            } else if(Random.value < 0.5f && tag == "tank" && player != null) { //確率で射撃 0.5f            
                Debug.Log("shooot!!");
                myTank.shoot(player.transform.position);
            }

            yield return new WaitForSeconds(1.0f);
        }
    }

    private Collider getCollider(Vector3 direction) {
        // Ray ray = new Ray(myTank.Tower.transform.position, direction); 
        // Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1, false);
        // RaycastHit[] raycastHitList =  Physics.RaycastAll(ray, Mathf.Infinity);
        // if(raycastHitList.Length != 0) {
        //     Debug.Log("当たった数" + raycastHitList.Length);
        //     Debug.Log(raycastHitList[0]);
        //     return raycastHitList[0].collider;
        // } else {
        //     Debug.Log(null);
        //     return null;
        // }

        Ray ray = new Ray(myTank.Tower.transform.position, direction); 
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            Debug.Log(hit.collider);
            return hit.collider;
        } else {
            throw new MissingReferenceException();
        }
    }

    private Vector3 bestRotate() {
        Vector3 direction = myTank.Tower.transform.forward;
        Vector3 rotation = myTank.transform.eulerAngles;
        if(getCollider(direction).tag == "obstacle") {
            float y = rotation.y;
            int sign;
            if(Random.value > 0.5f) {
                sign = 1;
            } else {
                sign = -1;
            }
            Debug.Log("rotation.y :" + rotation.y);
            for(int i = 1; getCollider(Quaternion.Euler(0, rotation.y, 0) * direction).tag != "obstacle"; i++) {
                rotation.y += sign * rotation.y + i;
                Debug.Log("rotation.y :" + rotation.y);
                if(i >= 360) {
                    Debug.Log("一周してもダメだった");
                    break;
                }
            }
        }
        return rotation;
    }

}
