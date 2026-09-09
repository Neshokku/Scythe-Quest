using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public static LogicScript Instance { get; private set; }

    [SerializeField, Min(0)] private int maxCollectedSouls = 3;
    [SerializeField, Min(0)] private int collectedSouls = 0;

    [SerializeField] private UnityEvent CollectedSoulsChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        CollectedSoulsChanged?.Invoke();
    }

    public bool CanCollectSouls()
    {
        if (collectedSouls < maxCollectedSouls)
        {
            return true;
        }

        return false;
    }

    public bool CanUseSouls()
    {
        if (collectedSouls > 0)
        {
            return true;
        }

        return false;
    }

    public void IncreaseCollectedSouls()
    {
        collectedSouls++;
        CollectedSoulsChanged?.Invoke();
    }

    public void DecreaseCollectedSouls()
    {
        collectedSouls--;
        CollectedSoulsChanged?.Invoke();
    }

    public int GetCollectedSouls()
    {
        return collectedSouls;
    }

    public int GetMaxSouls()
    {
        return maxCollectedSouls;
    }

    public void ResetLogic()
    {
        collectedSouls = 0;
    }
}
