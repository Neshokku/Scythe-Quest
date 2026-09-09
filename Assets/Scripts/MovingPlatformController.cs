using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Config")]
    [SerializeField] private Vector3 targetPosition;
    private Vector3 originalPos;
    [SerializeField] private float moveSpeed;
    private bool isMoving = false;

    [Header("Sprites")]
    [SerializeField] private Sprite spriteStill;
    [SerializeField] private Sprite spriteMoving;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            HandleMove();
        }
    }

    private void HandleMove()
    {
        if (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = targetPosition;
            targetPosition = originalPos;
            originalPos = transform.position;
        }
    }

    public void StartMoving()
    {
        isMoving = true;
        spriteRenderer.sprite = spriteMoving;
    }
    
    public void StopMoving()
    {
        isMoving = false;
        spriteRenderer.sprite = spriteStill;
    }
}
