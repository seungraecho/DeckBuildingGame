// Assets/_Scripts/PlayerInventoryManager.cs
using UnityEngine;
using System.Collections.Generic; // List(리스트)를 사용하기 위해 필요해!

public class PlayerInventoryManager : MonoBehaviour
{
    // 🧙‍♂️ 마법 주문! 이 친구가 게임 전체에서 딱 한 명의 "인벤토리 관리자"로 존재하게 해줄 거야!
    // 다른 스크립트에서 PlayerInventoryManager.Instance.currentGold 로 네 돈을 바로 볼 수 있어!
    public static PlayerInventoryManager Instance { get; private set; }

    [Header("내 현재 재산!")]
    [SerializeField] private int _currentGold = 1000; // 💰 네가 가진 현재 골드! 초기값은 1000골드부터 시작해볼까?
    public int currentGold => _currentGold; // 다른 곳에서 이 골드 값을 '읽기만' 할 수 있도록!

    [Header("내가 구매한 아이템들!")]
    // 네가 상점에서 구매한 ShopItemData들을 여기에 쭉 저장할 거야.
    public List<ShopItemData> playerInventory = new List<ShopItemData>();

    void Awake()
    {
        // 씬이 바뀌어도 나(PlayerInventoryManager)는 절대 없어지지 않아!
        // 게임이 시작되면 딱 한 번만 생겨나서 게임 끝날 때까지 네 인벤토리를 관리해줄 거야.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 파괴되지 않음! 마법처럼!
        }
        else
        {
            // 어? 이미 인벤토리 관리자가 있네? 그럼 새로 만들어진 나는 필요 없으니까 사라져야지!
            Destroy(gameObject);
        }
    }

    // 💰 돈을 쓰는 기능! (아이템을 살 때 쓸 거야!)
    // 'amount'만큼 돈을 쓸 수 있는지 확인하고, 가능하면 돈을 줄이고 true를 돌려줄 거야.
    // 돈이 부족하면 아무것도 안 하고 false를 돌려줘!
    public bool TryRemoveGold(int amount)
    {
        if (_currentGold >= amount)
        {
            _currentGold -= amount;
            Debug.Log($"골드 {amount} 지출! 남은 돈: {_currentGold} 골드.");
            // TODO: 돈이 줄어들었다고 UI에 표시해주는 코드 같은 것을 나중에 추가할 수 있어!
            return true;
        }
        Debug.Log($"헉! 골드가 부족해! 현재 {_currentGold} 골드밖에 없어. 더 벌어야겠는데?");
        return false;
    }

    // 💰 돈을 버는 기능!
    public void AddGold(int amount)
    {
        _currentGold += amount;
        Debug.Log($"골드 {amount} 획득! 현재 {_currentGold} 골드.");
        // TODO: 돈이 늘어났다고 UI에 표시해주는 코드를 나중에 추가할 수 있어!
    }

    // 📦 아이템을 인벤토리에 추가하는 기능!
    public void AddItemToInventory(ShopItemData item)
    {
        if (!playerInventory.Contains(item)) // 똑같은 아이템이 인벤토리에 없다면 (한정 판매 아이템 같은 경우!)
        {
            playerInventory.Add(item);
            // 아이템 데이터 자체에 구매됨 표시를 남겨두자! 상점에서 재구매 막을 때 유용할 거야.
            item.isPurchased = true;
            Debug.Log($"새 아이템 '{item.itemName}' 획득! 인벤토리 아이템 총 {playerInventory.Count}개.");
            // TODO: 아이템을 얻었다고 UI나 메시지로 알려주는 코드를 나중에 추가할 수 있어!
        }
        else
        {
            Debug.Log($"이미 '{item.itemName}'을(를) 가지고 있어서 또 받을 순 없어!");
        }
    }

    // 📦 내가 이 아이템을 이미 가지고 있는지 확인하는 기능!
    public bool HasItemInInventory(ShopItemData item)
    {
        return playerInventory.Contains(item);
    }
}