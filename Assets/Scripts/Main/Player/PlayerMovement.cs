using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    float forceAmount = 5f;
    UnityEngine.Vector3 force;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)){
            force.x += -forceAmount;
        }
        else if(Input.GetKeyUp(KeyCode.A)){
            force.x -= -forceAmount;
        }
        if(Input.GetKeyDown(KeyCode.D)){
            force.x += forceAmount;
        }
        else if(Input.GetKeyUp(KeyCode.D)){
            force.x -= forceAmount;
        }

        rb.AddForce(force);

        if(rb.linearVelocityX > 10){
            rb.linearVelocityX = 10;
        }
        else if(rb.linearVelocityX < -10){
            rb.linearVelocityX = -10;
        }
    }
}

// using UnityEngine;

// public class PlayerMovement1 : MonoBehaviour
// {
//     public Rigidbody2D rb;
//     float velocityAmount = 5f;
//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//     }

//     void Update()
//     {
//         if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)){
//             rb.linearVelocityX = 0;
//         }
//         else if(Input.GetKey(KeyCode.D)){
//             rb.linearVelocityX = velocityAmount;
//         }
//         else if(Input.GetKey(KeyCode.A)){
//             rb.linearVelocityX = -velocityAmount;
//         }
//         else{
//             rb.linearVelocityX = 0;
//         }
//     }
// }
