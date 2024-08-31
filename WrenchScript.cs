using UnityEngine;

public class WrenchScript : MonoBehaviour
{
    public float speed = 12f;
    public Vector2 targetDirection;
    public GameObject player;
    public float rotationSpeed = 400f;
    public Rigidbody2D wrenchRb;
    public GameObject explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("krisztian");
        wrenchRb = GetComponent<Rigidbody2D>();

        if (player != null)
        {
            targetDirection = (player.transform.position - transform.position).normalized;
        }
        wrenchRb.velocity = targetDirection * speed;


    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject != null)
        {

            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }



    }
    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("krisztian"))
        {
            Debug.Log("Az isten bassza meg!");
            Explosion();
            Destroy(gameObject);


        }
        else if (collision.gameObject.CompareTag("cnc_static") || collision.gameObject.CompareTag("vent_static"))
        {
            Explosion();
            Destroy(gameObject);
        }
        else
        {
            Explosion();
            Destroy(gameObject, 20f);
        }


    }

    public void Explosion()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 2f);
    }
}
