using UnityEngine;
using UnityEngine.SceneManagement;

public class Fire : MonoBehaviour
{
    public static bool isStoped;
    public float UpForce = 0.08F;
    public PlayerController player;

    // Update is called once per frame
    void Update()
    {
        if (player.Pow && !Pause.paused)
            transform.position += Vector3.up * UpForce;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player":
                SceneManager.LoadScene("GameOver");
                break;
            case "Barrel":
                WoodenBarrel barril = other.GetComponent<WoodenBarrel>();
                if (barril.ItHasPlayer)
                    SceneManager.LoadScene("GameOver");
                else
                    Destroy(other.gameObject);
                break;
        }
    }
}
