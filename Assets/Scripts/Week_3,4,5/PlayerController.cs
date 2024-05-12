using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 4;
    public float jump = 558;
    public Transform groundCheck;
    public LayerMask whatIsGround;
   
    public bool lookingRight = true;

    private Rigidbody2D rb2d;
    private Animator anim;
     bool grounded = false;
   
   

    SpriteRenderer spriteRenderer;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
      

        UiManager.Instance.InitLevel();
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

        if(!isCollide)
        rb2d.velocity = new Vector2(hor * speed, rb2d.velocity.y);      

        anim.SetFloat("V", rb2d.velocity.normalized.magnitude);

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

    void SpriteTransparentFx(float a)
    {
        Color tmp = spriteRenderer.color;
        tmp.a = a;
        spriteRenderer.color = tmp;

        foreach (Transform child in transform.GetChild(0))
        {
            tmp = child.GetComponent<SpriteRenderer>().color;
            tmp.a = a;
            child.GetComponent<SpriteRenderer>().color = tmp;
        }

    }
    IEnumerator HpEffect()
    {
        int tim = 33;
        while (tim > 0)
        {
            tim--;

            SpriteTransparentFx(.3f);
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            SpriteTransparentFx(.9f);

            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();


            if (tim == 32)
            {
               
                rb2d.velocity = Vector3.zero;
            }

            if (tim <=25)
            {
                isCollide = false;
              
            }


        }

        SpriteTransparentFx(1f);
        isLife = true;
    }

    bool isLife = true;
    bool isCollide = false;
    int life = 3;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trap" && isLife)
        {

            if (life > 0)
            {
                life--;

                Week3_GameManager.Instance.Life(life);

                isLife = false;
                isCollide = true;

                StartCoroutine(HpEffect());               
              
            }
          

            return;
           
        }

       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bonus")
        {
            Destroy(collision.gameObject);
            Week3_GameManager.Instance.AddScore();
        }       

        
    }

}