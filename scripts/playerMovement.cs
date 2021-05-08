using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    Vector2 movement;
    public float velocity=5f;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        if (movement.x < 0) anim.SetBool("is_left", true);
        else if (movement.x > 0) anim.SetBool("is_left", false);
        if (Input.GetKeyDown(KeyCode.LeftControl))  anim.SetBool("is_rolling", true);
        else   anim.SetBool("is_rolling", false);
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * velocity * Time.fixedDeltaTime);
    }
}
