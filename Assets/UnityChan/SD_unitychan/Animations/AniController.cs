using System.Collections;
using TMPro;
using UnityEngine;

public class AniController : MonoBehaviour
{
    private Animator anim;
    public float moveSpeed = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(NextBattle(38.0f));
        }
        if ( Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetTrigger("Damaged");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            anim.SetTrigger("Attack");
        }
    }
    private IEnumerator NextBattle(float distance)
    {
        anim.SetBool("NextStage", true);

        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + (Vector3.right * distance);
        float duration = distance / moveSpeed;
        float enlapse = 0f;

        while (enlapse < duration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, (enlapse / duration));

            enlapse += Time.deltaTime;
            yield return null;
        }
        transform.position = endPosition;
        anim.SetBool("NextStage", false);
    }
}
