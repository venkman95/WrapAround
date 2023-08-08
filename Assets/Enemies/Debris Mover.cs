using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisMover : MonoBehaviour
{
    float rotation;
    Vector2 momentum;
    SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Collider2D collider2D;
        if (TryGetComponent<Collider2D>(out collider2D))
        {
            Destroy(collider2D);
        }
        Rigidbody2D rigidbody2D;
        if (TryGetComponent<Rigidbody2D>(out rigidbody2D))
        {
            Destroy(rigidbody2D);
        }
        momentum = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        rotation = Random.Range(-1,1);
    }
    // Update is called once per frame
    void Update()
    {
        
        Color color = spriteRenderer.color;
        if (color.a <= 0)
        {
            Destroy(gameObject);
        }
        color.a -= Time.deltaTime;
        spriteRenderer.color = color;
        transform.position += (Vector3)momentum * Time.deltaTime;
        transform.Rotate(Vector3.forward, rotation);

    }
}
