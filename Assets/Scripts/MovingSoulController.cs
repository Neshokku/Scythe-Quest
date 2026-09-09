using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovingSoulController : MonoBehaviour
{
    private Transform objective;
    public UnityEvent onReach;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private AudioSource audioSource;

    private float travelTime = 0.6f;
    [SerializeField] private AnimationCurve fadeCurve;

    void Start()
    {
        audioSource.pitch = Random.Range(0f,2.0f);
        audioSource.Play();
        StartCoroutine(moveToObjective());
    }

    // Update is called once per frame
    void Update()
    {
        

        
    }

    IEnumerator moveToObjective()
    {
        float elapsedTime = 0f;
        Vector3 originalPos = transform.position;

        while (elapsedTime < travelTime)
        {
            elapsedTime += Time.deltaTime;

            transform.position = Vector3.Lerp(originalPos, objective.position, elapsedTime / travelTime);
            var rotateDirection = (objective.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, rotateDirection);
            transform.rotation = targetRotation * Quaternion.Euler(0, 0, 90);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, fadeCurve.Evaluate(elapsedTime / travelTime));
            yield return null;
        }
        onReach.Invoke();
        Destroy(gameObject);
    }

    public void SetObjective(Transform objective)
    {
        this.objective = objective;
    }
}
