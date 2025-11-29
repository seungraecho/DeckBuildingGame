// Assets/_Scripts/ShopItemData.cs
using UnityEngine;

// 유니티 에디터의 Create 메뉴에 "Shop/Shop Item Data" 항목을 추가해줘요!
[CreateAssetMenu(fileName = "NewShopItem", menuName = "Shop/Shop Item Data")]
public class ShopItemData : ScriptableObject
{
    [Header("Item Info")]
    public string itemName = "아이템 이름";        // 상점에 표시될 아이템의 이름! (예: 주사위 강화, 리롤 코인)
    public Sprite itemIcon;                       // 상점 UI에 표시될 아이템 이미지 (나중에 이미지 준비되면 넣어줄 거야!)
    public int itemPrice = 100;                   // 이 아이템을 구매하는 데 필요한 골드!
    [TextArea] // 유니티 에디터에서 여러 줄로 아이템 설명을 쓸 수 있게 해줘!
    public string itemDescription = "아이템 설명입니다."; // 아이템 설명

    [Header("Item Effect Info (Dice Game)")]
    // 이 아이템이 네 주사위 게임에서 어떤 종류의 효과를 줄지 결정하는 타입이야!
    public ShopItemEffectType effectType;
    public float effectValue;                     // 효과의 수치 (예: 대미지 증가량 1.5, 리롤 횟수 3 등)
    public int durationTurns;                     // 효과가 몇 턴 동안 지속될지 (예: 공격력 2턴 증가)

    [Header("Purchase Status (RUNTIME - 상점 로직에서 사용)")]
    // 이 변수는 상점 로직에서 '이미 구매된 아이템'을 표시하기 위해 사용될 거야.
    // 플레이어가 이 아이템을 한 번 사면, true로 바뀌어서 다시 구매 못 하게 할 거야!
    public bool isPurchased = false; // 기본은 구매되지 않은 상태!
}

// 아이템 효과의 종류를 정의하는 목록이야. 코드로 관리하기 편하게!
public enum ShopItemEffectType
{
    None,                  // 아무런 특별한 효과 없음 (코스메틱 아이템 등)
    DicePowerUp,           // 주사위 굴림 결과(딜)을 직접적으로 강화 (예: +딜 수치, x배)
    RerollToken,           // 주사위 굴림을 다시 할 수 있는 기회 추가
    TurnExtension,         // 현재 스테이지/전투의 총 턴 수를 늘려줘 (시간 제한 늘리기!)
    TemporaryDamageBoost,  // 일정 턴 동안 딜량이 확 늘어나는 부스트!
    AdditionalDice,        // 주사위를 하나 더 굴릴 수 있게 해줘!
    // 네 게임에 필요한 새로운 효과가 있다면 여기에 언제든지 추가해줄 수 있어!
}