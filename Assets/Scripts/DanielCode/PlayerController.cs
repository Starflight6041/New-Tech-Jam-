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
    public bool IsGrounded
    {
        get => isGrounded;
        set => isGrounded = value;
    }
    [SerializeField] protected Transform baseTransform;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask deathMask;
    [SerializeField] private LayerMask winMask;
    [SerializeField] private LayerMask loseMask;
    private float timeOfJump = 0f;
    private float movementDirectionX = 0f;
    public static float movementSpeed = 5f;
    public static float baseSpeed = 5f;
    public static float jumpHeight = 10f;
    public static Rigidbody2D PlayerRb;
    public InputAction move;
    public static bool isDrifting = false;
    public static float groundedLenience = 0f;

    public GameObject grid_master;

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
        isRolling = false;
        isDrifting = false;
        timeOfJump = 0f;
        groundedLenience = 0f;

    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isGrounded);
        isGrounded = Physics2D.OverlapBox(baseTransform.position, new Vector2(1, 0.4f), 0, groundMask) && Time.time - groundedLenience > 0.4f;
        if (Physics2D.OverlapBox(baseTransform.position, new Vector2(1, 0.4f), 0, deathMask))
        {
            resetPosVelocity();
        }
        if (Physics2D.OverlapBox(baseTransform.position, new Vector2(1, 0.4f), 0, winMask))
        {
            resetPosVelocity();
            win();
        }
        if (Physics2D.OverlapBox(baseTransform.position, new Vector2(1, 0.4f), 0, loseMask))
        {
            resetPosVelocity();
            lose();
        }
        if (isGrounded)
        {
            //Debug.Log("grounded");

        }
        if (isDrifting)
        {
            //Debug.Log("drifting");
        }
        if (movementLocked)
        {
            //Debug.Log("movementLocked");
        }
        // Debug.Log(isGrounded);
        if (isGrounded && Time.time - groundedLenience > 0.1f)
        {
            if (isDrifting)
            {
                isDrifting = false;
                
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
        
        if (Mathf.Abs(rb.linearVelocityX) >= movementSpeed * 1.1f && (!isGrounded || isRolling))
        {
            
            isDrifting = true;
        }
        if (!isDrifting && !isRolling)
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
        if (context.started && isGrounded && Time.time - timeOfJump > 0.05f && !isRolling)
        {
            Debug.Log("started");
            //rb.AddForce(new Vector2(0, jumpHeight));
            rb.linearVelocityY = jumpHeight;
            timeOfJump = Time.time;

        }

    }

    void resetPosVelocity()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        rb.linearVelocityX = 0f;
        rb.linearVelocityY = 0f;
    }

    public void win()
    {

    }

    public void lose()
    {

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
        //movementLocked = true;
        if(movementDirectionX > 0)
        {
            SimplePlayer.animator.SetBool("DashRight", true);
        }
        else
        {
            SimplePlayer.animator.SetBool("DashLeft", true);
        }
        StartCoroutine(DashingStart(duration, length));
    }
    public IEnumerator DashingStart(float duration, float length)
    {
        Vector2 currentPos = gameObject.transform.position;
        Vector2 targetPos = (Vector2) (gameObject.transform.position) + move.ReadValue<Vector2>() * length;
        yield return StartCoroutine(Dashing(duration, length, currentPos, targetPos, Time.time));
        //movementLocked = false;
        
    }
    public IEnumerator Dashing(float duration, float length, Vector2 currentPos, Vector2 targetPos, float startingTime)
    {
        while (Time.time < startingTime + duration)
        {
            gameObject.transform.position = Vector2.Lerp(currentPos, targetPos, (Time.time - startingTime) / duration);
            yield return null;
        }
        yield return null;
        rb.linearVelocity = (targetPos - currentPos) / (targetPos - currentPos).magnitude * rb.linearVelocity.magnitude;
        GameManager.isAbility = false;


    }
    public void Roll(float rollForce)
    {
        if(movementDirectionX > 0)
        {
            SimplePlayer.animator.SetBool("RollRight", true);
        }
        else if (movementDirectionX < 0)
        {
            SimplePlayer.animator.SetBool("RollLeft", true);
        }
        StartCoroutine(Rolling(rollForce));
        
    }
    public void SlamThenRoll(float rollForce)
    {
        if(movementDirectionX > 0)
        {
            SimplePlayer.animator.SetBool("RollRight", true);
        }
        else if (movementDirectionX < 0)
        {
            SimplePlayer.animator.SetBool("RollLeft", true);
        }
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
            foreach (AbilityBase a in gameManager.abilitiesPossessed)
            {
                a.CancelOnGrounded();
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
        GameManager.isAbility = false;
        isDrifting = true;
        movementLocked = true;
        
        
    }

    // make a launch function
    
}
