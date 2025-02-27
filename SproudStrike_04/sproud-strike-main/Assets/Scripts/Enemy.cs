using System.Collections;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private int countShootToPlayer;

    private bool canShoot = true;
    
    public Vector2 Direction
    {
        set
        {
            direction = value;
            directionShoot = direction;
        }
    }

    private void Update()
    {
        base.Update();
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        var hit = Physics2D.Raycast(transform.position, direction, 100f, 1 << LayerMask.NameToLayer("Player"));

        if (!hit.collider)
        {
            return;
        }
        
        if (hit.collider.gameObject.TryGetComponent(out Player player) && canShoot)
        {
            canShoot = false;
            StartCoroutine(SerialShoot());
        }
    }

    private IEnumerator SerialShoot()
    {
        int counter = 0;
        
        while (countShootToPlayer >= counter)
        {
            Shoot();
            yield return new WaitForSeconds(1f);
            counter++;
        }

        canShoot = true;
    }
}