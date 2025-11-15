using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;





public class DiceMachine : MonoBehaviour
{

    public List<int> diceValue;

    public int diceNum = 3;

    public int playerSelectDiceValue = 0;

    public int playerSelectDiceCount = 0;



    static private int _maxDiceRandomValue = 6;

    private int _diceListInputValue;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        diceValue = new List<int>();

        for(int i = 0; i < diceNum; i++)
        {
            _diceListInputValue = Random.Range(1, _maxDiceRandomValue + 1);
            diceValue.Add(_diceListInputValue);
        }



        Debug.Log("Dice Values: " + string.Join(", ", diceValue));

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {

            diceValue.Clear();


            for (int i = 0; i < diceNum; i++)
            {

                _diceListInputValue = Random.Range(1, _maxDiceRandomValue + 1);
                diceValue.Add(_diceListInputValue);

            }

            Debug.Log("Dice Values: " + string.Join(", ", diceValue));

        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            Debug.Log("Player selected dice value: ");

            playerSelectDiceCount = GetCountInDiceList(diceValue, 1);

            Debug.Log("Player selected dice value: " + playerSelectDiceCount);

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            playerSelectDiceCount = GetCountInDiceList(diceValue, 2);

            Debug.Log("Player selected dice value: " + playerSelectDiceCount);

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

            playerSelectDiceCount = GetCountInDiceList(diceValue, 3);

            Debug.Log("Player selected dice value: " + playerSelectDiceCount);

        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {

            playerSelectDiceCount = GetCountInDiceList(diceValue, 4);

            Debug.Log("Player selected dice value: " + playerSelectDiceCount);

        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {

            playerSelectDiceCount = GetCountInDiceList(diceValue, 5);

            Debug.Log("Player selected dice value: " + playerSelectDiceCount);

        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {

            playerSelectDiceCount = GetCountInDiceList(diceValue, 6);

            Debug.Log("Player selected dice value: " + playerSelectDiceCount);

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            

        }
    }
    

    public int GetCountInDiceList(List<int> list, int targetNumber)
    {
        int count = 0;

        // 리스트의 모든 요소를 순회
        foreach (int number in list)
        {
            // 요소가 찾는 숫자와 일치하면 카운트 증가
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

        Debug.Log("Roll DIce : " + string.Join(", ", diceValue));
    }

}
