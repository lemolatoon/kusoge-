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

    // Start is called before the first frame update
    void Start()
    {
        masterObj = GameObject.Find("GameMaster");
        master = masterObj.GetComponent<GameMaster_Battle>();
        master.tankComputer = this.gameObject;
        player = master.smallTank.GetComponent<TankController_Battle>();
        myTank = this.GetComponent<TankController_Battle>();
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
        float time = Random.Range(3.0f, 9.0f);
        Debug.Log("randomwalk開始" + time);
        float now = Time.time;
        float passedTime = 0;
        float euler = Random.Range(0, 360);
        myTank.transform.Rotate(new Vector3(0, euler, 0));
        while(passedTime < time) {
            passedTime += Time.fixedDeltaTime;
            myTank.forwardMove(Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
            if(tankCollision.StayWall) {
                break;
            }
        }
        walking = false;
        Debug.Log("randomwalk終了");
    }

    private IEnumerator Attacking() {
        StartCoroutine(RandomWalk());
        while(true && !master.gameEnd) {
            if(!walking && UnityEngine.Random.value < 0.0f) { //確率でランダムウォーク 0,7f
                walking = true;
                StartCoroutine(RandomWalk());
            } 
            
            if(Random.value < 0.9f) { //確率で射撃 0.5f
                Debug.Log("shooot!!");
                myTank.shoot(player.transform.position);
            }

            yield return new WaitForSeconds(1.0f);
        }
    }

}
