using UnityEngine;

public class BossScript : MonoBehaviour
{
    public float speed; // Bewegungsgeschwindigkeit des Gegners
    public float upperLimit; // Oberes Limit der Bewegung (dynamisch berechnet)
    public float lowerLimit; // Unteres Limit der Bewegung (dynamisch berechnet)
    public bool movingUp = true;
    public Rigidbody2D bossRb;
    public GameObject bigExplosion;
    public GameObject wrenchPrefab;
    public float shootInterval = 1.5f; // Zeitintervall zwischen den Würfen
    public float shootTimer;
    public GameObject player;
    public int bossScore;
    public KrisztianScript krisztianScript;
    public GameObject winnerScreen;

    // Start is called before the first frame update
    void Start()
    {
        bossRb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("krisztian");
        krisztianScript = player.GetComponent<KrisztianScript>();

        bossScore = 15;
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
        if (player != null)
        {
            handleShooting();
        }

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

        Vector3 spawnPosition = transform.position + Vector3.right * 1.5f; // 1.5f ist der Abstand rechts vom Boss, den du anpassen kannst

        // Schraubenschlüssel an der berechneten Position instanziieren
        GameObject wrench = Instantiate(wrenchPrefab, spawnPosition, Quaternion.identity);

        // Optional: Kollision zwischen Schraubenschlüssel und Boss ignorieren
        Collider2D wrenchCollider = wrench.GetComponent<Collider2D>();
        Collider2D bossCollider = GetComponent<Collider2D>();

        if (wrenchCollider != null && bossCollider != null)
        {
            Physics2D.IgnoreCollision(wrenchCollider, bossCollider);
        }



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
        speed = Random.Range(8, 17);
    }
    public int getBossScore()
    {
        return this.bossScore;
    }

    public void RemoveBossScore(int bossScore)
    {
        if (this.bossScore > 0)
        {
            this.bossScore -= bossScore;
        }
        if (this.bossScore == 0)
        {
            playerWon();
        }

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("drotkefe_tag"))
        {
            RemoveBossScore(1);
            Debug.Log("A fõnököt találat érte: " + this.bossScore);
        }
    }

    public void playerWon()
    {
        GameObject explosion = Instantiate(bigExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(explosion, 1f);
        krisztianScript.setShootingStatus(false);
        winnerScreen.SetActive(true);
    }
}
