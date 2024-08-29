using UnityEngine;

public class KrisztianScript : MonoBehaviour
{
    public float speed = 2;
    public Vector2 minBounds;
    public Vector2 maxBounds;
    public Vector3 newPosition;
    public Rigidbody2D kriszbody;

    // Start is called before the first frame update
    void Start()
    {
        CalculateBounds();
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


        HandleMovement();
        calculatePosition();


    }

    public void HandleMovement()
    {
        newPosition = transform.position;



        // Bewegung nur durchführen, wenn keine Kollision mit der Wand besteht
        if (Input.GetKey(KeyCode.W))
        {
            moveUp();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDown();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveRight();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveLeft();
        }

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

    public void moveRight()
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

}









