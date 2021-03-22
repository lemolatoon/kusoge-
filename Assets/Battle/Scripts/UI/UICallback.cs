using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UICallback : MonoBehaviour
{
    public GameObject masterObj;
    private GameMaster_Battle master;
    private VariableJoystick joystick;
    private GameObject joystickObj;

    void Awake() {
        masterObj = GameObject.Find("GameMaster");
        master = masterObj.GetComponent<GameMaster_Battle>();
    }

    void Start() {
        joystickObj = master.joystick;
        joystick = joystickObj.GetComponent<VariableJoystick>();
        joystickObj.SetActive(false);
    }

    public void OnValueChangedToggle() {
        if(joystickObj.activeSelf) {
            Debug.Log("falseにした");
            joystickObj.SetActive(false);
            
        } else {
            joystickObj.SetActive(true);
        }
    }

    public void OnPressedButtonReload() {
        SceneManager.LoadScene("Stage1");
    }

}
