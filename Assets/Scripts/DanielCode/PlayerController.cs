using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public static bool movementLocked = false;
    public static bool isRolling = false;
    public GameManager gameManager;
    public static PlayerController controller;
    public static bool isGrounded;
    [SerializeField] protected Transform baseTransform;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundMask;
    private float timeOfJump = 0f;
    private float movementDirectionX = 0f;
    public static float movementSpeed = 5f;
    public static float baseSpeed = 5f;
    public static float jumpHeight = 10f;
    public static Rigidbody2D PlayerRb;
    public InputAction move;
    public static bool isDrifting = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        PlayerRb = rb;
        
        controller = this;
        move = InputSystem.actions.FindAction("Move");
    }
    private void OnEnable()
    {
        movementLocked = false;
        timeOfJump = 0f;

    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isGrounded);
        isGrounded = Physics2D.OverlapBox(baseTransform.position, new Vector2(1, 0.3f), 0, groundMask);
        if (isGrounded)
        {
            Debug.Log("grounded");

        }
        if (isDrifting)
        {
            Debug.Log("drifting");
        }
        if (movementLocked)
        {
            Debug.Log("movementLocked");
        }
        // Debug.Log(isGrounded);
        if (isGrounded)
        {
            if (isDrifting)
            {
                isDrifting = false;
                movementLocked = false;
            }
            foreach (AbilityBase a in gameManager.abilitiesPossessed)
            {
                a.CancelOnGrounded();
            }
        }
        /*
        if (!movementLocked)
        {
            rb.linearVelocityX = movementDirectionX * movementSpeed;
        }
        if  (isDrifting)
        {
            rb.AddForceX(movementDirectionX * 2);
        */
        if (rb.linearVelocityX >= movementSpeed * 1.1f)
        {
            movementLocked = true;
            isDrifting = true;
        }
        if (!movementLocked)
        {
            rb.linearVelocityX = movementDirectionX * movementSpeed;
        }
        else
        {
            rb.AddForceX(movementDirectionX * 1.5f);
        }

        // later change to an always drifting solution for high speeds


    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded && Time.time - timeOfJump > 0.05f)
        {
            Debug.Log("started");
            //rb.AddForce(new Vector2(0, jumpHeight));
            rb.linearVelocityY = jumpHeight;
            timeOfJump = Time.time;

        }

    }
    public static void Jump()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // find exactly what context is as an object
        movementDirectionX = context.ReadValue<Vector2>().x;

    }
    public void Dash(float duration, float length)
    {
        movementLocked = true;
        StartCoroutine(DashingStart(duration, length));
    }
    public IEnumerator DashingStart(float duration, float length)
    {
        Vector2 currentPos = gameObject.transform.position;
        Vector2 targetPos = (Vector2) (gameObject.transform.position) + move.ReadValue<Vector2>() * length;
        yield return StartCoroutine(Dashing(duration, length, currentPos, targetPos, Time.time));
        movementLocked = false;
        rb.linearVelocityY = 0;
    }
    public IEnumerator Dashing(float duration, float length, Vector2 currentPos, Vector2 targetPos, float startingTime)
    {
        while (Time.time < startingTime + duration)
        {
            gameObject.transform.position = Vector2.Lerp(currentPos, targetPos, (Time.time - startingTime) / duration);
            yield return null;
        }
        
    }
    public void Roll(float rollForce)
    {
        StartCoroutine(Rolling(rollForce));
        
    }
    public void SlamThenRoll(float rollForce)
    {
        
        StartCoroutine(SlamRoll(rollForce));
    }
    public IEnumerator Rolling(float rollForce)
    {
        while (isRolling && isGrounded)
        {
            if (rb.linearVelocityX > 0)
            {
                rb.AddForceX(rollForce);
            }
            else if (rb.linearVelocityX < 0)
            {
                rb.AddForceX(-rollForce);
            }
            else
            {
                isRolling = false;
            }
            yield return null;
        }
        Leap();

    }
    public IEnumerator SlamRoll(float rollForce)
    {
        yield return StartCoroutine(Slam());
        StartCoroutine(Rolling(rollForce));
    }
    public IEnumerator Slam()
    {
        rb.linearVelocityY = -7f;
        while (!isGrounded)
        {
            yield return null;
        }
        rb.linearVelocityY = 0f;
    }
    public static void Leap()
    {
        PlayerRb.AddForceY(400);
        isDrifting = true;
    }

    // make a launch function
    
}
