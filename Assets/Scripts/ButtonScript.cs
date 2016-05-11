using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
            SceneManager.LoadScene("StageComplete");
    }
}
