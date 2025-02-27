using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private static int score = 0; //score

    public TextMeshProUGUI scoreText; //ref textmeshproGUI

    /*
    public string targetTag = "Clouds"; //tag zoeken
    public Material phase1Material;
    

    public void ResetMaterialColorByTag()
    {
        //zoek alle objects met die tag
        GameObject[] imageObjects = GameObject.FindGameObjectsWithTag(targetTag);



        foreach (GameObject image in imageObjects)
        {
            //check als gameobject renderer heeft
            Renderer renderer = image.GetComponent<Renderer>();
            if (renderer != null)
            {
                //reset mat
                renderer.material = phase1Material; // Apply the phase 1 material to reset color
                Debug.Log("Color reset for " + image.name + " to Phase 1 color.");
            }
            else
            {
                Debug.LogWarning("Renderer not found for " + image.name);
            }
        }
    }

    */
    public void RestartGame()
    {
        Time.timeScale = 1f; //continue timer

        SceneManager.LoadScene("GameScreen");
        //ResetMaterialColorByTag(); methode toepassen
        

    }
}
