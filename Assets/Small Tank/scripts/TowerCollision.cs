using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCollision : MonoBehaviour
{
    private float initialTime;
    public float gameOverDeltaTime = 5.0f;

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Terrain") {
            initialTime = Time.time;
            Debug.Log("衝突した。");
        }
        Debug.Log("触れてはいる");
    }

    private void OnCollisionStay(Collision other) {
        if(Time.time - initialTime > gameOverDeltaTime) { //一定時間以上Terrainに触れていたら
            Debug.Log(gameOverDeltaTime + "以上Terrainに触れていました。");
        }
    }

    private void OnCollisionExit(Collision other) {
        if(other.gameObject.tag == "Terrain") {
            initialTime = 0;
            Debug.Log("衝突終了。");
        }
    }
    
}
