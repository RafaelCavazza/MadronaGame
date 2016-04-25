using UnityEngine;
using System.Collections;

public class WoodenBarrel : MonoBehaviour {

    public float rotation = 3;
    public float DownSpeed = 0.08f;
    public float rotationDirection = 1;
   
    private Transform tranform;
	// Use this for initialization
	void Start () {
        tranform = GetComponent<Transform>();
        System.Random rand = new System.Random();
        int value = rand.Next(-2, 2);
        if (value < 0)
            rotationDirection = -1;
        else
            rotationDirection = 1;
    }
	
	// Update is called once per frame
	void Update () {
        tranform.Rotate(0, 0, (rotation * rotationDirection));
        MoveBarrel();
    }

    void MoveBarrel()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y - DownSpeed, 0);
    }

    void StopRotation()
    {
        rotation = 0;
    }
}
