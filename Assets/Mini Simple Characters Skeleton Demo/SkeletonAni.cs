using UnityEngine;

public class SkeletonAni : MonoBehaviour
{
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            anim.SetTrigger("Damaged");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            anim.SetTrigger("Death");
        }
    }
}
