using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulController : MonoBehaviour
{
    private bool isCollected = false;
    [SerializeField] GameObject movingSoul;

    private Transform scytheTransform;

    // Start is called before the first frame update
    void Start()
    {
        scytheTransform = GameObject.FindGameObjectWithTag("Scythe").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ScytheCollectionRange") && LogicScript.Instance.CanCollectSouls() && !isCollected)
        {
            isCollected = true;

            var newSoul = Instantiate(movingSoul, transform.position, transform.rotation);
            var newSoulController = newSoul.GetComponent<MovingSoulController>();
            newSoulController.SetObjective(scytheTransform);
            newSoulController.onReach.AddListener(LogicScript.Instance.IncreaseCollectedSouls);
            RemoveSoul();
        }
    }

    private void RemoveSoul()
    {
        Destroy(gameObject);
    }
}
