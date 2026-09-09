using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipColliderController : MonoBehaviour
{
    [SerializeField] private TipController parentController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ScytheTrigger") && parentController.active)
        {
            if (parentController.GetDisabledOnExit())
            {
                Debug.Log("FadingOut");
                GetComponent<Collider2D>().enabled = false;
            }
            parentController.FadeIn();
        }
    }
}
