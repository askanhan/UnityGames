using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float timeToDestroy;
    [SerializeField] private float speed;
    
    private float timer;

    private void Update()
    {
        if (timer > timeToDestroy)
        {
            Destroy(this);
        }
        
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        timer += Time.deltaTime;
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Character character))
        {
            character.TakeDamage();
            
            if (character is Player)
            {
                Player.Instance.UpdateHealth();
            }
        }
        
        Destroy(gameObject);
    }
}
