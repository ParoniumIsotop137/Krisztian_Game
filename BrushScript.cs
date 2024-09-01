using UnityEngine;

public class BrushScript : MonoBehaviour
{
    public float speed = 12f;
    public float rotationSpeed = 400f;
    public Rigidbody2D brushRb;
    public GameObject explosionPrefab;
    private Vector2 targetDirection;
    // Start is called before the first frame update
    void Start()
    {
        brushRb = GetComponent<Rigidbody2D>();

        // Überprüfen, ob Rigidbody2D korrekt zugewiesen ist
        if (brushRb == null)
        {
            Debug.LogError("Rigidbody2D-Komponente fehlt an der Drahtbürste!");
        }

        // Zielrichtung berechnen basierend auf der aktuellen Position des Bosses
        UpdateTargetDirection();
    }

    public void UpdateTargetDirection()
    {
        GameObject boss = GameObject.FindGameObjectWithTag("boss_tag");

        // Überprüfen, ob der Boss existiert
        if (boss != null)
        {
            targetDirection = (boss.transform.position - transform.position).normalized;
        }
        else
        {
            Debug.LogWarning("Kein Boss-Objekt gefunden!");
            targetDirection = Vector2.right; // Fallback-Richtung, falls kein Boss vorhanden ist
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject != null)
        {
            // Bewege die Drahtbürste in Richtung des Ziels
            brushRb.velocity = targetDirection * speed;

            // Rotieren der Drahtbürste
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("boss_tag"))
        {
            Debug.Log("Ez az!");
            Explosion();
            Destroy(gameObject);


        }
        else if (collision.gameObject.CompareTag("cnc_static") || collision.gameObject.CompareTag("vent_static"))
        {
            Explosion();
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("kulcs_prefab"))
        {

            Destroy(gameObject, 2f);
        }
        else
        {

            Destroy(gameObject, 8f);
        }
    }
    public void Explosion()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 2f);
    }
}
