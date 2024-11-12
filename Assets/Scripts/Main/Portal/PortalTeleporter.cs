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
        float sudut = (otherPortal.eulerAngles.z - thisPortal.eulerAngles.z) * Mathf.PI / 180;
        float xAkhir, yAkhir;
        if (thisPortal.eulerAngles.z == otherPortal.eulerAngles.z) { // masuk dan keluar berbalik arah
            if ((thisPortal.eulerAngles.z + 180) % 180 == 0) { // atas bawah
                otherTransfrom.localScale = new Vector3(otherTransfrom.localScale.x, -otherTransfrom.localScale.y, otherTransfrom.localScale.z);
                xAkhir = otherPortal.position.x + (otherTransfrom.position.x - thisPortal.position.x) * Mathf.Abs(Mathf.Cos(sudut));
                yAkhir = otherPortal.position.y + (otherTransfrom.position.y - thisPortal.position.y) * -Mathf.Cos(sudut);
            }
            else { // kanan kiri
                otherTransfrom.localScale = new Vector3(-otherTransfrom.localScale.x, otherTransfrom.localScale.y, otherTransfrom.localScale.z);
                xAkhir = otherPortal.position.x + (otherTransfrom.position.x - thisPortal.position.x) * -Mathf.Cos(sudut);
                yAkhir = otherPortal.position.y + (otherTransfrom.position.y - thisPortal.position.y) * Mathf.Abs(Mathf.Cos(sudut));
            }
        }
        else if ((thisPortal.eulerAngles.z - otherPortal.eulerAngles.z + 180) % 180 == 0) { // masuk dan keluar sama arah
            if ((thisPortal.eulerAngles.z + 180) % 180 == 0) { // atas bawah
                xAkhir = otherPortal.position.x + (otherTransfrom.position.x - thisPortal.position.x) * Mathf.Abs(Mathf.Cos(sudut));
                yAkhir = otherPortal.position.y + (otherTransfrom.position.y - thisPortal.position.y) * -Mathf.Cos(sudut);
            }
            else { // kanan kiri
                xAkhir = otherPortal.position.x + (otherTransfrom.position.x - thisPortal.position.x) * -Mathf.Cos(sudut);
                yAkhir = otherPortal.position.y + (otherTransfrom.position.y - thisPortal.position.y) * Mathf.Abs(Mathf.Cos(sudut));
            }
        }
        else if ((thisPortal.eulerAngles.z + 180) % 180 == 0) { // dari atas bawah
            otherTransfrom.Rotate(new Vector3(0, 0, otherPortal.eulerAngles.z - thisPortal.eulerAngles.z + 180));
            xAkhir = otherPortal.position.x + (otherTransfrom.position.x - thisPortal.position.x) * -Mathf.Cos(sudut) + (otherTransfrom.position.y - thisPortal.position.y) * Mathf.Sin(sudut);
            yAkhir = otherPortal.position.y + (otherTransfrom.position.x - thisPortal.position.x) * -Mathf.Sin(sudut) + (otherTransfrom.position.y - thisPortal.position.y) * -Mathf.Cos(sudut);
        }
        else { // dari kanan kiri
            otherTransfrom.Rotate(new Vector3(0, 0, otherPortal.eulerAngles.z - thisPortal.eulerAngles.z + 180));
            xAkhir = otherPortal.position.x + (otherTransfrom.position.x - thisPortal.position.x) * -Mathf.Cos(sudut) + (otherTransfrom.position.y - thisPortal.position.y) * Mathf.Sin(sudut);
            yAkhir = otherPortal.position.y + (otherTransfrom.position.x - thisPortal.position.x) * -Mathf.Sin(sudut) + (otherTransfrom.position.y - thisPortal.position.y) * -Mathf.Cos(sudut);
        }
        otherTransfrom.position = new Vector3(xAkhir, yAkhir, otherTransfrom.position.z);
        otherBody.linearVelocityY = vAwal.y * -Mathf.Cos(sudut) + vAwal.x * -Mathf.Sin(sudut);
        otherBody.linearVelocityX = vAwal.x * -Mathf.Cos(sudut) + vAwal.y * Mathf.Sin(sudut);
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