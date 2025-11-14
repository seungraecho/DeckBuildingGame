using UnityEngine;

public class MinotaurAni : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.O))
        {
            anim.SetTrigger("Damaged");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            anim.SetTrigger("Damaged2");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            anim.SetTrigger("Death");
        }
    }
}
