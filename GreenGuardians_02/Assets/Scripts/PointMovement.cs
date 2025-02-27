using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PointMovement : MonoBehaviour
{
    private Rigidbody2D rb; // Reference to Rigidbody2D
    public Sprite[] goodSprites; //array of enemy sprites
    private SpriteRenderer spriteRender; //laat het zien

    public float speed = 10f; // Speed of the bullet
    public float lifetime = 3f; // Lifetime of the bullet
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get Rigidbody2D component
        spriteRender = GetComponent<SpriteRenderer>(); //init
        int randomInedex = Random.Range(0, goodSprites.Length); //hoelang de array is
        spriteRender.sprite = goodSprites[randomInedex]; //render de sprites
        if (rb != null)
        {
            rb.gravityScale = 0; // Disable gravity
        }
    }

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime; // Move the bullet
    }
}
