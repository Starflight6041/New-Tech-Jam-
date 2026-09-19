using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public static bool movementLocked = false;
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
    private float jumpHeight = 10f;
    public static Rigidbody2D PlayerRb;
    public InputAction move;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        PlayerRb = rb;
        controller = this;
        move = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapBox(baseTransform.position, new Vector2(1, 0.2f), 0, groundMask);
        if (isGrounded)
        {
            foreach (AbilityBase a in gameManager.abilitiesPossessed)
            {
                a.CancelOnGrounded();
            }
        }
        if (!movementLocked)
        {
            rb.linearVelocityX = movementDirectionX * movementSpeed;
        }
        
        
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
}
