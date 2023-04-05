using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerLife : MonoBehaviour
{
    public GameObject Body;

    private float lastCollisionTime = 0;
    private Vector3 startPosition = new Vector3(-8.38f, -1.58f, 0f);

    private void Start()
    {
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Die();
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            DestroyAllBodies();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float collisionTime = Time.time;
        if (collision.gameObject.CompareTag("Trap") && IsNewCollision(collisionTime))
        {
            lastCollisionTime = collisionTime;
            Die();
        }
    }

    private void Die()
    {
        Vector3 oldPosition = transform.position;
        transform.position = startPosition;
        Instantiate(Body, oldPosition, Quaternion.identity);
    }

    private bool IsNewCollision(float currentTime)
    {
        return (currentTime - lastCollisionTime) > 0.001;
    }
    void DestroyAllBodies()
    {
        foreach (var body in GameObject.FindGameObjectsWithTag("Body"))
        {
            Destroy(body);
        }
    }
}
