using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelSelectScreen;
    [SerializeField] private GameObject credits;

    [SerializeField] private Animator animator;

    [SerializeField] private AudioSource clickButton;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        levelSelectScreen.SetActive(false);
        credits.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenLevelSelection()
    {
        clickButton.Play();
        mainMenu.SetActive(false);
        levelSelectScreen.SetActive(true);
    }
    
    public void CloseLevelSelection()
    {
        clickButton.Play();
        mainMenu.SetActive(true);
        levelSelectScreen.SetActive(false);
    }

    public void ClickPlay()
    {
        clickButton.Play();
        StartCoroutine(ClickPlayFadeOut());
    }

    IEnumerator ClickPlayFadeOut()
    {
        animator.SetTrigger("TriggerFadeOut");
        yield return new WaitForSeconds(2f);
        LevelManager.Instance.LoadCurrentLevel();
    }

    public void EnterLevel(int id)
    {
        clickButton.Play();
        StartCoroutine(EnterLevelFadeOut(id));
    }

    IEnumerator EnterLevelFadeOut(int id)
    {
        animator.SetTrigger("TriggerFadeOut");
        yield return new WaitForSeconds(2f);
        LevelManager.Instance.LoadLevel(id);
    }

    public void OpenCredits()
    {
        clickButton.Play();
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }

    public void CloseCredits()
    {
        clickButton.Play();
        mainMenu.SetActive(true);
        credits.SetActive(false);
    }
}
