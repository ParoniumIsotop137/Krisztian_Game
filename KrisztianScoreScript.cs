using UnityEngine;
using UnityEngine.UI;

public class KrisztianScoreScript : MonoBehaviour
{
    public KrisztianScript krisztianScript;
    public GameObject player;
    public Text krisztianScoreText;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("krisztian");
        krisztianScript = player.GetComponent<KrisztianScript>();
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        krisztianScoreText.text = krisztianScript.getPlayerScore().ToString();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateScoreText();
    }
}
