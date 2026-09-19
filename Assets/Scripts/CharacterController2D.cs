using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController2D : MonoBehaviour{
    public Rigidbody2D rb;
    public float speed = 0.5f;
    public InputAction playerControls;
    public int jumps = 1;
    public float jumpSpeed = 0.05f;

    Vector2 moveDirection = Vector2.zero;

    private void OnEnable(){
        playerControls.Enable();
    }

    private void OnDisable(){
        playerControls.Disable();
    }

    void Update(){
        moveDirection = playerControls.ReadValue<Vector2>();
        if(IsGrounded()){
            jumps = 1;
        }
    }

    private void FixedUpdate(){
        if(moveDirection.y > 0){
            Jump();
        }
        rb.linearVelocity = new Vector2(moveDirection.x * speed, rb.linearVelocity.y - 40f * Time.fixedDeltaTime);
        if(rb.transform.position.y < -10f){
            //put game over stuff here
            rb.transform.position = new Vector2(0, 0);
        }
    }

    private void Jump(){
        if (jumps > 0 && IsGrounded()){
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
            jumps--;
        }
    }

    bool IsGrounded(){
        float GroundedDistance = 0.1f;
        if (rb.linearVelocity.y == 0){
            RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, GroundedDistance);
            return hit.collider != null;
        }
        return false;
    }
}