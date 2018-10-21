using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {
    public GameObject target;


	// Use this for initialization
	void Start () {
    
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {

            target.GetComponent<Demo>().GameOver();
            other.gameObject.SetActive(false);

        }
       
    }
}
