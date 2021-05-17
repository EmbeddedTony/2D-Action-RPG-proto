using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState
{
    walk,
    attack,
    interact
}

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector2 movement;
    private Animator animator;
    public bl_Joystick joyStick;
    



    // Movement
    float moveHorizontal;
    float moveVertical;
    

    // Start is called before the first frame update
    void Start()
    {
        speed = 1f;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Vector2.zero;
        moveHorizontal = joyStick.Horizontal;
        moveVertical = joyStick.Vertical;
        movement = new Vector2(moveHorizontal, moveVertical);
        if (Input.GetButtonDown("Attack") && !animator.GetBool("attacking") ) 
        {
            StartCoroutine(AttackCo());
        }

        animationAndMove();

    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
       // currentState = PlayerState.walk;
    }



    private void FixedUpdate()
    {
        
            transform.Translate(movement * Time.deltaTime * speed);
         
    }



    void animationAndMove()
    {
        if (movement != Vector2.zero)
        {
            animator.SetFloat("moveX", movement.x);
            animator.SetFloat("moveY", movement.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }


}
