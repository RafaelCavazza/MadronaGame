using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumpForce;

    public bool Pow { get; set; }
    private Rigidbody2D MyRigidbody;
    private float _tempGravityScale = 0;
    private bool isInBarrel = false;
    private WoodenBarrel Barrel;
    private BoxCollider2D BarrelBoxColider;
    private CircleCollider2D PlayerColider;
    public AudioSource PowSound;
    public AudioSource JumpSound;

    private bool  BarrelRotatePlayer = false;
    private float BarrelRotatePlayerSpeed = 0;

    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        this.MyRigidbody = GetComponent<Rigidbody2D>();
        this.PlayerColider = GetComponent<CircleCollider2D>();
    }
     
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isInBarrel)
            ExitBarel();

        RotatePlayer();
    }

    void RotatePlayer()
    {
        if (!BarrelRotatePlayer)
            return;
        transform.Rotate(0, 0, BarrelRotatePlayerSpeed);
    }

    void OnCollisionEnter2D(Collision2D colliderObject)
    {
        if (colliderObject.gameObject.tag.Equals("Barrel"))
        {
            BarrelBoxColider = colliderObject.gameObject.GetComponent<BoxCollider2D>();
            PlayerColider.enabled = false;
            Barrel = colliderObject.gameObject.GetComponent<WoodenBarrel>();

            if(Barrel.X)
                SceneManager.LoadScene("GameOver");

            Barrel.ItHasPlayer = true;
            MyRigidbody.velocity = new Vector2(0, 0);
            DisableGravity();
            isInBarrel = true;
            HidePlayer();
        }
    }

    void DisableGravity()
    {
        if (MyRigidbody.gravityScale != 0)
        {
            _tempGravityScale = MyRigidbody.gravityScale;
            MyRigidbody.gravityScale = 0;
        }
    }

    void EnableGravity()
    {
        if (MyRigidbody.gravityScale == 0)
        {
            MyRigidbody.gravityScale = _tempGravityScale;
        }
    }

    void HidePlayer()
    {
        var render = GetComponent<Renderer>();
        render.enabled = false;
    }

    void ShowPlayer()
    {
        var render = GetComponent<Renderer>();
        render.enabled = true;
    }

    void ExitBarel()
    {
        var rotation = Barrel.transform.rotation.eulerAngles.z;

        var sin = Math.Sin(ConvertToRadians(360 - rotation));
        var cos = Math.Cos(ConvertToRadians(360 - rotation));

        var startX = (((PlayerColider.radius) * transform.lossyScale.x) * sin * 1.1f) + (((BarrelBoxColider.size.y / 2) * Barrel.transform.lossyScale.y) * sin) + BarrelBoxColider.transform.position.x;
        var startY = (((PlayerColider.radius) * transform.lossyScale.y) * cos * 1.1f) + (((BarrelBoxColider.size.y / 2) * Barrel.transform.lossyScale.y) * cos) + BarrelBoxColider.transform.position.y;

        this.transform.position = new Vector2((float)startX, (float)startY);

        MyRigidbody.velocity = new Vector2((float)sin, (float)cos) * Barrel.ExitSpeed;

        BarrelRotatePlayer = Barrel.RotatePlayer;
        BarrelRotatePlayerSpeed = Barrel.RotatePlayerSpeed;
        if (!Pow)
            PowSound.Play();
        else
            JumpSound.Play();

        Barrel.ItHasPlayer = false;
        PlayerColider.enabled = true;
        Pow = true;
        isInBarrel = false;

        EnableGravity();
        ShowPlayer();
    }

    public double ConvertToRadians(double angle)
    {
        return (Math.PI / 180) * angle;
    }
}
