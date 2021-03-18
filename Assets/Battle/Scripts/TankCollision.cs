using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankCollision : MonoBehaviour
{   
    private GameObject masterObj;
    private GameMaster_Battle master;
    private Player player;
    private TankController_Battle tankController;
    private Exploder exploder;
    public float span = 0.1f;

    void Start() {
        masterObj = GameObject.Find("GameMaster");
        master = masterObj.GetComponent<GameMaster_Battle>();
        player = master.smallTank.GetComponent<Player>();
        tankController = master.smallTank.GetComponent<TankController_Battle>();
        exploder = gameObject.GetComponent<Exploder>();
        InvokeRepeating("shoot", span, span);
    }

    void Update() {
    }
    
    void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "bullet") {
            exploder.startExplode();
            exploder.enabled = true;
            Debug.Log("弾にあたったねgameOver");
            Destroy(gameObject);
        }
    }

    private void shoot() {
        tankController.shoot(player.CurrentMousePos);
    }

}
