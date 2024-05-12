using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Week_2_PlayerController : MonoBehaviour
{
    public float speed = 4;
    public float jump = 558;   
    public bool grounded = false;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public bool lookingRight = true;

    private Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.15F, whatIsGround);

        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            grounded = false;
            rb2d.velocity = new Vector2(rb2d.velocity.x, jump);
        }
    }


    void FixedUpdate()
    {
        float hor = Input.GetAxis("Horizontal");        
        
        rb2d.velocity = new Vector2(hor * speed, rb2d.velocity.y);       

        if ((hor > 0 && !lookingRight) || (hor < 0 && lookingRight))
            Flip();
    }

    public void Flip()
    {
        lookingRight = !lookingRight;
        Vector3 myScale = transform.localScale;
        myScale.x *= -1;
        transform.localScale = myScale;
    }
}
