using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pablo_Controller : MonoBehaviour
{
    //Movement
    Rigidbody2D rb;
    Vector2 movementInput;
    [SerializeField] private float movespeed = 2f;
    bool canMove = true;
    bool isMoving = true;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        if(canMove==true&&movementInput !=Vector2.zero)
        {
            rb.MovePosition(rb.position + movementInput * movespeed * Time.fixedDeltaTime);
            isMoving = true;
            Debug.Log(isMoving);
        }

        else
        {
            isMoving = false;
        }
    }
    
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
