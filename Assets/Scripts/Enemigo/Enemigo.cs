using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float speed;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePos;

    //public bool shouldRotate;

    public LayerMask whatIsPlayer;

    private Transform target;
    private Rigidbody2D rb;
    //private Animator animator;
    private Vector2 movement;
    public Vector3 direction;

    private bool isInChaseRange;
    private bool isInAttackRange;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    //    animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
    //    animator.SetBool("isRunning", isInChaseRange);

        isInChaseRange = Physics2D.OverlapCircle(transform.position, chaseRadius, whatIsPlayer);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsPlayer);

        direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        movement = direction;
        //if(shouldRotate)
        //{
    //        animator.SetFloat("X", direction.x);
    //        animator.SetFloat("Y", direction.y);
        //}
    }

    private void FixedUpdate(){
        if(isInChaseRange && !isInAttackRange)
        {
            MoveCharacter(movement);
        } else if(isInAttackRange)
        {
            rb.velocity = Vector2.zero;
        } else 
        {
            goHome();
        }
    }

    private void MoveCharacter(Vector2 dir)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    private void goHome(){
        transform.position = Vector3.MoveTowards(transform.position, homePos.position, (speed / 2) * Time.deltaTime);
    }
}
