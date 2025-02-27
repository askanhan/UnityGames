using UnityEngine;

public class Character : MonoBehaviour
{
    public int MaxHealth => maxHealth;
    public int Health => health;
    
    [SerializeField] private int maxHealth;
    [SerializeField] private float speed;
    [SerializeField] private GameObject bullet;
    
    protected Vector2 direction;
    protected Vector2 directionShoot;
    
    private int health;

    protected void Awake() => health = maxHealth;
    
    public void TakeDamage()
    {
        health -= 1;
        if (health <= 0)
        {
            Destroy(gameObject);

            if (this is Enemy)
            {
                Player.Instance.AddKill();
            }
        }
    }

    protected void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
    
    protected void Shoot()
    {
        var spawnbullet = Instantiate(bullet);
        spawnbullet.transform.position = transform.position;
        spawnbullet.transform.rotation = Quaternion.Euler(getRotation());
    }

    protected void Move(Vector2 direction)
    {
        this.direction = direction;
        // blokowanie porusanie się na skos
        if (Mathf.Abs(direction.x) == Mathf.Abs(direction.y) && Mathf.Abs(direction.x) == 1f)
        {
            this.direction = Vector2.zero;
            return;
        }

        if (direction != Vector2.zero)
        {
            directionShoot = direction;
        }
    }

    protected Vector3 getRotation() 
    {
        float angle = Vector2.SignedAngle(directionShoot, Vector2.up);
        angle = Mathf.Abs(angle) == 90 ? -angle : angle;
        return new Vector3(0f, 0f, angle);
    }
}