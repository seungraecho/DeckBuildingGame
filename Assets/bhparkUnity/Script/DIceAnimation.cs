using UnityEngine;

public class DIceAnimation : MonoBehaviour
{
    public Transform playerTransform;
    Transform diceTransform;

    public GamePauseSystem pauseSystem;

    public int myIndex;

    public Outline outline;

    public DiceMachine diceMachine;

    public GameRuleMaster gameRuleMaster;

    public AudioSource DiceShake;
    public AudioSource DicePosition;
    public AudioSource DiceSelected;
    public AudioSource DeniedSound;
    private bool ShakingAudioPlay = false;

    private float _diceTransformRotationX;
    private float _diceTransformRotationY;
    private float _diceTransformRotationZ;

    Vector3 _diceTransformPosition;
    private Vector3[] _diceTransformRotation;

    void Start()
    {
        _diceTransformRotation = new Vector3[6];

        _diceTransformRotation[0] = new Vector3(0, -210, 0);
        _diceTransformRotation[1] = new Vector3(90, -210, 0);
        _diceTransformRotation[2] = new Vector3(90, -120, 0);
        _diceTransformRotation[3] = new Vector3(90, 60, 0);
        _diceTransformRotation[4] = new Vector3(90, -30, 0);
        _diceTransformRotation[5] = new Vector3(0, -30, 0);

        diceTransform = GetComponent<Transform>();
        _diceTransformPosition = diceTransform.position;
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    void Update()
    {
        if (diceMachine.IsRolling)
        {
            DiceRolling();
        }
    }

    private void OnMouseEnter()
    {
        if (!diceMachine.IsRolling && !gameRuleMaster.isAttacking && !pauseSystem.isGamePaused)
            outline.enabled = true;
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
    }

    private void OnMouseDown()
    {
        if (diceMachine.IsRolling || gameRuleMaster.isAttacking || pauseSystem.isGamePaused) return;

        if (diceMachine.IsMouseClickedCount)
        {
            DeniedSound.Play();
            return;
        }

        PlayAudio(2);
        diceMachine.OnDiceClicked(myIndex);
    }

    public void RestartRolling()
    {
        if (ShakingAudioPlay) PlayAudio(0);
        ShakingAudioPlay = true;
    }

    public void SetDiceFinalRotation(int diceValue)
    {
        Debug.Log("SetDiceFinalRotation called with diceValue: " + diceValue);
        Debug.Log("Final Rotation: " + _diceTransformRotation[diceValue - 1]);
        PlayAudio(1);

        diceTransform.rotation = Quaternion.Euler(_diceTransformRotation[diceValue - 1]);
    }

    private void DiceRolling()
    {
        _diceTransformRotationX += Time.deltaTime * 300;
        _diceTransformRotationY += Time.deltaTime * 300;
        _diceTransformRotationZ += Time.deltaTime * 300;

        _diceTransformPosition.x = _diceTransformRotationX;
        _diceTransformPosition.y = _diceTransformRotationY;
        _diceTransformPosition.z = _diceTransformRotationZ;

        diceTransform.rotation = Quaternion.Euler(_diceTransformPosition);
    }

    public void PlayAudio(int Getnum)
    {
        if (Getnum == 0)
        {
            DiceShake.Play();
        }
        else if (Getnum == 1)
        {
            DicePosition.Play();
        }
        else if (Getnum == 2)
        {
            DiceSelected.Play();
        }
    }
}