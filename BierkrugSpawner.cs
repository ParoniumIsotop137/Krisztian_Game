using UnityEngine;

public class BierkrugSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D krugBody;
    public GameObject bierKrugPrefab;
    public float spawnInterval;   // Zeitintervall zwischen den Spawns
    public float timer;               // Timer, um das Zeitintervall zu steuern
    private Camera mainCamera;
    public GameObject player;           // Referenz zum Spieler
    public KrisztianScript krisztianScript;
    public Renderer beerRenderer;

    void Start()
    {
        // Hole die Hauptkamera, um die sichtbaren Grenzen zu berechnen
        mainCamera = Camera.main;
        timer = 0f;
        player = GameObject.FindGameObjectWithTag("krisztian");
        krisztianScript = player.GetComponent<KrisztianScript>();
        beerRenderer = bierKrugPrefab.GetComponent<Renderer>();
        beerRenderer.enabled = true;

    }

    void Update()
    {

        // Timer hochzählen
        timer += Time.deltaTime;

        // Überprüfen, ob es Zeit ist, den nächsten Bierkrug zu spawnen
        if (timer >= spawnInterval)
        {
            spawnInterval = Random.Range(15f, 30f);
            SpawnBierkrug();
            timer = 0f; // Timer zurücksetzen
        }

    }

    void SpawnBierkrug()
    {
        beerRenderer.enabled = true;
        Debug.Log("Bierkrug wird gespawnt.");
        // Berechne die sichtbaren X-Grenzen des Bildschirms
        Vector3 leftBoundary = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 rightBoundary = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0));

        // Zufällige X-Position bestimmen, innerhalb des sichtbaren Bereichs
        float randomX = Random.Range(leftBoundary.x, rightBoundary.x);

        // Y-Position über dem Bildschirm
        float spawnY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + 2f; // Spawne etwas über dem Bildschirmrand

        // Instanziere den Bierkrug an der zufälligen X-Position und der festen Y-Höhe
        GameObject spawnedKrug = Instantiate(bierKrugPrefab, new Vector2(randomX, spawnY), Quaternion.identity);

        Destroy(spawnedKrug, 8f);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("krisztian"))
        {
            krisztianScript.AddScore(1);
            beerRenderer.enabled = false;

        }
    }
}
