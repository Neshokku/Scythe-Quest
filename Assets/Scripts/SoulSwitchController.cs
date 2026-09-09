using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class SoulSwitchController : MonoBehaviour
{
    [SerializeField] private GameObject eIndicator;
    private SpriteRenderer spriteRenderer;

    public UnityEvent OnSwitchOn;
    public UnityEvent OnSwitchOff;

    public bool isOnRange { get; private set; } = false;
    private bool isOn = false;
    public bool onCooldown { get; private set; }

    [SerializeField] private Sprite spriteOn;
    [SerializeField] private Sprite spriteOff;

    [SerializeField] private GameObject movingSoul;

    [SerializeField] private AudioSource triggerSound;

    private ScytheController scytheController;
    private Transform scytheTransform;

    // Start is called before the first frame update
    void Start()
    {
        scytheController = GameObject.FindGameObjectWithTag("Scythe").GetComponent<ScytheController>();
        scytheTransform = GameObject.FindGameObjectWithTag("Scythe").GetComponent<Transform>();
        eIndicator.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerInteraction") && !onCooldown && 
            ((!isOn && LogicScript.Instance.CanUseSouls()) || 
            (isOn && LogicScript.Instance.CanCollectSouls())))
        {
            eIndicator.SetActive(true);
            isOnRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerInteraction"))
        {
            eIndicator.SetActive(false);
            isOnRange = false;
        }
    }

    public void SwitchTrigger()
    {
        Debug.Log("Succesfully performed");

        GameObject newMovingSoul = Instantiate(movingSoul, transform.position, transform.rotation);
        MovingSoulController soulController = newMovingSoul.GetComponent<MovingSoulController>();

        scytheController.Freeze("SwitchTriggering");

        if (isOn)
        {
            triggerSound.Play();
            spriteRenderer.sprite = spriteOff;
            OnSwitchOff?.Invoke();
            soulController.SetObjective(scytheTransform);
            soulController.onReach.AddListener(SwitchOff);
        }
        else
        {
            LogicScript.Instance.DecreaseCollectedSouls();

            newMovingSoul.transform.position = scytheTransform.position;
            soulController.SetObjective(transform);
            soulController.onReach.AddListener(SwitchOn);
        }
        StartCoroutine(Cooldown());
    }

    private void SwitchOn()
    {
        isOn = true;
        OnSwitchOn?.Invoke();
        spriteRenderer.sprite = spriteOn;
        triggerSound.Play();
        scytheController.UnFreeze("SwitchTriggering");
    }

    private void SwitchOff()
    {
        isOn = false;
        LogicScript.Instance.IncreaseCollectedSouls();
        scytheController.UnFreeze("SwitchTriggering");
    }

    IEnumerator Cooldown()
    {
        eIndicator.SetActive(false);
        onCooldown = true;
        yield return new WaitForSeconds(1f);
        if (isOnRange) eIndicator.SetActive(true);
        onCooldown = false;
    }
}
