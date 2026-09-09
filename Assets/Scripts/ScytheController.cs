using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ScytheController : MonoBehaviour
{
    public Rigidbody2D myRb { get; private set; }
    private PlayerInput playerInput;

    [Header("Component References")]
    [SerializeField] ScytheFloorController floorController;
    [SerializeField] BoxCollider2D lateralColliders;
    [SerializeField] BoxCollider2D collectionRange;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audioSource;

    [Header("Sounds")]
    [SerializeField] AudioClip spawnSound;
    [SerializeField] AudioClip stickSound;

    [Header("Attributes")]
    [SerializeField] private float jumpStrength;
    [SerializeField] private float dashForce;
    [SerializeField] private float speed;
    public bool canMove = true;

    private float movementInput;
    private bool isDashing = false;
    private bool isStuck = false;

    private List<string> freezeSources = new List<string>();

    private GameObject[] soulSwitches = null;

    private void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Start is called before the first frame update
    void Start()
    {
        soulSwitches = GameObject.FindGameObjectsWithTag("SoulSwitch");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0f) movementInput = playerInput.actions["Move"].ReadValue<float>();

        if (transform.position.y < -6)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void FixedUpdate()
    {
        if (!isDashing && canMove) myRb.linearVelocity = new Vector2(movementInput * speed, myRb.linearVelocity.y);

        if (isStuck)
        {
            myRb.gravityScale = 0;
            myRb.linearVelocity = new Vector2(myRb.linearVelocity.x,0f);
        } else
        {
            myRb.gravityScale = 2;
        }

        if (movementInput != 0 && !isDashing && canMove)
        {
            transform.localScale = new Vector3(movementInput, 1, 1);
        }
    }

    public void Jump(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed && Time.timeScale != 0f && canMove)
        {
            if (floorController.canJump)
            {
                myRb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
            }
        }
    }

    public void Interact(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            foreach (var soulSwitch in soulSwitches)
            {
                SoulSwitchController soulSwitchController = soulSwitch.GetComponent<SoulSwitchController>();

                if (soulSwitchController != null && !soulSwitchController.onCooldown && soulSwitchController.isOnRange)
                {
                    soulSwitchController.SwitchTrigger();
                    break;
                }
                
            }
        }
    }

    IEnumerator JumpBack()
    {
        float dashDirection = Mathf.Sign(transform.localScale.x);
        isDashing = true;
        myRb.AddForce(Vector2.right * dashDirection * dashForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        isDashing = false;
    }

    public void Stick(Transform collTransform)
    {
        isStuck = true;
        audioSource.PlayOneShot(stickSound);
        animator.speed = 0f;
        floorController.gameObject.SetActive(false);
        gameObject.transform.parent = collTransform;
    }

    public void UnStick()
    {
        isStuck = false;
        animator.speed = 1f;
        floorController.gameObject.SetActive(true);
        gameObject.transform.parent = null;
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
        collectionRange.enabled = canMove;
    }

    public void Freeze(string source)
    {
        if (freezeSources.Count == 0)
        {
            SetCanMove(false);
            myRb.linearVelocity = Vector2.zero;
        }

        if (!freezeSources.Contains(source))
        {
            freezeSources.Add(source);
        }
        else
        {
            Debug.LogError("Error: Freeze Source already in place.");
        }
    }

    public void UnFreeze(string source)
    {
        if (freezeSources.Contains(source))
        {
            freezeSources.Remove(source);
        }
        else
        {
            Debug.LogError("Error: Freeze Source not found");
        }

        if (freezeSources.Count == 0)
        {
            SetCanMove(true);
        }
    }

    public void PlaySpawnSound()
    {
        audioSource.PlayOneShot(spawnSound);
    }
}
