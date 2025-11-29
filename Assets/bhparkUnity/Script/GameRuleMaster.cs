using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

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

    public AniController playerAnimator;

    public Startmenu startmenu;

    public List<DIceAnimation> dIceAnimations;
    public List<Monster> Monsters;

    public AudioSource preAttackAudio;
    public AudioSource attackAudio;
    public AudioSource BGM;
    public AudioSource Victorytriumph;
    public ParticleSystem attackEffect;

    public TextMeshProUGUI defeatText;
    public TextMeshProUGUI winText;

    private float _time0 = 0;
    private float _time1 = 0;

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
        Debug.Log("Game Rule Master is Running");


        defeatText.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (diceMachine.playerSelectDiceCount > 0 && !isAttacking)
        {
            for (int i = 0; i < 3; i++)
            {
                dIceAnimations[i].PlayAudio(2);
            }
            StartCoroutine(PlayerAttack(diceMachine.playerSelectDiceCount));
            diceMachine.playerSelectDiceCount = 0;
        }


        //// If Roll Dice RollNum is 0 . Defeat.
        if (diceButton.RollNum == -1)
        {

            defeatText.gameObject.SetActive(true);

            _time0 += Time.deltaTime;

            if (_time0 > 2.0f)
            {

                SceneManager.LoadScene("StartMenu");
            }


        }

        Debug.Log("currentStage: " + currentStage);
        Debug.Log("diceButton.RollNum: " + diceButton.RollNum);

        if (currentStage == 4)
        {
            winText.gameObject.SetActive(true);

            _time0 += Time.deltaTime;

            if (_time0 > 10.0f)
            {

                SceneManager.LoadScene("StartMenu");
            }

        }
    }

    private IEnumerator PlayerAttack(int count)
    {
        isAttacking = true;
        playerAnimator.AttackAni();

        float targetVolume = 0.1f;
        float timer = 0f;
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
                diceMachine.StartRollingState();


                StartCoroutine(playerAnimator.NextBattle(38.0f));

                yield return new WaitForSeconds(runningAnimTime);
            }
            else
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

                Victorytriumph.Play();
                playerAnimator.VictoryAni();
                playerAnimator.jumpAudio.Play();
                yield return new WaitForSeconds(1.0f);
                playerAnimator.kirat.Play();
                yield return new WaitForSeconds(hitDelay * 3);

                StartCoroutine(playerAnimator.NextBattle(38.0f));
                startmenu.StageClear();

                yield break;
            }
        }

        isAttacking = false;
    }
}