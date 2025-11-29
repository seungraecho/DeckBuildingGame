// Assets/Scripts/Managers/PlayerManager.cs (혹은 원하는 경로)
using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance
    {
        get;
        private set;
    } = new PlayerManager();// 싱글톤 패턴

    public int currentGold = 1000; // 플레이어 소지 골드 (에디터에서 초기값 설정 가능)
    // List<ShopItemData> 대신 List<int> (ItemID) 나 Dictionary<int, int> (ItemID, Count) 등으로 관리하는 것이 더 유연할 수 있음.
    // 여기서는 일단 간편하게 ShopItemData 리스트로 가져감.
    public List<ShopItemData> playerInventory = new List<ShopItemData>(); // 플레이어 인벤토리

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않음
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 있다면 자신을 파괴
        }
    }

    // 골드를 감소시키고 성공 여부 반환 (2번 요구사항 처리)
    public bool TryRemoveGold(int amount)
    {
        if (currentGold >= amount)
        {
            currentGold -= amount;
            Debug.Log($"골드 {amount} 소모, 남은 골드: {currentGold}");
            return true;
        }
        //Debug.Log($"골드 부족! 현재 골드: {currentGold}");
        return false;
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
        Debug.Log($"골드 {amount} 획득, 현재 골드: {currentGold}");
    }

    // 인벤토리에 아이템 추가 (3번 요구사항 처리)
    public void AddItem(ShopItemData item)
    {
        playerInventory.Add(item);
        Debug.Log($"{item.itemName} 획득! 인벤토리: {playerInventory.Count}개");
        // TODO: 여기서 인벤토리 UI 업데이트 이벤트를 발생시키거나,
        // 플레이어 능력치를 바로 증가시키는 로직 등을 추가할 수 있어.
    }
}