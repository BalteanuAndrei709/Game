using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Vector3 originalPos;
    bool moveBack = false;
    public GameObject block;
    public bool value;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.name == "Player" || collision.transform.name == "MoveableBody(Clone)")
        {
            if (originalPos.y - transform.position.y < 0.5f)
            {
                transform.Translate(0, -0.01f, 0);
            }
            else if (originalPos.y - transform.position.y >= 0.5f)
            {
                block.SetActive(value);
            }
            moveBack = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Player" || collision.transform.name == "MoveableBody(Clone)")
        {
            collision.transform.parent = transform;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.name == "Player" || collision.transform.name == "MoveableBody(Clone)")
        {

            moveBack = true;
            collision.transform.parent = null;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moveBack)
        {
            if(transform.position.y < originalPos.y)
            {
                transform.Translate(0, 0.1f, 0);
            }
            else
            {
                block.SetActive(!value);
                moveBack = false;
            }
        }
    }
}
