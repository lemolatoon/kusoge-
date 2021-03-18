using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject masterObj;
    private GameMaster_Battle master;
    private TankController_Battle player;
    private TankController_Battle myTank;

    // Start is called before the first frame update
    void Start()
    {
        masterObj = GameObject.Find("GameMaster");
        master = masterObj.GetComponent<GameMaster_Battle>();
        master.tankComputer = this.gameObject;
        player = master.smallTank.GetComponent<TankController_Battle>();
        myTank = this.GetComponent<TankController_Battle>();
        StartCoroutine(Attacking());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator RandomWalk() {
        float time = Random.Range(3.0f, 5.0f);
        float now = Time.time;
        float passedTime = 0;
        while(passedTime < time) {
            float t = Time.time;
            float deltaTime = t - now;
            now = t;
            myTank.Move(Random.Range(0, 360), deltaTime);
            yield return new WaitForFixedUpdate();
        }

    }

    private IEnumerator Attacking() {
        while(false) {
            if(UnityEngine.Random.value < 0.3f) { //確率でランダムウォーク
                StartCoroutine(RandomWalk());
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

}
