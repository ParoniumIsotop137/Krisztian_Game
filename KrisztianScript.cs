using UnityEngine;

public class KrisztianScript : MonoBehaviour
{
    public float speed = 2;
    public Vector2 minBounds;
    public Vector2 maxBounds;
    public Vector3 newPosition;
    public Rigidbody2D kriszbody;
    public GameObject brushPrefab;
    public int playerScore;
    public GameObject bigExplosion;
    public GameObject boss;
    public bool isOnShooting = false;
    public GameObject GameOverScreen;
    public AudioManager audioManger;


    public float delayBetweenSounds = 2f;

    public void Awake()
    {
        audioManger = GameObject.FindGameObjectWithTag("audio_tag").GetComponent<AudioManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
        playerScore = 5;
        kriszbody = GetComponent<Rigidbody2D>(); // Zugriff auf Rigidbody2D des Spielers
        boss = GameObject.FindGameObjectWithTag("boss_tag");


        if (kriszbody == null)
        {
            Debug.LogError("Rigidbody2D-Komponente fehlt am Spieler!");
        }


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
        handleShooting();

    }

    public void handleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isOnShooting = true;
            shootBrush();
        }
    }

    public void shootBrush()
    {
        if (boss != null)
        {
            Instantiate(brushPrefab, transform.position, Quaternion.identity);
        }

    }

    public void HandleMovement()
    {
        Vector2 movement = Vector2.zero;

        // Bewegung basierend auf Input steuern
        if (Input.GetKey(KeyCode.W))
        {
            movement = Vector2.up; // Bewegt den Spieler nach oben
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement = Vector2.down; // Bewegt den Spieler nach unten
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement = Vector2.right; // Bewegt den Spieler nach rechts
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movement = Vector2.left; // Bewegt den Spieler nach links
        }

        // Bewegungsgeschwindigkeit setzen
        kriszbody.velocity = movement * speed;

        // Begrenzung der Position innerhalb der Kamera-Ansicht
        kriszbody.position = new Vector2(
            Mathf.Clamp(kriszbody.position.x, minBounds.x, maxBounds.x),
            Mathf.Clamp(kriszbody.position.y, minBounds.y, maxBounds.y)
        );

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        // Logik bei Kollisionen, z.B. mit einem Projektil
        if (collision.gameObject.CompareTag("kulcs_prefab"))
        {


            // Spieler-Physik zurücksetzen
            kriszbody.velocity = Vector2.zero; // Geschwindigkeit auf null setzen

            // Spieler-Steuerung wiederherstellen
            kriszbody.constraints = RigidbodyConstraints2D.None;
            kriszbody.constraints = RigidbodyConstraints2D.FreezeRotation;// Nur Rotation auf Z einfrieren
            audioManger.playSound(audioManger.krisztianHit);

        }
    }





    public int getPlayerScore()
    {
        return this.playerScore;
    }

    public void AddScore(int score)
    {
        this.playerScore += score;
    }
    public void RemoveScore(int score)
    {
        if (this.playerScore > 0)
        {
            this.playerScore -= score;
        }

        if (this.playerScore == 0)
        {
            GameOver();
        }
    }
    public bool getShootingStatus()
    {
        return isOnShooting;
    }
    public void setShootingStatus(bool check)
    {
        this.isOnShooting = check;
    }

    public void GameOver()
    {
        GameObject explosion = Instantiate(bigExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(explosion, 1f);
        GameOverScreen.SetActive(true);
    }
}











