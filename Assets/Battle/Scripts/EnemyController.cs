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
            if(!walking && UnityEngine.Random.value < 0.7f) { //確率でランダムウォーク 0,7f
                walking = true;
                StartCoroutine(RandomWalk());
            } else if(Random.value < 0.5f && player != null) { //確率で射撃 0.5f            
                Debug.Log("shooot!!");
                myTank.shoot(player.transform.position);
            }

            yield return new WaitForSeconds(1.0f);
        }
    }

}
