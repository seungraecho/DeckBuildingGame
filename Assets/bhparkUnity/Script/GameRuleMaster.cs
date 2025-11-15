using UnityEngine;



public class GameRuleMaster : MonoBehaviour
{


    public GameObject DIceMachine;

    //1. 다이스 정보 가져오기

    //- 다이스 값
    //- 플레이어의 선택한 키워드
    //- 다이스 개수 X 선택 키워드 



    public GameObject ItemManager;

    //2. 아이템 정보 가져오기

    //- 아이템 키워드
    //- 아이템 효과



    public GameObject Monster;

    //3. 몬스터 정보 가져오기

    //- 몬스터 체력
    //- 몬스터 디버프

    //=============



    // 데미지 산출


    DiceMachine diceMachine;

    ItemManager itemManager;

    Monster monster;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        diceMachine = DIceMachine.GetComponentInChildren<DiceMachine>();

        itemManager = ItemManager.GetComponentInChildren<ItemManager>();

        monster = Monster.GetComponentInChildren<Monster>();


        diceMachine.RollDice();
    }

    // Update is called once per frame
    void Update()
    {


        Debug.Log("Game Rule Master is Running" );


        if(Input.GetKeyDown(KeyCode.Space))
        {


            diceMachine.RollDice();

            Debug.Log("Dice Value: " + string.Join(",", diceMachine.diceValue));
            

        }


    }
}


