using System.Collections;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    public Transform otherPortal;
    private Transform thisPortal;
    private Collider2D col2d;
    public bool canTeleport = true;
    void Start() {
        thisPortal = GetComponent<Transform>();
        col2d = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (!canTeleport) return;
        Transform otherTransfrom = other.gameObject.transform;
        Rigidbody2D otherBody = other.gameObject.GetComponent<Rigidbody2D>();
        Vector2 vAwal = new (otherBody.linearVelocityX, otherBody.linearVelocityY);
        float sudut = (thisPortal.eulerAngles.z - otherPortal.eulerAngles.z) * Mathf.PI / 180;
        float xAkhir, yAkhir;
        if ((thisPortal.eulerAngles.z + 180) % 180 == 0) {
            xAkhir = otherPortal.position.x + (otherTransfrom.position.x - thisPortal.position.x) * Mathf.Abs(Mathf.Cos(sudut)) + (otherTransfrom.position.y - thisPortal.position.y) * Mathf.Sin(sudut);
            yAkhir = otherPortal.position.y + (otherTransfrom.position.x - thisPortal.position.x) * Mathf.Sin(sudut) + (otherTransfrom.position.y - thisPortal.position.y) * Mathf.Cos(sudut);
        }
        else {
            xAkhir = otherPortal.position.x + (otherTransfrom.position.x - thisPortal.position.x) * Mathf.Cos(sudut) + (otherTransfrom.position.y - thisPortal.position.y) * Mathf.Sin(sudut);
            yAkhir = otherPortal.position.y + (otherTransfrom.position.x - thisPortal.position.x) * Mathf.Sin(sudut) + (otherTransfrom.position.y - thisPortal.position.y) * Mathf.Abs(Mathf.Cos(sudut));
        }
        otherTransfrom.Rotate(new Vector3(0, 0, thisPortal.eulerAngles.z - otherPortal.eulerAngles.z + 180));
        otherTransfrom.position = new Vector3(xAkhir, yAkhir, otherTransfrom.position.z);
        otherBody.linearVelocityY = vAwal.y * -Mathf.Cos(sudut) + vAwal.x * -Mathf.Sin(sudut);
        otherBody.linearVelocityX = vAwal.x * -Mathf.Cos(sudut) + vAwal.y * -Mathf.Sin(sudut);
        canTeleport = false;
        otherPortal.gameObject.GetComponent<PortalTeleporter>().canTeleport = false;
    }

    void OnTriggerExit2D(Collider2D other) {
        if (!canTeleport) {
            StartCoroutine(CanTeleportIn(0.1f));
        }
    }
    IEnumerator CanTeleportIn(float detik) {
        yield return new WaitForSeconds(detik);
        canTeleport = true;
        otherPortal.gameObject.GetComponent<PortalTeleporter>().canTeleport = true;
    }
}