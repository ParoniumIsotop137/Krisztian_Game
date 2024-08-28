using UnityEngine;

public class BossScript : MonoBehaviour
{
    public float speed; // Bewegungsgeschwindigkeit des Gegners
    private float upperLimit; // Oberes Limit der Bewegung (dynamisch berechnet)
    private float lowerLimit; // Unteres Limit der Bewegung (dynamisch berechnet)

    private bool movingUp = true;

    // Start is called before the first frame update
    void Start()
    {
        CalculateBounds();
        setSpeed();

    }

    // Update is called once per frame
    void Update()
    {
        if (movingUp)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);

            // Wenn die obere Grenze erreicht ist, Richtung umkehren
            if (transform.position.y >= upperLimit - 2)
            {
                movingUp = false;
                setSpeed();
            }
        }
        else
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);

            // Wenn die untere Grenze erreicht ist, Richtung umkehren
            if (transform.position.y <= lowerLimit + 2)
            {
                movingUp = true;
                setSpeed();
            }
        }

    }

    void CalculateBounds()
    {
        // Kamera in der Szene bekommen (nur wenn eine Kamera da ist)
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            // Viewport-Bereiche in Weltkoordinaten umrechnen
            Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
            Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

            // Berechne die min und max Grenzen basierend auf der Kamera
            lowerLimit = bottomLeft.y;
            upperLimit = topRight.y;
        }
    }

    void setSpeed()
    {
        speed = Random.Range(3, 12);
    }

}
