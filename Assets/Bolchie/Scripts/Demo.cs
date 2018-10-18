using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Basic Player Script//
//controls: 
//A, D, Left, Right to move
//Left Alt to attack
//Space to jump
//Z is to see dead animation

public class Demo : MonoBehaviour {

	//variable for how fast player runs//
	private float speed = 5f;

	private bool facingRight = true;
	private Animator anim;
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;

    private GameObject ActualGun;
    public GameObject gun1;
    public float fireRateGun1 ;
    public float fireRateGun2;
    public GameObject pocisk;
    private float nextFire ;


    //variable for how high player jumps//
    [SerializeField]
	private float jumpForce = 300f;

	public Rigidbody2D rb { get; set; }

	bool dead = false;
	bool attack = false;

	void Start () {
        gun1.SetActive(false);
		GetComponent<Rigidbody2D> ().freezeRotation = true;
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponentInChildren<Animator> ();
        ActualGun = gun1;
	}

	void Update()
	{
		HandleInput ();
	}

	//movement//
	void FixedUpdate ()
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);

		float horizontal = Input.GetAxis("Horizontal");
		if (!dead && !attack)
		{
			anim.SetFloat ("vSpeed", rb.velocity.y);
			anim.SetFloat ("Speed", Mathf.Abs (horizontal));
			rb.velocity = new Vector2 (horizontal * speed, rb.velocity.y);
		}
		if (horizontal > 0 && !facingRight && !dead && !attack) {
			Flip (horizontal);
		}

		else if (horizontal < 0 && facingRight && !dead && !attack){
			Flip (horizontal);
		}
	}

	//attacking and jumping//
	private void HandleInput()
	{
		if (Input.GetKeyDown (KeyCode.LeftAlt) && !dead && Time.time > nextFire) 
		{
             
			attack = true;
			anim.SetBool ("Attack", true);
			anim.SetFloat ("Speed", 0);
            gun1.SetActive(true);

            nextFire = Time.time + fireRateGun1;
            Instantiate(pocisk, ActualGun.GetComponent<Transform>().position, ActualGun.GetComponent<Transform>().rotation);

        }
		if (Input.GetKeyUp(KeyCode.LeftAlt))
			{
			attack = false;
			anim.SetBool ("Attack", false);
            gun1.SetActive(false);
        }

		if (grounded && Input.GetKeyDown(KeyCode.Space) && !dead)
		{
			anim.SetBool ("Ground", false);
			rb.AddForce (new Vector2 (0,jumpForce));
		}

		//dead animation for testing//
		if (Input.GetKeyDown (KeyCode.Z)) 
		{
			if (!dead) {
				anim.SetBool ("Dead", true);
				anim.SetFloat ("Speed", 0);
				dead = true;
			} else {
					anim.SetBool ("Dead", false);
					dead = false;
				}
		}
	}
		
	private void Flip (float horizontal)
	{
			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
	}
}
