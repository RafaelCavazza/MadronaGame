using UnityEngine;
using System.Collections;

public class WoodenBarrel : MonoBehaviour {

    public float Rotation = 3;
    public float DownSpeed = 0.00f;
    public float RotationDirection = 1;
    public float ExitSpeed = 30;

    private Vector3 StatedPosition;

    public  bool   MoveXAxis;
    public  float  MoveXAxisValue;
    public  float  MoveXAxisSpeed;
    public  bool   MoveXAxisLeftToRigth;
    public  bool   MoveXAxisCentered;

    public bool  MoveYAxis;
    public float MoveYAxisValue;
    public float MoveYAxisSpeed;
    public bool  MoveYAxisDownToUp;
    public bool  MoveYAxisCentered;

    public bool random = false;
   
	void Start ()
	{
	    StatedPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        System.Random rand = new System.Random();
        if (random)
        {
            int value = rand.Next(-2, 2);
            if (value < 0)
                RotationDirection = -1;
            else
                RotationDirection = 1;
            Rotation = rand.Next(1, 8);
            DownSpeed = ((float)(rand.Next(8, 30)))/100;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(Rotation!=0)
            transform.Rotate(0, 0, (Rotation * RotationDirection));
        MoveBarrel();

	    MoveXAxisFunc();
        MoveYAxisFunc();
	}

    void MoveBarrel()
    {
        if(DownSpeed!=0)
            this.transform.position = new Vector3(transform.position.x, transform.position.y - DownSpeed, 0);
    }

    void StopRotation()
    {
        Rotation = 0;
        DownSpeed = 0;
    }

    void Dispose()
    {
        Destroy(this);
    }

    void MoveYAxisFunc()
    {
        if (!MoveYAxis)
            return;

        var addPositoin = (MoveYAxisSpeed * MoveYAxisValue); //Posição a Ser Adicionada no Elemento

        if (!MoveYAxisDownToUp) //se estiver movendo da esquerda para direira
            addPositoin = addPositoin * -1;

        if (MoveYAxisDownToUp) //Se está Movendo da esquerda par direita
        {
            addPositoin = ((addPositoin + transform.position.y) > MoveYAxisLimitValue()) ? MoveYAxisLimitValue() - transform.position.y : addPositoin;
            if (((addPositoin + transform.position.y) >= MoveYAxisLimitValue()))
            {
                MoveYAxisDownToUp = false;
            }
        }
        else
        {
            addPositoin = ((addPositoin + transform.position.y) < MoveYAxisLimitValue()) ? MoveYAxisLimitValue() - transform.position.y : addPositoin;
            if ((addPositoin + transform.position.y) <= MoveYAxisLimitValue())
            {
                MoveYAxisDownToUp = true;
            }
        }

        transform.position = new Vector3(transform.position.x, transform.position.y+ addPositoin, transform.position.z);
    }

    void MoveXAxisFunc()
    {
        if (!MoveXAxis)
            return;

        var addPositoin = (MoveXAxisSpeed * MoveXAxisValue); //Posição a Ser Adicionada no Elemento

        if (!MoveXAxisLeftToRigth) //se estiver movendo da esquerda para direira
            addPositoin = addPositoin*-1;

        if (MoveXAxisLeftToRigth) //Se está Movendo da esquerda par direita
        {
            addPositoin = ((addPositoin + transform.position.x) > MoveXAxisLimitValue()) ? MoveXAxisLimitValue() - transform.position.x : addPositoin;
            if (((addPositoin + transform.position.x) >= MoveXAxisLimitValue()))
            {
                MoveXAxisLeftToRigth = false;
            }
        }
        else
        {
            addPositoin = ((addPositoin + transform.position.x) < MoveXAxisLimitValue()) ? MoveXAxisLimitValue() - transform.position.x : addPositoin;
            if ((addPositoin + transform.position.x) <= MoveXAxisLimitValue())
            {
                MoveXAxisLeftToRigth = true;
            }
        }

        transform.position = new Vector3(transform.position.x + addPositoin, transform.position.y, transform.position.z); 
    }

    float MoveXAxisLimitValue()
    {
        if (MoveXAxisCentered)
        {
            if(MoveXAxisLeftToRigth)
                return StatedPosition.x + (MoveXAxisValue/2);
            return StatedPosition.x - (MoveXAxisValue / 2);
        }
        else
        {
            if (MoveXAxisLeftToRigth)
                return StatedPosition.x + (MoveXAxisValue);
            return StatedPosition.x;
        }
    }

    float MoveYAxisLimitValue()
    {
        if (MoveYAxisCentered)
        {
            if (MoveYAxisDownToUp)
                return StatedPosition.y + (MoveYAxisValue / 2);
            return StatedPosition.y - (MoveYAxisValue / 2);
        }
        else
        {
            if (MoveYAxisDownToUp)
                return StatedPosition.y + (MoveYAxisValue);
            return StatedPosition.y;
        }
    }

}
