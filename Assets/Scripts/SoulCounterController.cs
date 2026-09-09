using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulCounterController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform rectTransform;

    [Header("Config")]
    private List<GameObject> soulHeads = new List<GameObject>();
    [SerializeField] private GameObject soulHead;
    [SerializeField] private float spaceBetweenHeads;
    [SerializeField] private float extraSpace;

    [SerializeField] AudioSource increaseSFX;
    [SerializeField] AudioSource decreaseSFX;

    private int currentHeadNumber;

    void Start()
    {
        SetupCounter();
        UpdateSoulCount();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSoulCount()
    {
        int headNumber = LogicScript.Instance.GetCollectedSouls();
        int i = 0;

        if (headNumber > currentHeadNumber)
        {
            increaseSFX.Play();
        }
        else if (headNumber < currentHeadNumber)
        {
            decreaseSFX.Play();
        }

        foreach (var head in soulHeads)
        {
            i++;
            RawImage sprite = head.GetComponent<RawImage>();

            if (i <= headNumber)
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
            }
            else
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.15f);
            }
        }

        currentHeadNumber = headNumber;
    }

    public void SetupCounter()
    {
        int headNumber = LogicScript.Instance.GetMaxSouls();

        float totalHeadWidth = spaceBetweenHeads * headNumber;
        float totalWidth = totalHeadWidth + extraSpace;
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, totalWidth);

        float startX = ((headNumber - 1) * spaceBetweenHeads) / 2;

        for (int i = 0; i < headNumber; i++)
        {
            GameObject newSoulHead = Instantiate(soulHead, rectTransform);
            RectTransform headRectTransform = newSoulHead.GetComponent<RectTransform>();

            headRectTransform.anchoredPosition = new Vector2((spaceBetweenHeads * i) - startX, 0);
            soulHeads.Add(newSoulHead);
        }
    }
}
