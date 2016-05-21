using UnityEngine;
using UnityEngine.SceneManagement;

public class WinButtonScript : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
            SceneManager.LoadScene("StageComplete");
    }
}
