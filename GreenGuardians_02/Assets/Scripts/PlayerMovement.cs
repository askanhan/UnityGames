using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    float horizontalInput;
    float moveSpeedX = 10f;
    bool isFacingRight = false;
    float jumpForce = 10f;
    float doubleJumpForce = 5f;
    bool isGrounded = false;
    private Vector3 prevPos;
    int jumpCount = 0;
    int maxJumpCount = 2; //double jump

    //public
    public GameObject gameoverPanel;
    public AudioSource gameOverMuziek;
    public AudioSource gameMuziek;
    public TextMeshProUGUI scoreText;
    private int score = 0;
    public GameObject pointGameObject;

    Rigidbody2D rb;
    Animator anim;

    void Start()
    {
        prevPos = transform.position; //start pos
        rb = GetComponent <Rigidbody2D>(); //init
        anim = GetComponent <Animator>();
        gameMuziek.mute = false;
        gameOverMuziek.mute = true;
        gameoverPanel.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        FlipSprite();

        if(Input.GetButtonDown("Jump"))
        {
            if(isGrounded)
            {
                jumpCount = 0; //jump count 0
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //forcen
                isGrounded = false; //terug false
                anim.SetTrigger("isJumping");
                anim.ResetTrigger("isDoubleJumping");
                
            }
            else if(jumpCount < maxJumpCount)
            {
                jumpCount++; //jumpen
                rb.AddForce(Vector2.up * doubleJumpForce, ForceMode2D.Impulse); //forcen grav
                anim.SetTrigger("isDoubleJumping");
                anim.ResetTrigger("isJumping");
            }
        }
        
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(horizontalInput * moveSpeedX,0); //only x direction
        rb.AddForce(movement, ForceMode2D.Force);//hor move

        
                                                                                 // Calculate the speed along the x and y axes by comparing position change
        float speedX = Mathf.Abs(transform.position.x - prevPos.x) / Time.deltaTime; // x-axis speed
        float speedY = Mathf.Abs(transform.position.y - prevPos.y) / Time.deltaTime; // y-axis speed

        prevPos = transform.position; //update pos

        anim.SetFloat("xVelocity", speedX); //animaties declareren
        anim.SetFloat("yVelocity", speedY);


    }

    void FlipSprite()
    {
        if(isFacingRight && horizontalInput < 0f || (!isFacingRight && horizontalInput > 0f))
        {
            isFacingRight = !isFacingRight; //flip
            Vector3 ls = transform.localScale; //transform
            ls.x *= -1f; //coord bepalen
            transform.localScale = ls; 
        }

    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; //player ground
            anim.ResetTrigger("isJumping");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground")) //check if ground leave
        {
            isGrounded = false; //no longer ground
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {

        if(trigger.CompareTag("Death") || trigger.CompareTag("Enemy")) //check enemy + death
        {
            Time.timeScale = 0;
            this.gameObject.SetActive(false);
            gameoverPanel.SetActive(true);
            gameOverMuziek.mute = false;
            gameMuziek.mute = true;

        }

        // Check if the object has the "Player" tag
        if (trigger.CompareTag("Point"))
        {
            
            score++;
            UpdateUI();
            


        }
        else
        {
            Debug.LogWarning("Error");
        }
    }
    private void UpdateUI()
    {
        if (scoreText != null) //als het niet null is
        {
            scoreText.text = "Score: " + score; //add score
            Debug.Log("added score");
        }
    }
}
