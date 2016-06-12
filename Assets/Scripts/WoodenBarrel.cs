using System;
using UnityEngine;

public class WoodenBarrel : MonoBehaviour
{
    public bool ItHasPlayer { get; set; }

    public bool X;
    public float Rotation = 3;
    public int RotationDirection = 1;
    public float RotationAngle = 120;
    public float ExitSpeed = 80;
    public bool ContinuousRotation = false;

    public bool RotatePlayer = true;
    public float RotatePlayerSpeed = 2f;

    private float RotatedValue;
    private Vector2 StartedPosition;

    public bool MoveXAxis;
    public float MoveXAxisValue;
    public float MoveXAxisSpeed;
    public bool MoveXAxisLeftToRigth;
    public bool MoveXAxisCentered;

    public bool MoveYAxis;
    public float MoveYAxisValue;
    public float MoveYAxisSpeed;
    public bool MoveYAxisDownToUp;
    public bool MoveYAxisCentered;

    public bool StartOnPlayerEnter;
    public bool StopOnPlayerExit;

    public bool playerEntered = false;
    public bool playerExited = false;

    void Start()
    {
        StartedPosition = transform.position;
        ItHasPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Pause.paused)
        {
            if (StartOnPlayerEnter && !playerEntered)
                return;

            if (StopOnPlayerExit && playerExited)
                return;

            MoveXAxisFunc();
            MoveYAxisFunc();
            RotateBarrel();
        }
    }

    void RotateBarrel()
    {
        var rotateValue = Rotation * RotationDirection;

        if (ContinuousRotation)
        {
            transform.Rotate(new Vector3(0, 0, rotateValue));
            return;
        }

        if (RotationDirection != -1)
        {
            if (Math.Abs(RotatedValue - RotationAngle) < 0.1)
            {
                RotatedValue = 0;
                rotateValue = 0;
                RotationDirection = -1;
            }
        }
        else
        {
            if (Math.Abs(RotatedValue - RotationAngle) < 0.1)
            {
                RotatedValue = 0;
                rotateValue = 0;
                RotationDirection = 1;
            }
        }
        RotatedValue += (rotateValue < 0 ? rotateValue * -1 : rotateValue);
        var rotation = new Vector3(0, 0, rotateValue);
        transform.Rotate(rotation);
    }

    void StopRotation()
    {
        Rotation = 0;
    }

    void Dispose()
    {
        Destroy(this);
    }

    void MoveYAxisFunc()
    {
        if (MoveYAxis)
        {
            var addPosition = (MoveYAxisSpeed * MoveYAxisValue); //Posição a Ser Adicionada no Elemento

            if (!MoveYAxisDownToUp) //se estiver movendo da esquerda para direira
                addPosition = addPosition * -1;

            if (MoveYAxisDownToUp) //Se está Movendo da esquerda par direita
            {
                addPosition = ((addPosition + transform.position.y) > MoveYAxisLimitValue()) ? MoveYAxisLimitValue() - transform.position.y : addPosition;
                if (((addPosition + transform.position.y) >= MoveYAxisLimitValue()))
                {
                    MoveYAxisDownToUp = false;
                }
            }
            else
            {
                addPosition = ((addPosition + transform.position.y) < MoveYAxisLimitValue()) ? MoveYAxisLimitValue() - transform.position.y : addPosition;
                if ((addPosition + transform.position.y) <= MoveYAxisLimitValue())
                {
                    MoveYAxisDownToUp = true;
                }
            }

            transform.position = new Vector2(transform.position.x, transform.position.y + addPosition);
        }
    }

    void MoveXAxisFunc()
    {
        if (MoveXAxis)
        {
            var addPosition = (MoveXAxisSpeed * MoveXAxisValue); //Posição a Ser Adicionada no Elemento

            if (!MoveXAxisLeftToRigth) //se estiver movendo da esquerda para direira
                addPosition = addPosition * -1;

            if (MoveXAxisLeftToRigth) //Se está Movendo da esquerda par direita
            {
                addPosition = ((addPosition + transform.position.x) > MoveXAxisLimitValue()) ? MoveXAxisLimitValue() - transform.position.x : addPosition;
                if (((addPosition + transform.position.x) >= MoveXAxisLimitValue()))
                {
                    MoveXAxisLeftToRigth = false;
                }
            }
            else
            {
                addPosition = ((addPosition + transform.position.x) < MoveXAxisLimitValue()) ? MoveXAxisLimitValue() - transform.position.x : addPosition;
                if ((addPosition + transform.position.x) <= MoveXAxisLimitValue())
                {
                    MoveXAxisLeftToRigth = true;
                }
            }

            transform.position = new Vector2(transform.position.x + addPosition, transform.position.y);
        }
    }

    float MoveXAxisLimitValue()
    {
        if (MoveXAxisCentered)
        {
            if (MoveXAxisLeftToRigth)
                return StartedPosition.x + (MoveXAxisValue / 2);
            return StartedPosition.x - (MoveXAxisValue / 2);
        }
        else
        {
            if (MoveXAxisLeftToRigth)
                return StartedPosition.x + (MoveXAxisValue);
            return StartedPosition.x;
        }
    }

    float MoveYAxisLimitValue()
    {
        if (MoveYAxisCentered)
        {
            if (MoveYAxisDownToUp)
                return StartedPosition.y + (MoveYAxisValue / 2);
            return StartedPosition.y - (MoveYAxisValue / 2);
        }
        else
        {
            if (MoveYAxisDownToUp)
                return StartedPosition.y + (MoveYAxisValue);
            return StartedPosition.y;
        }
    }

}
