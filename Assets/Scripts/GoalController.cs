using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject cam;
    [SerializeField] private Text alert;

    [SerializeField] private ScytheController scytheController;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip appearSound;

    private bool appeared = false;

    // Start is called before the first frame update
    void Start()
    {
        if (scytheController == null) scytheController = GameObject.FindGameObjectWithTag("Scythe").GetComponent<ScytheController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ScytheTrigger") && appeared)
        {
            if (!LogicScript.Instance.CanCollectSouls())
            {
                LevelManager.Instance.LoadNextLevel();
            }
            else
            {
                StartCoroutine(AlertFade(alert.color, new Color(alert.color.r, alert.color.g, alert.color.b, 1f)));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ScytheTrigger") && appeared)
        {
            StartCoroutine(AlertFade(alert.color, new Color(alert.color.r, alert.color.g, alert.color.b, 0f)));
        }
    }

    public void AppearConfirm()
    {
        if (LogicScript.Instance.GetCollectedSouls() == LogicScript.Instance.GetMaxSouls() && !appeared)
        {
            appeared = true;
            StartCoroutine(Appear());
        }
    }

    IEnumerator Appear()
    {
        scytheController.Freeze("GoalAppearing");
        cam.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        animator.SetTrigger("TriggerAppear");
        audioSource.PlayOneShot(appearSound);
        yield return new WaitForSeconds(1f);
        cam.SetActive(false);
        scytheController.UnFreeze("GoalAppearing");
    }

    IEnumerator AlertFade(Color from, Color to)
    {
        float fadeTime = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;

            alert.color = Color.Lerp(from, to, elapsedTime / fadeTime);

            yield return null;
        }

        alert.color = to;
    }
}
