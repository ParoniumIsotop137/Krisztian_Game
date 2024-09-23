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
        // Timer hochz�hlen
        timer += Time.deltaTime;

        // �berpr�fen, ob es Zeit ist, den n�chsten Bierkrug zu spawnen
        if (timer >= spawnInterval)
        {
            SpawnBierkrug();
            if (transform.position.y < bottomBoundary.y)
            {
                Destroy(bierKrugPrefab); // Zerst�re den Bierkrug
            }
            else
            {
                Destroy(bierKrugPrefab, 8f);
            }
            timer = 0f; // Timer zur�cksetzen
        }

    }

    void SpawnBierkrug()
    {
        Debug.Log("Bierkrug wird gespawnt.");
        // Berechne die sichtbaren X-Grenzen des Bildschirms
        Vector3 leftBoundary = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 rightBoundary = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0));

        // Zuf�llige X-Position bestimmen, innerhalb des sichtbaren Bereichs
        float randomX = Random.Range(leftBoundary.x, rightBoundary.x);

        // Y-Position �ber dem Bildschirm
        float spawnY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + 2f; // Spawne etwas �ber dem Bildschirmrand

        // Instanziere den Bierkrug an der zuf�lligen X-Position und der festen Y-H�he
        Instantiate(bierKrugPrefab, new Vector2(randomX, spawnY), Quaternion.identity);
    }
}
