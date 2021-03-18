using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankCollision : MonoBehaviour
{   
    public GameObject masterObj;
    private GameMaster_Battle master;
    private Player player;
    private TankController_Battle tankController;
    public float span = 0.1f;

    void Start() {
        master = masterObj.GetComponent<GameMaster_Battle>();
        player = master.smallTank.GetComponent<Player>();
        tankController = master.smallTank.GetComponent<TankController_Battle>();
        InvokeRepeating("shoot", span, span);
    }

    void Update() {
    }
    
    void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "bullet") {
            Debug.Log("弾にあたったねgameOver");
        }
    }

    private void shoot() {
        tankController.shoot(player.CurrentMousePos);
    }

}
