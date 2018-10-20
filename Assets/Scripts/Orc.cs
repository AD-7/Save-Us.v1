using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour
{

    Rigidbody2D enemyRigidBody2D;
    public int UnitsToMove;
    public float EnemySpeed;
    public bool _isFacingRight;
    private float _startPos;
    private float _endPos;
    private Transform target;
    public float _rangeToAttack;
    int attack = Animator.StringToHash("attack");
    Animator anim;

    public bool _moveRight = true;
    // Use this for initialization
    void Start()
    {
        enemyRigidBody2D = GetComponent<Rigidbody2D>();
        _startPos = transform.position.x;
        _endPos = _startPos + UnitsToMove;
        _isFacingRight = transform.localScale.x > 0;
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
   
    // Update is called once per frame
    void FixedUpdate()
    {
        if (_moveRight)
        {
            enemyRigidBody2D.AddForce(Vector2.right * EnemySpeed * Time.deltaTime);
            if (!_isFacingRight)
                Flip();
        }

        if (enemyRigidBody2D.position.x >= _endPos)
            _moveRight = false;


        if (!_moveRight)
        {
            enemyRigidBody2D.AddForce(-Vector2.right * EnemySpeed * Time.deltaTime);
            if (_isFacingRight)
                Flip();
        }
        if (enemyRigidBody2D.position.x <= _startPos)
            _moveRight = true;

        if (Mathf.Abs(target.position.x - transform.position.x) < _rangeToAttack &&
            (_endPos > target.position.x && target.position.x > (_startPos)))
        {
            AttackPlayer();
        }
    }
    public void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        _isFacingRight = transform.localScale.x > 0;
    }

    private void AttackPlayer()
    {

        if (target.position.x < transform.position.x && _isFacingRight)
        {
            this.GetComponent<BoxCollider2D>().edgeRadius += 1;
            anim.SetTrigger(attack);
          
            Flip();
            _moveRight = false;
           
        }

        if (target.position.x > transform.position.x && !_isFacingRight)
        {
            this.GetComponent<BoxCollider2D>().edgeRadius += 1;
            anim.SetTrigger(attack);
            Flip();
            _moveRight = true;

          
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Bolt")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.tag == "Player")
        {
       
        }



    }





}
