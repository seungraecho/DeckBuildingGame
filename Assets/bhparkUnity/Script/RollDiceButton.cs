using System.Collections;
using UnityEngine;

public class RollDiceButton : MonoBehaviour
{
    public Outline outline;

    public GameRuleMaster ruleMaster;

    public DiceMachine diceMachine;

    public GamePauseSystem pauseSystem;

    public Transform playerTransform;

    public Transform rollcubeTransform;

    public GameObject selectDice;
    public GameObject turnCube;
    public GameObject rerollCube;
    public GameObject decisionCube;

    public int RollNum = 5;
    public bool noRolls = false;
    private int firstTutorial = 1;
    public bool isInputBlocked = false;

    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;

        rollcubeTransform = GetComponent<Transform>();

        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                playerTransform = playerObj.transform;
        }
    }

    void Update()
    {
        if (playerTransform != null)
            rollcubeTransform.position = playerTransform.position + new Vector3(3f, 7f, 0f);
        if (pauseSystem.isGamePaused)
            outline.enabled = false;
    }

    void OnMouseDown()
    {
        if (!ruleMaster.isAttacking && !pauseSystem.isGamePaused)
        {
            if (firstTutorial == 1)
            {
                decisionCube.SetActive(false);
                selectDice.SetActive(true);
                firstTutorial += 1;
            }
            else if (firstTutorial == 2)
            {
                selectDice.SetActive(false);
                rerollCube.SetActive(false);
                turnCube.SetActive(true);
                StartCoroutine(BlockInputRoutine());
                firstTutorial = 0;
            }
            if (diceMachine.IsRolling || RollNum > -1)
            {
                Debug.Log("Rolling is Start, RollNUM : " + RollNum);
                diceMachine.ToggleDiceState();
            }
            else
            {
                noRolls = true;
                Debug.Log("No rolls left!");
            }
        }
    }

    private void OnMouseEnter()
    {
        outline.enabled = true;
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
    }
    private IEnumerator BlockInputRoutine()
    {
        isInputBlocked = true;
        yield return null;
        isInputBlocked = false;
    }
}