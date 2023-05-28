using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerLife : MonoBehaviour
{
    public GameObject Body;
    public GameObject MoveableBody;

    private float lastCollisionTime = 0;
    private Vector3 startPosition = new Vector3(-8.38f, -1.58f, 0f);

    private void Start()
    {
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Die("Bullet");
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }
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
        if(tag != "Lava")
        {
            if(tag == "Trap" || tag == "Saw")
            {
                Vector3 oldPosition = transform.position;
                Instantiate(Body, oldPosition, Quaternion.identity);
            }
            else if(tag == "Bullet")
            {
                Vector3 oldPosition = transform.position;
                Instantiate(MoveableBody, oldPosition, Quaternion.identity);
            }
        }
        transform.position = startPosition;
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
