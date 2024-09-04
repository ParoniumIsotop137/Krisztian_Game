using UnityEngine;
using UnityEngine.UI;

public class BossScoreScript : MonoBehaviour
{
    public BossScript bossScript;
    public GameObject boss;
    public Text bossScoreText;
    void Start()
    {

        boss = GameObject.FindGameObjectWithTag("boss_tag");
        bossScript = boss.GetComponent<BossScript>();
        UpdateScoreText();

    }

    public void UpdateScoreText()
    {
        bossScoreText.text = bossScript.getBossScore().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScoreText();
    }
}
