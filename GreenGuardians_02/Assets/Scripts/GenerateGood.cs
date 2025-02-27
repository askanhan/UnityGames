using System.Collections;
using UnityEngine;

public class GenerateGood : MonoBehaviour
{
    public GameObject good; //good
    public int posX;
    public int posY;
    public int enemyCount; //hoeveel enemies er zijn

    private void Start()
    {
        StartCoroutine(GoodDrop()); //start routine


    }

    IEnumerator GoodDrop()
    {
        while (GameObject.Find("Player") == enabled)
        {

            posX = Random.Range(-28, 28); //x coord spawn
            posY = Random.Range(-8, 10); //y coord spawn
            Instantiate(good, new Vector2(posX, posY), Quaternion.identity); //identifeer
            yield return new WaitForSeconds(2f);
            enemyCount++; //optellen
        }
    }
}
