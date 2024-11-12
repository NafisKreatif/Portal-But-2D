using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    private Transform playerTranform;
    private float rotationDelay = 0.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        playerTranform = GetComponent<Transform>();
        // playerTranform.gameObject.GetComponent<Rigidbody2D>().linearVelocityX = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //if (playerTranform.gameObject.GetComponent<Rigidbody2D>().linearVelocityX == 0) playerTranform.gameObject.GetComponent<Rigidbody2D>().AddForceX(300f);
        if (rotationDelay > 0.0001f) {
            if (playerTranform.localScale.y > 0) { // Player tidak ter-flip
                if (playerTranform.rotation.z % 180 != 0) rotationDelay -= Time.deltaTime;
                else rotationDelay = 0.2f;
            }
            else {
                if ((playerTranform.rotation.z + 180) % 180 != 180) rotationDelay -= Time.deltaTime;
                else rotationDelay = 0.2f;
            }
        }
        else {
            Quaternion currentRotation = playerTranform.rotation;
            Quaternion targetRotation;
            if (playerTranform.localScale.y > 0) { // Player tidak ter-flip
                targetRotation = Quaternion.Euler(0, 0, 0);
            }
            else {
                targetRotation = Quaternion.Euler(0, 0, 180);
            }
            playerTranform.rotation = Quaternion.Slerp(currentRotation, targetRotation, 0.05f);
            if (Mathf.Abs(playerTranform.rotation.z) < 0.0001f) {
                rotationDelay = 0.2f;
                playerTranform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}
