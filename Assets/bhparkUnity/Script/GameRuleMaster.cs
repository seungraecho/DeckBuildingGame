using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRuleMaster : MonoBehaviour
{ 
    public GameObject DiceMachine;

    public GameObject ItemManager;

    public GameObject Enemy;

    public RollDiceButton diceButton;

    DiceMachine diceMachine;

    ItemManager itemManager;

    public float hitDelay = 1.0f;
    public float runningAnimTime = 7.0f;
    public bool isAttacking = false;
    private int currentStage = 0;
    private bool inBattle = false;
    private float timer = 0f;

    public AniController playerAnimator;
    public Startmenu startmenu;

    public List<DIceAnimation> dIceAnimations;
    public List<Monster> Monsters;

    public AudioSource preAttackAudio;
    public AudioSource attackAudio;
    public AudioSource BGM;
    public AudioSource Victorytriumph;
    public ParticleSystem MagicCircle;
    public ParticleSystem attackEffect;
    public GameObject selectDice;
    public GameObject rerollCube;
    public GameObject turnCube;


    void Start()
    {
        Debug.Log("GameRuleMaster::Start()");
        diceMachine = DiceMachine.GetComponentInChildren<DiceMachine>();
        itemManager = ItemManager.GetComponentInChildren<ItemManager>();
        if (Enemy != null)
        {
            Monsters = new List<Monster>(Enemy.GetComponentsInChildren<Monster>());
        }
        BGM.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!inBattle) isAttacking = playerAnimator.isMoving;
        if (diceButton.noRolls)
        {
            GameEnd();
            diceButton.noRolls = false;
        }

        if (diceMachine.playerSelectDiceCount > 0 && !isAttacking)
        {
            for (int i = 0; i < 3; i++)
            {
                dIceAnimations[i].PlayAudio(2);
            }
            StartCoroutine(PlayerAttack(diceMachine.playerSelectDiceCount));
            diceMachine.playerSelectDiceCount = 0;
        }

        if (diceButton.isInputBlocked) 
            return;
        if (Input.GetMouseButtonDown(0) && turnCube.activeSelf)
        {
            turnCube.SetActive(false);
            playerAnimator.firstTutorial = false;
        }
    }

    public void GameEnd()
    {
        startmenu.Defeat();
    }

    private IEnumerator PlayerAttack(int count)
    {
        inBattle = true;
        if (playerAnimator.firstTutorial)
            selectDice.SetActive(false);
        isAttacking = true;
        playerAnimator.AttackAni();
        MagicCircle.Play();

        float targetVolume = 0.1f;
        timer = 0;
        preAttackAudio.volume = 0f;
        preAttackAudio.Play();

        while (timer < hitDelay)
        {
            timer += Time.deltaTime;
            preAttackAudio.volume = Mathf.Lerp(0f, targetVolume, timer / hitDelay);
            yield return null;
        }

        if (currentStage >= Monsters.Count)
        {
            isAttacking = false;
            yield break;
        }

        preAttackAudio.Stop();
        Monster currentMonster = Monsters[currentStage];
        if (attackEffect != null)
        {
            MagicCircle.Stop();
            attackEffect.Play();
            attackAudio.Play();
        }
        int Damage = count * 10;
        float monsterAnimTime = currentMonster.TakeDamage(Damage);

        yield return new WaitForSeconds(monsterAnimTime);

        if (currentMonster.IsDead())
        {
            currentStage++;
            if (!(currentStage >= Monsters.Count))
            {
                diceButton.RollNum = 5;
                diceMachine.StartRollingState(true);
                
                StartCoroutine(playerAnimator.NextBattle(38.0f));

                yield return new WaitForSeconds(runningAnimTime);
            }
            else
            {
                StartCoroutine(TurnOffBGM());
                yield return new WaitForSeconds(hitDelay);

                Victorytriumph.Play();
                playerAnimator.VictoryAni();
                playerAnimator.jumpAudio.Play();
                yield return new WaitForSeconds(1.0f);
                playerAnimator.kirat.Play();
                yield return new WaitForSeconds(hitDelay * 3.0f);

                StartCoroutine(playerAnimator.NextBattle(38.0f));
                startmenu.StageClear();

                yield break;
            }
        }
        inBattle = false;

        if (diceButton.RollNum - 1 < 0)
        {
            diceButton.noRolls = true;
        }
        
        if (playerAnimator.firstTutorial)
            rerollCube.SetActive(true);

        isAttacking = false;
    }
    public IEnumerator TurnOffBGM()
    {
        float bgmVolume = BGM.volume;
        timer = 0f;
        while (timer < hitDelay)
        {
            timer += Time.deltaTime;
            BGM.volume = Mathf.Lerp(bgmVolume, 0f, timer / hitDelay);

            yield return null;
        }
        BGM.Stop();
    }
}