using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheFloorController : MonoBehaviour
{
    [SerializeField] GameObject scytheCore;

    public bool canJump = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            scytheCore.transform.parent = collision.gameObject.transform;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!canJump && !collision.gameObject.CompareTag("Border") && !collision.gameObject.CompareTag("TipCollider"))
        {
            canJump = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (canJump && !collision.gameObject.CompareTag("Border") && !collision.gameObject.CompareTag("TipCollider"))
        {
            canJump = false;
        }

        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            scytheCore.transform.parent = null;
        }
    }
}
