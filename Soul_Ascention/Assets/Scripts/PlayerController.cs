using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    Animator animator;
    string animationState = "AnimationState";
    enum ChrStates
    {
        walkright = 4,
        walkleft = 3,
        walkback = 2,
        walkfront = 1,
        idle = 0

    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;*/
        this.UpdateState();
    }

    private void UpdateState (){
        if(movement.x > 0){
            animator.SetInteger( animationState, (int)ChrStates.walkright);
        } else if (movement.x < 0) {
            animator.SetInteger( animationState, (int)ChrStates.walkleft);
        }else if (movement.y > 0){
            animator.SetInteger( animationState, (int)ChrStates.walkback);
        }else if(movement.y < 0){
            animator.SetInteger( animationState, (int)ChrStates.walkfront);
        }else{
            animator.SetInteger( animationState, (int)ChrStates.idle);
        }
    }

    private void FixedUpdate()
    {
        //rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        this.MoveCharacter();
    }

    private void MoveCharacter(){
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        movement.Normalize();
        rb.velocity = movement * speed;
    }
}
