using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    private Transform playerTranform;
    private float rotationDelay = 0.3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTranform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rotationDelay > 0.0001f) {
            rotationDelay -= Time.deltaTime;
        }
        else {
            Quaternion currentRotation = playerTranform.rotation;
            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
            playerTranform.rotation = Quaternion.Slerp(currentRotation, targetRotation, 0.05f);
            if (Mathf.Abs(playerTranform.rotation.z) < 0.0001f) {
                rotationDelay = 0.3f;
                playerTranform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}
