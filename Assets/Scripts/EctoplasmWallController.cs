using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EctoplasmWallController : MonoBehaviour
{
    [Header("Config")]
    [SerializeField, Min(0)] int soulRequirement;
    [SerializeField, Range(-1,1)] int sign;

    [Header("References")]
    [SerializeField] Text textBox;
    [SerializeField] GameObject wallCollider;

    // Start is called before the first frame update
    void Start()
    {
        string finalText = ExtractSign(sign) + soulRequirement.ToString();

        textBox.text = finalText;
    }

    private char ExtractSign(int i)
    {
        if (i == 0)
        {
            return ' ';
        } 
        else if (i > 0)
        {
            return '>';
        }
        else if (i < 0)
        {
            return '<';
        }

        return ' ';
    }

    public void UpdateCollider()
    {
        if ((sign == 0 && LogicScript.Instance.GetCollectedSouls() == soulRequirement) ||
            (sign > 0 && LogicScript.Instance.GetCollectedSouls() >= soulRequirement) ||
            (sign < 0 && LogicScript.Instance.GetCollectedSouls() <= soulRequirement))
        {
            wallCollider.SetActive(false);
        }
        else
        {
            wallCollider.SetActive(true);
        }
    }
}
