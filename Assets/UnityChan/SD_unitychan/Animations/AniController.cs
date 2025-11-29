using System.Collections;
using UnityEngine;

public class AniController : MonoBehaviour
{
    private Animator anim;
    public AudioSource footstepAudio;
    public AudioSource jumpAudio;
    public AudioSource kirat;
    public float moveSpeed = 5.0f;
    public bool isMoving = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(NextBattle(20.0f));
    }

    public IEnumerator NextBattle(float distance)
    {
        anim.SetBool("NextStage", true);
        isMoving = true;
        if (footstepAudio != null && !footstepAudio.isPlaying)
        {
            footstepAudio.Play();
        }

        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + (Vector3.right * distance);
        float enlapse = 0f;
        float duration = distance / moveSpeed;

        while (enlapse < duration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, (enlapse / duration));

            enlapse += Time.deltaTime;
            yield return null;
        }
        transform.position = endPosition;

        if (footstepAudio != null && footstepAudio.isPlaying)
        {
            footstepAudio.Stop();
        }
        anim.SetBool("NextStage", false);
        isMoving = false;
    }

    public void AttackAni()
    {
        anim.SetTrigger("Attack");
    }

    public void VictoryAni()
    {
        anim.SetTrigger("Victory");

    }
}