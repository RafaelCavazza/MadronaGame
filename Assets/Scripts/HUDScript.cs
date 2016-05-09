using UnityEngine;
using System.Collections;

public class HUDScript : MonoBehaviour
{

    float playerScrore;

    // Use this for initialization
    void Start()
    {
        playerScrore = 120;
    }

    // Update is called once per frame
    void Update()
    {
        playerScrore -= Time.deltaTime;
    }

    void OnDisable()
    {
        PlayerPrefs.SetInt("Score", (int)playerScrore);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 30), "Score: " + (int)playerScrore);
    }
}
