using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Author: Saša
 * Description: Handles player movement. Note: facingDirection is for the hitbox,
 * from player_melee so that it faces the same direction, the player is facing.
 */
public class movement : MonoBehaviour
{
    public float MOVE_SPEED = 11f;
    public float dashAmount = 2f;

    private Animator animator;
    private Rigidbody2D rigidbody2D;
    public Vector3 moveDirection;
    private bool isDashButtonDown;
    public Vector3 facingDirection;
    public float stunnedTime;
    public bool stunned;
    public bool isDashing;
    public bool cooldown;

    private void Awake()
    {
        facingDirection = new Vector3(0,0);
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
       HandleMovement();
       HandleDash();
    }

    private void FixedUpdate() // Hier geschieht alles mit dem Rigidbody
    {
        if(stunned)
        {
            return;
        }

        rigidbody2D.velocity = moveDirection * MOVE_SPEED;

        HandleAnimation();

        if (isDashButtonDown) // Wenn gedasht wurde
        {
            animator.SetBool("isDashing", true);
            soundEngine.play_sound(soundEngine.sound_type.player_melee);
            Vector3 dashPosition = transform.position + moveDirection * dashAmount;
            rigidbody2D.AddForce(dashPosition * dashAmount);
            //rigidbody2D.MovePosition(dashPosition); // dashAmount = 5
            isDashButtonDown = false;
            StartCoroutine(waitfordash());
            cooldown = true;
            StartCoroutine(dashCoolDown());
        }
    }

    private void HandleAnimation()
    {
        // run animation:
        animator.SetFloat("horizontal", rigidbody2D.velocity.x);
        animator.SetFloat("vertical", rigidbody2D.velocity.y);

        // facing the correct direction when idling:
        if(Input.GetAxisRaw("Horizontal") == 1 || 
           Input.GetAxisRaw("Horizontal") == -1 || 
           Input.GetAxisRaw("Vertical") == 1 || 
           Input.GetAxisRaw("Vertical") == -1)
        {
            animator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
        }
    }

    private void HandleMovement()
    {
        float x = 0f;
        float y = 0f;
        
        if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.UpArrow))
        {
            facingDirection = new Vector3(0, 2); // hitbox can now also face the direction of the player
            y = +1f;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            facingDirection = new Vector3(0,- 2);
            y = -1f;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            facingDirection = new Vector3( - 2, 0);
            x = -1f;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            facingDirection = new Vector3( 2,0);
            x = +1f;
        }

        moveDirection = new Vector3(x, y).normalized; // Bewegungsrichtung erzeugen
    }

    private void HandleDash()
    {
        if (!cooldown)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isDashButtonDown = true;
                isDashing = true;
                StartCoroutine(invulnerability());
            }
        }
    }

    public void knockback(float knockbackDuration, float knockbackPower, Transform obj)
    {
        if(isDashing) // prevents knockback while dashing
        {
            return;
        }

        stunnedTime = knockbackDuration;
        StartCoroutine(wait());
        rigidbody2D.velocity = Vector2.zero;
        Vector2 direction = (obj.transform.position - this.transform.position).normalized;
        // rigidbody2D.AddForce(-direction * knockbackPower);
        rigidbody2D.velocity = Vector3.Lerp(rigidbody2D.velocity, -direction * knockbackPower, 9000.0f * Time.deltaTime);
    }

    IEnumerator wait()
    {
        stunned = true;
        yield return new WaitForSeconds(stunnedTime);
        stunned = false;
    }

    IEnumerator waitfordash()
    {
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("isDashing", false);
    }

    IEnumerator dashCoolDown()
    {
        yield return new WaitForSeconds(3.0f);
        cooldown = false;
    }

    IEnumerator invulnerability()
    {
        yield return new WaitForSeconds(0.95f);
        isDashing = false;
    }
}
