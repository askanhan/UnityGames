using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3; //player got 3 health

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Die()
    {
        Debug.Log("Player Died");
        gameObject.SetActive(false); //disable player
    }

    void TakeDamage(int damage)
    {
        health -= damage; //take damage
        Debug.Log("Health:" + health);

        if(health == 0)
        {
            
            Die(); //player dies
        }
    }
}
