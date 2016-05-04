using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour
{

    public float UpForce = 0.08F;
    public PlayerController player;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(player.Pow)
        transform.position += Vector3.up * UpForce;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
            Debug.Break();
       else if(other.tag.Equals("Barrel"))
            Destroy(other.gameObject);
    }
}
