using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    private int count = 0; 
    public float power = 25.0f;

    private GameObject masterObj;
    private GameMaster_Battle master;

    private GameObject shotByObj;
    private TankController_Battle shotBy;

    void Awake() {
        masterObj = GameObject.Find("GameMaster");
        master = masterObj.GetComponent<GameMaster_Battle>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void init(GameObject shotByObj) {
        this.shotByObj = shotByObj;
        shotBy = shotByObj.GetComponent<TankController_Battle>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void shoot(Vector3 shotPos) {
        var p = power * this.transform.forward;
        Debug.Log(power);
        this.GetComponent<Rigidbody>().AddForce(p);
    } 

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "wall") {
            if(count == 1) {
                Destroy(gameObject);
            } else {
                count++;
            }
        }
    }

    void OnDestroy() {
        shotBy.bulletCount--;
    }

}
