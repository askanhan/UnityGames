using System.Collections.Specialized;
using System.Transactions;
using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class BallBehaviour : MonoBehaviour
{
    private Rigidbody2D rb; //pshyics
    private bool isGrounded = false; //check als het de grond raakt
    private static int score = 0; //score
    private Transform groundCheck; //pos to chck ground

    public float bounceForce = 5f;
    public float bounceIncreaseRate = 0.5f;
    public float maxBounceForce = 20f; //max bounce force to prevent from bouncing out of control
    public float groundCheckDistance = 0.1f; //distance check for ground
    public TextMeshProUGUI scoreText; //ref textmeshproGUI
    public GameObject[] clouds; //def object en array
    public Material cloudMaterial; //public waarde houden
    public GameObject gameOverScreen; //canvas met die data invoegen
    public AudioSource muziek;

    //bal spawner
    public GameObject enemyBall; //enemy ball
    public GameObject pointBall; //point ball
    public float spawnAreaWidth = 5f; //breedte
    public float spawnAreaHeight = 5f; //hoogte
    public float spawnInterval = 2f; //interval spawn rate


    private float timer = 0f;
   









    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //init
        clouds = GameObject.FindGameObjectsWithTag("Clouds"); //zoek cloud tag in unity
        muziek.mute = false;
        



        groundCheck = new GameObject("RoadCheck").transform;
        groundCheck.parent = transform;
        groundCheck.localPosition = new Vector3(0, -0.5f, 0); //slightly below the ball
        gameOverScreen.SetActive(false); //begin omdat game niet over is


    }

    // Update is called once per frame
    void Update()
    {
        //ground check using overlapse
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.5f, 0.1f), 0); //bounce pschyics

        timer += Time.deltaTime; //regular intervals houden
        if (timer >= spawnInterval)
        {
            SpawnBall(); //spawn new ball
            timer = 0f; //reset
        }


        //increase force bounce overtime
        if (bounceForce < maxBounceForce)
        {
            bounceForce += bounceIncreaseRate * Time.deltaTime; //increase force overtime
        }



        if (isGrounded)
        {
            Bounce();

        }

        UpdateScoreUI(); //update UI
        AppearanceChanger();

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collided with" + other.collider.name); //general log

        if (other.collider.CompareTag("Pointball"))
        {
            Debug.Log("Nice");
            score += 1; //+1 score

        }
        else if (other.collider.CompareTag("EnemyBall"))
        {
            Debug.Log("Game Over");
            FreezeGame(); //stop game
            ShowGameOverScreen(); //laat game over zien
            muziek.mute = true;
        }
        else if (other.collider.CompareTag("Border") || other.collider.CompareTag("Ground"))
        {
            Debug.Log("The Wall!!!");
        }
        else
        {
            Debug.Log("Unknown collision" + other.collider.tag);
        }
    }

    private void Bounce()
    {
        //apply force by adjusting the vertical pos
        if (isGrounded)
        {
            float bounceMovement = bounceForce * Time.deltaTime; //bounce amount
            transform.position = new Vector2(transform.position.x, transform.position.y); //move upwards

        }

    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score; //add score
            Debug.Log("added a score!");

            

        }
    }

    private void FreezeGame()
    {
        Time.timeScale = 0; //freeze game, time stop
        Debug.Log("time frozen!");

    }

    private void AppearanceChanger()
    {        
        //change cloud based on score
        if (cloudMaterial != null)
        {
            if (score < 1)
            {
                cloudMaterial.SetFloat("_PhaseState", 0); //phase 1 => wit
                Debug.Log("Phase 1");
            }
            else if (score >= 1 && score < 3)
            {
                cloudMaterial.SetFloat("_PhaseState", 1); //phase 1 => geel
                Debug.Log("Phase 2");
            }
            else if (score >= 3)
            {
                cloudMaterial.SetFloat("_PhaseState", 2); //phase 1 => rood
                Debug.Log("Phase 3");
            }
        }
    }

    private void ShowGameOverScreen() //maak het zichtbaar methode
    {
        if(gameOverScreen != null)
        {
            gameOverScreen.SetActive(true); //maak het zichtbaar
        }
    }

    private void QuitApplication()
    {
        Application.Quit(); //quit game
        Debug.Log("quitting");
    }

    private void RestartGame()
    {
        Time.timeScale = 1; //tijd terug naar 1
        score = 0; //score terug 0 
        gameOverScreen.SetActive(false); //buttons en text moeten weg
        transform.position = new Vector2(2, 0); //restart pos
        DestroyClonesByTag("EnemyBall");  // Destroy enemy balls
        DestroyClonesByTag("Pointball");  // Destroy point balls

    }

    private void DestroyClonesByTag(string tag)
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag(tag); //find tag
        foreach(GameObject ball in balls )
        {
            Destroy(ball); //destroy all balls
        }
    }
    private void SpawnBall()
    {
        float randomX = Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2); //random spawn
        Vector2 spawnPosition = new Vector2(randomX, spawnAreaHeight); //spawn positie

        //randomly bepaal welke ball spawned
        if (Random.value > 0.5f)
        {
            Instantiate(enemyBall, spawnPosition, Quaternion.identity); //spawn enemy ball
        }
        else
        {
            Instantiate(pointBall, spawnPosition, Quaternion.identity); //spawn point ball
        }
    }
}
