using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    private bool starGame = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !starGame)
        {
            starGame = true;
            SceneManager.LoadScene("Main");
        }
    }
}
