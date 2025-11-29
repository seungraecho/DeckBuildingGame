using TMPro;
using UnityEngine;

public class ShowMonsterHpTextUI : MonoBehaviour
{
    public TextMeshProUGUI monsterHPText;
    public Monster monster1;
    public Monster monster2;
    public Monster monster3;
    public Monster Boss;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        monsterHPText.text = "test";
    }

    // Update is called once per frame
    void Update()
    {
        if (monster1.monsterHP > 0)
        {
            monsterHPText.text = "HP: " + monster1.monsterHP.ToString();
        }
        else if (monster2.monsterHP > 0)
        {
            monsterHPText.text = "HP: " + monster2.monsterHP.ToString();
        }
        else if (monster3.monsterHP > 0)
        {
            monsterHPText.text = "HP: " + monster3.monsterHP.ToString();

        }
        else if (Boss.monsterHP > 0)
        {
            monsterHPText.text = "HP: " + Boss.monsterHP.ToString();
        }
        else
        {
            monsterHPText.text = "All Monsters Defeated!";
        }
    }
}
