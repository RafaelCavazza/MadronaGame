using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumpForce;

    public bool Pow { get; set; }
    private Rigidbody2D rb2d;
    private float _tempGravityScale = 0;
    private bool isInBarrel = false;
    private Collision2D Barrel;
    private BoxCollider2D BarrelBoxColider;
    private CircleCollider2D PlayerColider;

    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
        PlayerColider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        var key = Input.GetKey(KeyCode.Q);

        if (key && isInBarrel)
        {
            ExitBarel();
        }
    }

    void SetCameraPosition()
    {
        return;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Barrel")
        {
            BarrelBoxColider = coll.gameObject.GetComponent<BoxCollider2D>();
            PlayerColider.enabled = false;
            Barrel = coll;
            transform.position = new Vector3(coll.transform.position.x, coll.transform.position.y, 0);
            rb2d.velocity = new Vector2(0, 0);
            DisableGravity();
            isInBarrel = true;
            Debug.Log("Colidiu");
            HidePlayer();
        }
    }

    void DisableGravity()
    {
        if (rb2d.gravityScale != 0)
        {
            _tempGravityScale = rb2d.gravityScale;
            rb2d.gravityScale = 0;
        }
    }

    void EnableGravity()
    {
        if (rb2d.gravityScale == 0)
        {
            rb2d.gravityScale = _tempGravityScale;
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
        var spriteSize = this.GetComponent<SpriteRenderer>().bounds.size;

        var startX = (((PlayerColider.radius) * this.transform.lossyScale.x) * sin * 1.1f) + (((BarrelBoxColider.size.y/2) * Barrel.transform.lossyScale.y) * sin) + BarrelBoxColider.transform.position.x;
        var startY = (((PlayerColider.radius) * this.transform.lossyScale.y) * cos * 1.1f) + (((BarrelBoxColider.size.y/2) * Barrel.transform.lossyScale.y) * cos) + BarrelBoxColider.transform.position.y;

        Pow = true;
        isInBarrel = false;

        this.transform.position = new Vector2((float)startX, (float)startY);
        rb2d.velocity = new Vector2((float)sin, (float)cos) * jumpForce;

        //Barrel.gameObject.SendMessage("StopRotation");
        PlayerColider.enabled = true;
        EnableGravity();
        ShowPlayer();
    }

    public double ConvertToRadians(double angle)
    {
        return (Math.PI / 180) * angle;
    }
}
