using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankRotationWatcher : MonoBehaviour
{

    private Vector3 rotation;
    public float gameOverTime = 5.0f;
    private float initialTime = 0;
    private bool isTurned = false;

    // Start is called before the first frame update
    void Start()
    {
        //回転を四元数で取得してから、x,y,zへ変換
        rotation = this.transform.localRotation.eulerAngles;

    }

    // Update is called once per frame
    void Update()
    {
        rotation = this.transform.localRotation.eulerAngles;
        float x = rotation.x % 360;
        float z = rotation.z % 360;
        if((x >= 50 && x <=310) || (z >= 50 && z <= 310)) {
            if(!isTurned) { //ひっくり返りの最初ならば
                Debug.Log("ひっくり返ってるで");
                isTurned = true;
                initialTime = Time.time;
            } else if(Time.time - initialTime > gameOverTime) { //規定の時間以上ひっくり返っていたら
                Debug.Log("GameOver");
            }
        } else {
            isTurned = false;
        }
    }
}
