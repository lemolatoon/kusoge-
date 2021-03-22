using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    private int count = 0; 
    public float power = 25.0f;

    // Start is called before the first frame update
    void Start()
    {
        // rb = this.GetComponent<Rigidbody>();
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

}
