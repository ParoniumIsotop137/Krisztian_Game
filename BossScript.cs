using UnityEngine;

public class BossScript : MonoBehaviour
{
    public float speed; // Bewegungsgeschwindigkeit des Gegners
    public float upperLimit; // Oberes Limit der Bewegung (dynamisch berechnet)
    public float lowerLimit; // Unteres Limit der Bewegung (dynamisch berechnet)
    public bool movingUp = true;
    public Rigidbody2D bossRb;

    public GameObject wrenchPrefab;
    public float shootInterval = 4f; // Zeitintervall zwischen den Würfen
    public float shootTimer;

    // Start is called before the first frame update
    void Start()
    {
        bossRb = GetComponent<Rigidbody2D>();

        if (bossRb == null)
        {
            Debug.LogError("Rigidbody2D-Komponente fehlt am Gegner!");
        }

        CalculateBounds();
        setSpeed();
        shootTimer = shootInterval;
    }

    // Update is called once per frame
    void Update()
    {
        moveBoss();
        handleShooting();
    }

    void handleShooting()
    {
        shootTimer -= Time.deltaTime; // Reduziere den Timer

        if (shootTimer <= 0)
        {
            shootWrench(); // Wirf einen Schraubenschlüssel
            shootTimer = shootInterval; // Setze den Timer zurück
        }
    }

    void shootWrench()
    {

        Instantiate(wrenchPrefab, transform.position, Quaternion.identity);


    }

    void moveBoss()
    {
        if (movingUp)
        {
            bossRb.velocity = Vector2.up * speed; // Setze Geschwindigkeit nach oben

            if (transform.position.y >= upperLimit - 2)
            {
                movingUp = false;
                setSpeed(); // Neue zufällige Geschwindigkeit setzen
            }
        }
        else
        {
            bossRb.velocity = Vector2.down * speed; // Setze Geschwindigkeit nach unten

            if (transform.position.y <= lowerLimit + 2)
            {
                movingUp = true;
                setSpeed(); // Neue zufällige Geschwindigkeit setzen
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
