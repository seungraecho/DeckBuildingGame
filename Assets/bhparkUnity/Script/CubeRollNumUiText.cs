using TMPro;
using UnityEngine;

public class CubeRollNumText : MonoBehaviour
{
    public TextMeshProUGUI rollNumText;

    public RollDiceButton rollDiceButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rollDiceButton = GetComponentInParent<RollDiceButton>();
        rollNumText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( rollDiceButton.RollNum > -1)
        {
            rollNumText.text = "Turns\t" + rollDiceButton.RollNum.ToString() + "/5";
        }
    }
}
