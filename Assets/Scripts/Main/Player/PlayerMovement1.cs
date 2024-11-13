using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    public Rigidbody2D rb;
    float velocityAmount = 10f;
    BoxCollider2D groundCheck;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck").gameObject.GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)){
            rb.linearVelocityX = 0;
        }
        else if(Input.GetKey(KeyCode.D)){
            rb.linearVelocityX = velocityAmount;
        }
        else if(Input.GetKey(KeyCode.A)){
            rb.linearVelocityX = -velocityAmount;
        }
        else{
            rb.linearVelocityX = 0;
            
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            rb.linearVelocityY = 10;
        }
    }
}