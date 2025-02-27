using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; //movement
    public float jumpForce = 5f; //jump
    

    private Animator animator;
    private float speed;
    private float moveInput;
    private Rigidbody2D rb; //rigid
    private bool isGrounded = false; //kijken als de player op de grond is
    private bool isFacingRight = true; //track welke direct de player faced

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        

        if(isFacingRight) //begin met die richting
        {
            Flip();
        }
        

    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal"); //get movement input -1 left +1 right 0 no input
        speed = Mathf.Abs(moveInput) > 0.1f ? Mathf.Abs(moveInput) : 0f; //speed bekijken

        animator.SetFloat("Speed", speed); //update speed on animation


        //moveplayer left and right
        transform.Translate(Vector2.right * moveInput * moveSpeed * Time.deltaTime);

        

        //flip sprite based on direction
        if (moveInput > 0 && isFacingRight)
        {
            Flip();
            Debug.Log("Player facing right");
        }
        else if (moveInput < 0 && !isFacingRight) //kijk welke richting het kijkt
        {
            Flip();
            Debug.Log("Player facing left");
        }

       
        if(isGrounded == true )
        {
            if (isGrounded && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //force jump


            }

        }

        

        
        
        

        //direct key inputs
        /*
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(Keycode.D)) //right
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            transform.localScale = new Vector3(1, 1, 1); 
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(Keycode.A)) //left
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            rb.velocity = new Vector2(0,rb.velocity.y); //stop moving when no key is pressed
        }
        */


        
    }
    //check als er jump wordt uigevoerd met collision
    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.contacts.Length > 0)
        {
            isGrounded = true; //player touches ground
            Debug.Log("Player is on ground");

        }

        
    }

    private void OnCollisionExit2D(Collision2D c)
    {
        isGrounded = false; //player is in the air
        Debug.Log("Player is in the air");

    }

    private void Flip()
    {
        isFacingRight = !isFacingRight; //toggle
        Vector3 scale = transform.localScale; //get current scale
        scale.x = isFacingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x); //flip only in direction change
        transform.localScale = scale; //apply flipped scale
    }


}
