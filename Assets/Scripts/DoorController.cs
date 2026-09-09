using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject doorCollider;
    [SerializeField] private bool isOpen = false;


    // Start is called before the first frame update
    void Start()
    {
        if (isOpen)
        {
            doorCollider.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Trigger Door")]
    public void TriggerDoor()
    {
        if (isOpen)
        {
            isOpen = false;
            doorCollider.SetActive(true);
            animator.SetTrigger("TriggerDoorClosed");
        } else
        {
            isOpen = true;
            doorCollider.SetActive(false);
            animator.SetTrigger("TriggerDoorOpen");
        }
    }
}
