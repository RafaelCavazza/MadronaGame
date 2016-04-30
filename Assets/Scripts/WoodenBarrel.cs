using UnityEngine;
using System.Collections;

public class WoodenBarrel : MonoBehaviour {

    public float rotation = 3;
    public float DownSpeed = 0.00f;
    public float rotationDirection = 1;
    public float ExitSpeed = 30;

    /// <summary>
    /// <para> Define se os atributps do Script serão gerados aletoriamente. </para>
    /// </summary>
    public bool random = false;
   
    private Transform tranform;

	void Start () {
        tranform = GetComponent<Transform>();
        System.Random rand = new System.Random();
        if (random)
        {
            int value = rand.Next(-2, 2);
            if (value < 0)
                rotationDirection = -1;
            else
                rotationDirection = 1;
            rotation = rand.Next(1, 8);
            DownSpeed = ((float)(rand.Next(8, 30)))/100;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(rotation!=0)
            tranform.Rotate(0, 0, (rotation * rotationDirection));
        MoveBarrel();
    }

    void MoveBarrel()
    {
        if(DownSpeed!=0)
            this.transform.position = new Vector3(transform.position.x, transform.position.y - DownSpeed, 0);
    }

    void StopRotation()
    {
        rotation = 0;
        DownSpeed = 0;
    }

    void Dispose()
    {
        Destroy(this);
    }
}
