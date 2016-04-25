using UnityEngine;
using System.Collections;

public class WoodenBarrel : MonoBehaviour {

    public float rotation = 3;
    public float DownSpeed = 0.08f;
    public float rotationDirection = 1;
    public bool random = true;
    private bool CreateChild = false;
   
    private Transform tranform;
	// Use this for initialization
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
            //DownSpeed = ((float)(rand.Next(8, 30)))/100;
        }
    }
	
	// Update is called once per frame
	void Update () {
        tranform.Rotate(0, 0, (rotation * rotationDirection));
        MoveBarrel();
        CreateNewBarrrel();
        Dispose();
    }

    void MoveBarrel()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y - DownSpeed, 0);
    }

    void StopRotation()
    {
        rotation = 0;
        DownSpeed = 0;
    }

    void CreateNewBarrrel() {
        if (tranform.position.y < -5 && CreateChild==false)
        {
            var new_name = (name.Split('_')[1]);
            int number = int.Parse(new_name) + 2;
            var new_woodenBarrel = Instantiate<WoodenBarrel>(this);
            new_woodenBarrel.name = "WoodenBarrel_" + number.ToString();

            System.Random rand = new System.Random();
            int y = rand.Next(24, 32);
            int x = rand.Next(-16, +16);
            new_woodenBarrel.transform.position = new Vector3(x, y, 0);

            this.CreateChild = true;
        }
    }

    void Dispose()
    {
        if (tranform.position.y < -25)
        {
            GetComponent<Renderer>().enabled = false;
            Destroy(this);
        }
    }
}
