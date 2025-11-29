using UnityEngine;

public class HealthBoxBlack : MonoBehaviour
{
    public Monster monster;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        monster = GetComponentInParent<Monster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (monster.monsterHP <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
