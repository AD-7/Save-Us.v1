using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Basic Player Script//
//controls: 
//A, D, Left, Right to move
//Left Alt to attack
//Space to jump
//Z is to see dead animation

public class Demo : MonoBehaviour
{

    private float speed = 10f;

    private bool facingRight = true;
    private Animator anim;
    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;


    public bool _isFacingRight;
    private GameObject ActualGun;
    public GameObject gun1;
    public GameObject gun2;
    public float fireRateGun1;
    public float fireRateGun2;
    public GameObject pocisk,pocisk2;
    private float nextFire;


    private int points;
    public Text countText;
    public Text winText;

    public float timeLeft;
    public Text timeText;

    //variable for how high player jumps//
    [SerializeField]
    private float jumpForce = 300f;

    public Rigidbody2D rb { get; set; }

    bool dead = false;
    bool attack = false;

    void Start()
    {
        gun1.SetActive(false);
        gun2.SetActive(false);
        ActualGun = gun1;
        ActualGun.SetActive(false);
        _isFacingRight = transform.localScale.x > 0;
        GetComponent<Rigidbody2D>().freezeRotation = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        this.points = 0;
        SetCountText();
        winText.text = "";
        timeText.text = "";

        timeLeft = 120;

    }

    void Update()
    {
        HandleInput();

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            //GameOver();
        }
        SetCountText();

    }

    //movement//
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);

        float horizontal = Input.GetAxis("Horizontal");
        if (!dead && !attack)
        {
            anim.SetFloat("vSpeed", rb.velocity.y);
            anim.SetFloat("Speed", Mathf.Abs(horizontal));
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            _isFacingRight = transform.localScale.x > 0;
        }
        if (horizontal > 0 && !facingRight && !dead && !attack)
        {
            Flip(horizontal);
        }

        else if (horizontal < 0 && facingRight && !dead && !attack)
        {
            Flip(horizontal);
        }
    }

    //attacking and jumping//
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) && !dead && Time.time > nextFire)
        {

            attack = true;
            anim.SetBool("Attack", true);
            anim.SetFloat("Speed", 0);
            ActualGun.SetActive(true);

            nextFire = Time.time + fireRateGun1;
            if (_isFacingRight) 
            Instantiate(pocisk, ActualGun.GetComponent<Transform>().position, ActualGun.GetComponent<Transform>().rotation);
            if (!_isFacingRight)
            {
            Instantiate(pocisk2, ActualGun.GetComponent<Transform>().position, ActualGun.GetComponent<Transform>().rotation);
            }
            
        }
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            attack = false;
            anim.SetBool("Attack", false);
            ActualGun.SetActive(false);
        }

        if (grounded && Input.GetKeyDown(KeyCode.Space) && !dead)
        {
            anim.SetBool("Ground", false);
            rb.AddForce(new Vector2(0, jumpForce));
        }

        //dead animation for testing//
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!dead)
            {
                anim.SetBool("Dead", true);
                anim.SetFloat("Speed", 0);
                dead = true;
            }
            else
            {
                anim.SetBool("Dead", false);
                dead = false;
            }
        }
    }

    private void Flip(float horizontal)
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "GunItem")
        {
            ActualGun = gun2;
            Destroy(collision.gameObject);
            fireRateGun1 = fireRateGun2;
        }

        if (collision.tag == "PickUp")
        {
            Destroy(collision.gameObject);
            points++;
            SetCountText();
        }
    }


    void SetCountText()
    {
        countText.text = "Count: " + points.ToString() + "/5";
        timeText.text = "Time left : " + timeLeft.ToString();

        if (points >= 1)
        {
            winText.text = "You Win!";
        }

        if (timeLeft <= 0)
        {
            //GameOver();
            winText.text = "You Loose :( ";

        }
    }


}
