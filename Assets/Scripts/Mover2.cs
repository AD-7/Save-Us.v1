using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover2 : MonoBehaviour {


    public float speed;
    // Use this for initialization
    void Start () {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
