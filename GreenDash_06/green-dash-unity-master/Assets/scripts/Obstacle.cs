using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float rightEdge;

    private void Start() {
        rightEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
    }
    
    public void Update()
    {
        transform.position += Vector3.right * GameManager.Instance.gameSpeed * Time.deltaTime;

        if (transform.position.x < rightEdge) {
            Destroy(gameObject);
        }   
    }
}
