using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster_Battle : MonoBehaviour
{

    public GameObject smallTank;
    public GameObject tankComputer;

    public GameObject joystick;
    public bool gameEnd = false;
    private GameObject[] objects;

    // Start is called before the first frame update
    void Start()
    {
        objects = new GameObject[3] {smallTank, tankComputer, joystick};
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
