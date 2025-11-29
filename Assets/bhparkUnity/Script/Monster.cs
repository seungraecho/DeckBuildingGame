using UnityEngine;

public class Monster : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int MaxHP;
    public int monsterHP;
    private IMonsterAnimatable monsterAnimatable;

    void Awake()
    {
        monsterAnimatable = GetComponent<IMonsterAnimatable>();
        MaxHP = monsterHP;
    }

    public float TakeDamage(int damage)
    {
        monsterHP -= damage;
        Debug.Log(gameObject.name + damage + " 데미지를 받음. 남은 HP: " + monsterHP);

        if (monsterHP <= 0)
        {
            monsterHP = 0;
            if (monsterAnimatable != null)
            {
                return monsterAnimatable.DeadAni();
            }
        }
        else
        {
            if (monsterAnimatable != null)
            {
                return monsterAnimatable.DamagedAni();
            }
        }

        return 0f;
    }
    public bool IsDead()
    {
        return monsterHP <= 0;
    }
}
