using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMangement : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1.0f; //continue timer

        SceneManager.LoadScene("GameScreen"); //load the screen opnieuw
        Debug.Log("Reloading scene...");
    }

    public void ExitGame()
    {
        Application.Quit(); //exit game
        Debug.Log("Application quitting...");
    }
    
}
