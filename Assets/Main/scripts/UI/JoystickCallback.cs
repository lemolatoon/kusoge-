using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickCallback : MonoBehaviour
{

    public GameObject master;
    private TankController tankController;

    // Start is called before the first frame update
    void Start()
    {
        tankController = master.GetComponent<GameMaster>().smallTank.GetComponent<TankController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
