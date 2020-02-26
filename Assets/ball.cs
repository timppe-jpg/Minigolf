using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour {

    public float speed;
    public float maxSpeed;
    public float minSpeed;
    
    

    private Rigidbody2D rb;
    //private Vector2 position;

    public LineRenderer lineRenderer;
    private bool aiming;
    private Vector2 aimStart;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {

        if (aiming)
        {
            lineRenderer.SetPosition(0, aimStart);
            lineRenderer.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        

        if (Input.GetButtonDown("Jump"))
        {
            Vector3 shootDirection;
            shootDirection = Input.mousePosition;
            shootDirection.z = 0.0f;
            shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
            shootDirection = shootDirection - transform.position;

            Vector2 velocity = new Vector2(shootDirection.x * speed, shootDirection.y * speed);
            while (velocity.magnitude > maxSpeed) velocity *= 0.99f; //Needs fixing


            rb.velocity = velocity;
        }

        if (rb.velocity.magnitude < minSpeed)
        {
            rb.velocity *= 0;
        }
        else
        {
            rb.velocity = rb.velocity * 0.995f;
        }
        
        
    }

    void OnMouseDown()
    {
        aimStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aiming = true;
    }

    private void OnMouseUp()
    {
        aiming = false;
    }
}
