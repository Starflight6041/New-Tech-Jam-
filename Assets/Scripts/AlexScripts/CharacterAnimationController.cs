using UnityEngine;
using System.Collections;

public class SimplePlayer : MonoBehaviour {

    Animator animator;
    PlayerController characterController;
    
    void Start () {
        animator = GetComponent<Animator>();
        characterController = GetComponent<PlayerController>();
    }

    void Update () {
        bool still = characterController.rb.linearVelocity.x==0;
        bool l = characterController.rb.linearVelocity.x<0;
        bool r = characterController.rb.linearVelocity.x>0;
        bool fall = characterController.rb.linearVelocity.y<-1;
        bool jump = characterController.rb.linearVelocity.y>1;
        bool stand = characterController.IsGrounded && still;
        bool walk = characterController.IsGrounded && !still;
        bool walkLeft = walk && l;
        bool walkRight = walk && r;
        bool fallLeft = fall && l;
        bool fallRight = fall && r;
        bool jumpLeft = jump && l;
        bool jumpRight = jump && r;

        animator.SetBool("JumpLeft", jumpLeft);
        animator.SetBool("JumpRight", jumpRight);
        animator.SetBool("FallLeft", fallLeft);
        animator.SetBool("FallRight", fallRight);
        animator.SetBool("WalkRight", walkRight);
        animator.SetBool("WalkLeft", walkLeft);
        animator.SetBool("Jumping", jump && !jumpLeft && !jumpRight);
        animator.SetBool("Falling", fall && !fallLeft && !fallRight);
        animator.SetBool("Standing", stand);

    }

}