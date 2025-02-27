using TMPro;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Speed of the bullet
    public float lifetime = 3f; // Lifetime of the bullet


    private Rigidbody2D rb; // Reference to Rigidbody2D
    public Sprite[] enemySprites; //array of enemy sprites
    private SpriteRenderer spriteRender; //laat het zien

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get Rigidbody2D component
        spriteRender = GetComponent<SpriteRenderer>(); //init
        int randomInedex = Random.Range(0, enemySprites.Length); //hoelang de array is
        spriteRender.sprite = enemySprites[randomInedex]; //render de sprites
        if (rb != null)
        {
            rb.gravityScale = 0; // Disable gravity
        } 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime; // Move the bullet
    }
}
