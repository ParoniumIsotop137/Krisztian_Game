using UnityEngine;

public class BierkrugSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D krugBody;
    public GameObject bierKrugPrefab;
    public float spawnInterval = 15f;   // Zeitintervall zwischen den Spawns
    private float timer;               // Timer, um das Zeitintervall zu steuern
    private Camera mainCamera;

    void Start()
    {
        // Hole die Hauptkamera, um die sichtbaren Grenzen zu berechnen
        mainCamera = Camera.main;
        krugBody = GetComponent<Rigidbody2D>();
        timer = 0f;

    }

    void Update()
    {
        Vector3 bottomBoundary = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        // Timer hochzählen
        timer += Time.deltaTime;

        // Überprüfen, ob es Zeit ist, den nächsten Bierkrug zu spawnen
        if (timer >= spawnInterval)
        {
            SpawnBierkrug();
            if (transform.position.y < bottomBoundary.y)
            {
                Destroy(bierKrugPrefab); // Zerstöre den Bierkrug
            }
            else
            {
                Destroy(bierKrugPrefab, 8f);
            }
            timer = 0f; // Timer zurücksetzen
        }

    }

    void SpawnBierkrug()
    {
        Debug.Log("Bierkrug wird gespawnt.");
        // Berechne die sichtbaren X-Grenzen des Bildschirms
        Vector3 leftBoundary = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 rightBoundary = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0));

        // Zufällige X-Position bestimmen, innerhalb des sichtbaren Bereichs
        float randomX = Random.Range(leftBoundary.x, rightBoundary.x);

        // Y-Position über dem Bildschirm
        float spawnY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + 2f; // Spawne etwas über dem Bildschirmrand

        // Instanziere den Bierkrug an der zufälligen X-Position und der festen Y-Höhe
        Instantiate(bierKrugPrefab, new Vector2(randomX, spawnY), Quaternion.identity);
    }
}
