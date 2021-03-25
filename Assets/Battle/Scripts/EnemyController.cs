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
        player = GameObject.FindWithTag("player").GetComponent<TankController_Battle>();
    }
    void Start()
    {
        //GameMasterから値を受け取る処理
        // player = master.smallTank.GetComponent<TankController_Battle>();
        Debug.Log("player取得:" + player);
        myTank = GetComponent<TankController_Battle>();
        Debug.Log(myTank + "---mytank");
        tankCollision = this.GetComponent<TankCollision>();

        StartCoroutine(Attacking());
    }

    // Update is called once per frame
    void Update()
    {
        myTank.Tower.transform.LookAt(player.transform.position);
        Debug.Log(player.Tower.transform.position + "の方を向く");

        if(!walking && serchTag(myTank.gameObject, "bullet", getCollider(myTank.Tower.transform.forward).distance) != null) {
            StartCoroutine(RandomWalk());
        }
    }

    private IEnumerator RandomWalk() {
        walking = true;
        float time = Random.Range(3.0f, 5.0f);
        Debug.Log("randomwalk開始" + time);
        float now = Time.time;
        float passedTime = 0;
        myTank.transform.LookAt(myTank.transform.position + bestDirection());
        // myTank.transform.rotation = Quaternion.Euler(bestRotate());
        myTank.forwardMove(Time.fixedDeltaTime);
        for(int i = 0; passedTime < time; i++) {
            passedTime += Time.fixedDeltaTime;
            myTank.forwardMove(Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
            if((i > 10 && tankCollision.StayWall) || walking == false) {
                break;
            }
        }
        walking = false;
        Debug.Log("randomwalk終了");
    }

    private IEnumerator Attacking() {
        StartCoroutine(RandomWalk());
        while(true) {
            string tag = getCollider(myTank.Tower.transform.forward).collider.tag;
            if(!walking && UnityEngine.Random.value < 0.7f) { //確率でランダムウォーク 0,7f
                // walking = true;
                // StartCoroutine(RandomWalk());
            } else if(Random.value < 0.5f && tag == "player" && player != null) { //確率で射撃 0.5f            
                Debug.Log("shooot!!");
                myTank.shoot(player.transform.position);
            }

            yield return new WaitForSeconds(1.0f);
        }
    }

    private RaycastHit getCollider(Vector3 direction) {

        Ray ray = new Ray(myTank.Tower.transform.position, direction);  //標準ではmyTank.Tower.transform.forward
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            //Debug.Log(hit.collider);
            //Debug.Log(hit.distance);
            return hit;
        } else {
            throw new MissingReferenceException("前になにもない！！");
        }
    }

    private Vector3 bestDirection() {
        Vector3 rotation = new Vector3(0, Random.Range(0, 360), 0);
        Transform tf = myTank.transform;
        tf.Rotate(rotation);
        Vector3 direction = tf.position - myTank.transform.position;

        if(isBulletNear(10.0f, ref direction)) {
            Debug.Log("bulletを検出");
        }

        return direction;
    }

    private GameObject serchTag(GameObject nowObj, string tagName, float maxDistance = Mathf.Infinity) {
        float tempDistance = 0;
        float nearDistance = 0;
        GameObject targetObj = null;

        foreach(GameObject obj in GameObject.FindGameObjectsWithTag(tagName)) {
            tempDistance = Vector3.Distance(nowObj.transform.position, obj.transform.position);

            if((nearDistance == 0 || tempDistance < nearDistance) && tempDistance <= maxDistance) { //一つ目またはより近いオブジェクトならば
                nearDistance = tempDistance;
                targetObj = obj;
            }
        }

        return targetObj;
    }

    private bool isBulletNear(float r, ref Vector3 direction) {
        //GameObject bull = serchTag(myTank.gameObject, "bullet", getCollider(myTank.Tower.transform.forward).distance);
        GameObject bull = serchTag(myTank.gameObject, "bullet", 0.001f);
        if(bull == null) {
            return false;
        }
        Vector3 bullet = bull.GetComponent<Rigidbody>().velocity;
        // Vector3 tank = myTank.Tower.transform.position;
        Vector3 yAxis = new Vector3(0, 1, 0);

        direction = (getSign() * Vector3.Cross(bullet, yAxis)).normalized;

        
        //StartCoroutine(RandomWalk());
        return true;
    }

    private bool bulletNear(float r, ref Vector3 direction) { //非推奨
        for(int i = 0; i >= 360; i++) {
            RaycastHit  raycasthit = getCollider(myTank.transform.forward);
            if(raycasthit.distance < r && raycasthit.collider.tag == "bullet") {
            Vector3 tank = myTank.Tower.transform.position;
            Vector3 bullet = raycasthit.point; //衝突した場所
            Vector3 yAxis = new Vector3(0, 1, 0);

            direction = (getSign() * Vector3.Cross(bullet - tank, yAxis)).normalized;

            
            StartCoroutine(RandomWalk());
            return true;
            }

            myTank.transform.Rotate(0, 1, 0);
        }
        return false;
    }

    private Vector3 bestRotate() { //保留
        Vector3 direction = myTank.Tower.transform.forward;
        Vector3 rotation = myTank.transform.eulerAngles;
        if(getCollider(direction).collider.tag == "obstacle") {
            int sign;
            if(Random.value > 0.5f) {
                sign = 1;
            } else {
                sign = -1;
            }
            Debug.Log("rotation.y :" + rotation.y);
            for(int i = 1; getCollider(myTank.transform.forward).collider.tag != "obstacle"; i++) {
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

    private int getSign() {
        int sign;
        if(Random.value > 0.5f) {
            sign = 1;
        } else {
            sign = -1;
        }
        return sign;
    }

}
