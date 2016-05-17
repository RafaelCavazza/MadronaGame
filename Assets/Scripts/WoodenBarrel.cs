using UnityEngine;

public class WoodenBarrel : MonoBehaviour
{
    public bool ItHasPlayer { get; set; }

    public float Rotation = 3;
    public float DownSpeed = 0.00f;
    public float RotationDirection = 1;
    public float ExitSpeed = 30;

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

    void Start()
    {
        StartedPosition = new Vector2(transform.position.x, transform.position.y);
        ItHasPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Rotation != 0)
            transform.Rotate(0, 0, (Rotation * RotationDirection));
        MoveBarrel();

        MoveXAxisFunc();
        MoveYAxisFunc();
    }

    void MoveBarrel()
    {
        if (DownSpeed != 0)
            this.transform.position = new Vector2(transform.position.x, transform.position.y - DownSpeed);
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

    void MoveXAxisFunc()
    {
        if (!MoveXAxis)
            return;

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
