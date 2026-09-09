using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheStickController : MonoBehaviour
{
    [SerializeField] ScytheController parentController;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stickable") || collision.gameObject.CompareTag("MovingPlatform"))
        {
            parentController.Stick(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stickable") || collision.gameObject.CompareTag("MovingPlatform"))
        {
            parentController.UnStick();
        }
    }
}
