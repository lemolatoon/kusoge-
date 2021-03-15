using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SliderCallback : MonoBehaviour
{

    Slider speedSlider;
    public GameObject master;

    // Start is called before the first frame update
    void Start()
    {
        speedSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeSpeed() {
        // master.GetComponent<GameMaster>().smallTank.GetComponent<TankController>().forwardSpeed = (float)Math.Pow(10, speedSlider.value);
        master.GetComponent<GameMaster>().smallTank.GetComponent<TankController>().forwardSpeed = speedSlider.value * speedSlider.value;
    }

}
