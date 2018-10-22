using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public Demo player;


	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Demo>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.GameOver();
            Destroy(player);
        }
    }
    

}
