using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void StopGame()
    {
        Application.Quit();
    }
}
