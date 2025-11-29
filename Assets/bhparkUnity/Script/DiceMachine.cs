using System.Collections.Generic;
using UnityEngine;

public class DiceMachine : MonoBehaviour
{
    public List<DIceAnimation> diceAnimations;

    public Transform playerTransform;

    public RollDiceButton diceButton;

    public List<int> diceValue;
    public int diceNum = 3;
    public int playerSelectDiceCount = 0;

    static private int _maxDiceRandomValue = 6;
    private int _diceListInputValue;

    public bool IsRolling { get; set; } = true;
    public bool IsMouseClickedCount { get; set; } = false;

    void Start()
    {
        diceValue = new List<int>();
        StartRollingState(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleDiceState();
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            playerSelectDiceCount = 300;
            Debug.Log("Player selected dice value: " + playerSelectDiceCount);
        }

        for (int i = 0; i < diceAnimations.Count; i++)
        {
            if (playerTransform == null) return;

            float xOffset = i * 3.0f;
            Vector3 targetPos = playerTransform.position + new Vector3(xOffset, 5f, 2f);

            diceAnimations[i].transform.position = targetPos;
        }
    }

    public void ToggleDiceState()
    {
        if (IsRolling)
        {
            StopRollingState();
        }
        else
        {
            StartRollingState(false);
        }
    }

    public void StartRollingState(bool FirstRoll)
    {
        if (diceButton.noRolls) return;
        if (!FirstRoll)
        {
            diceButton.RollNum -= 1;
            if (diceButton.RollNum < 0)
            {
                diceButton.noRolls = true;
                return;
            }
        }
        IsRolling = true;
        IsMouseClickedCount = false;
        playerSelectDiceCount = 0;

        foreach (var anim in diceAnimations)
        {
            anim.RestartRolling();
        }
    }

    public void StopRollingState()
    {
        RollDice(); 
        IsRolling = false;

        for (int i = 0; i < diceNum; i++)
        {
            if (i < diceAnimations.Count)
            {
                diceAnimations[i].SetDiceFinalRotation(diceValue[i]);
            }
        }
    }

    public void OnDiceClicked(int index)
    {
        if (IsMouseClickedCount || diceButton.noRolls)
        {
            return;
        }

        if (index >= 0 && index < diceValue.Count)
        {
            int selectedValue = diceValue[index];
            playerSelectDiceCount = GetCountInDiceList(diceValue, selectedValue);

            IsMouseClickedCount = true;
            Debug.Log("Player selected dice value: " + playerSelectDiceCount);
        }
    }

    public int GetCountInDiceList(List<int> list, int targetNumber)
    {
        int count = 0;
        foreach (int number in list)
        {
            if (number == targetNumber)
            {
                count++;
            }
        }
        return count;
    }

    public void RollDice()
    {
        diceValue.Clear();
        for (int i = 0; i < diceNum; i++)
        {
            _diceListInputValue = Random.Range(1, _maxDiceRandomValue + 1);
            diceValue.Add(_diceListInputValue);
        }
        Debug.Log("Rolled Values: " + string.Join(", ", diceValue));
    }
}