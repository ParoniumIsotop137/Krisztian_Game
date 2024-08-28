using System;
using UnityEngine;

public class KrisztianScript : MonoBehaviour
{
    public float speed = 2;
    public Vector2 minBounds;
    public Vector2 maxBounds;
    public Vector3 newPosition;
    public Rigidbody2D kriszbody;
    public Boolean isWall;

    // Start is called before the first frame update
    void Start()
    {
        CalculateBounds();
        isWall = false;
    }

    void CalculateBounds()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            // Viewport-Bereiche in Weltkoordinaten umrechnen
            Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
            Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

            // Berechne die min und max Grenzen basierend auf der Kamera
            minBounds = new Vector2(bottomLeft.x + 1, bottomLeft.y + 1);
            maxBounds = new Vector2(topRight.x + 1, topRight.y - 1);
        }
    }

    // Update is called once per frame

    void Update()
    {

        newPosition = transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            if (!isWall)
            {
                moveUp();
            }
            else
            {
                newDirection();
            }


        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (!isWall)
            {
                moveDown();
            }
            else
            {
                newDirection();
            }


        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (!isWall)
            {
                moveRigth();
            }
            else
            {
                newDirection();
            }


        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (!isWall)
            {
                moveLeft();
            }
            else
            {
                newDirection();
            }

        }
        calculatePosition();


    }

    public void calculatePosition()
    {
        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);


        transform.position = newPosition;
    }

    public void moveLeft()
    {
        newPosition += Vector3.left * Time.deltaTime * speed;
    }

    public void moveRigth()
    {
        newPosition += Vector3.right * Time.deltaTime * speed;
    }

    public void moveDown()
    {
        newPosition += Vector3.down * Time.deltaTime * speed;
    }

    public void moveUp()
    {
        newPosition += Vector3.up * Time.deltaTime * speed;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("cnc_static") && Input.GetKey(KeyCode.D))
        {
            isWall = true;
            newDirection();
            Debug.Log("Kollosion");
        }
        else if (collision.gameObject.CompareTag("vent_static") && Input.GetKey(KeyCode.D))
        {

            isWall = true;
            newDirection();
            Debug.Log("Kollosion");
        }

    }

    public void newDirection()
    {
        if (Input.GetKey(KeyCode.W))
        {
            isWall = false;
            Update();


        }
        else if (Input.GetKey(KeyCode.S))
        {
            isWall = false;
            Update();


        }
        else if (Input.GetKey(KeyCode.A))
        {
            isWall = false;
            Update();


        }


    }
}







