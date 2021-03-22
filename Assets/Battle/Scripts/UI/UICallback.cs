using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UICallback : MonoBehaviour
{
    public GameObject masterObj;
    private GameMaster_Battle master;
    private VariableJoystick joystick;
    private GameObject joystickObj;
    public Toggle Toggle;

    void Awake() {
        masterObj = GameObject.Find("GameMaster");
        master = masterObj.GetComponent<GameMaster_Battle>();
    }

    void Start() {
        joystickObj = master.joystick;
        joystick = joystickObj.GetComponent<VariableJoystick>();
        Debug.Log(Data.joystick + "にjoystickを設定");
        joystickObj.SetActive(Data.joystick);
        this.Toggle.isOn = Data.joystick;
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
        Data.joystick = master.joystick.activeSelf;
        SceneManager.LoadScene("Stage1");
    }

}
