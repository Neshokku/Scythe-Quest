using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipColliderExitController : MonoBehaviour
{
    [SerializeField] private TipController parentController;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ScytheTrigger") && parentController.active)
        {
            if (parentController.GetDisabledOnExit())
            {
                Debug.Log("FadingOut");
                GetComponent<Collider2D>().enabled = false;
            }
            parentController.FadeOut();
        }
    }
}
