using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;

public class BodyLife : MonoBehaviour
{

    private float lastCollisionTime = 0;
    private Vector3 startPosition = new Vector3(-8.38f, -1.58f, 0f);

    private void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1.0f, 0f)*20, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float collisionTime = Time.time;
        if ((collision.gameObject.CompareTag("Trap") 
            || collision.gameObject.CompareTag("Lava") 
            || collision.gameObject.CompareTag("Bullet")
            || collision.gameObject.CompareTag("Saw"))
            && IsNewCollision(collisionTime))
        {
            lastCollisionTime = collisionTime;
            Die(collision.gameObject.tag);
        }
    }

    private void Die(string tag)
    {
        if (tag == "Lava")
        {
            Destroy(gameObject);
        }
        else
        {
            if (tag == "Trap" || tag == "Saw")
            {
                Destroy(gameObject.GetComponent<Rigidbody2D>());
            }
        }
    }

    private bool IsNewCollision(float currentTime)
    {
        return (currentTime - lastCollisionTime) > 0.001;
    }
}
