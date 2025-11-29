// Assets/Scripts/Shop.cs
using UnityEngine;
using System.Collections.Generic;
using TMPro; // TMP_Text를 사용할 경우
using UnityEngine.UI; // Button, Image 등을 사용할 경우

public class Shop : MonoBehaviour
{
    [Header("Shop Settings")]
    [SerializeField] private List<ShopItemData> availableShopItems;
    [SerializeField] private Transform shopSlotUIParent;
    public ShopSlotUI shopSlotUIPrefab;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI playerGoldText; // 플레이어 골드를 표시할 TMP_Text
    // TODO: 구매 성공/실패 메시지 팝업 등을 위한 UI 요소도 필요하면 추가!

    private List<ShopSlotUI> instancedShopSlots = new List<ShopSlotUI>(); // 생성된 ShopSlotUI 인스턴스들을 저장

    void Start()
    {
        InitializeShop();
        UpdatePlayerGoldUI();
    }

    void OnEnable() // 상점 씬이 활성화될 때마다 UI 업데이트
    {
        // 씬 전환이 아니라 단순히 오브젝트 On/Off로 상점 열고 닫을 때 유용
        RefreshShopUI();
        UpdatePlayerGoldUI();
    }

    private void InitializeShop()
    {
        // 상점이 처음 로드될 때 모든 아이템 슬롯을 생성
        // 만약 UI에 이미 슬롯들이 배치되어 있다면 이 부분은 건너뛰고 바로 UpdateItemSlotsFromData를 호출해도 됨.
        // 현재는 동적으로 생성하는 방식으로 가정

        int i = 0;
        foreach (ShopItemData itemData in availableShopItems)
        {
            ShopSlotUI newSlot = Instantiate(shopSlotUIPrefab, shopSlotUIParent);
            RectTransform slotRectTransform = newSlot.GetComponent<RectTransform>();
            Vector2 originalPosition = slotRectTransform.anchoredPosition;
            originalPosition.x += 250 * i++;
            slotRectTransform.anchoredPosition = originalPosition;
            instancedShopSlots.Add(newSlot);
            newSlot.Setup(itemData, this); // 각 슬롯에 아이템 데이터와 ShopManager 자신을 전달
        }

        RefreshShopUI(); // 초기화 후 바로 UI 상태 새로고침
        UpdatePlayerGoldUI(); // 골드 텍스트도 항상 최신화
    }

    // 상점 UI 상태를 새로고침하는 함수 (골드 변화, 아이템 구매 후 등)
    public void RefreshShopUI()
    {
        foreach (ShopSlotUI slot in instancedShopSlots)
        {
            List<ShopItemData> ShopItemList = PlayerManager.Instance.playerInventory;
            if (slot.CurrentItemData != null)
            {
                // 1. 이미 구매한 아이템인지 확인 (재구매 방지)
                bool isOwned = PlayerManager.Instance.playerInventory.Contains(slot.CurrentItemData);
                slot.SetPurchasedState(isOwned); // 슬롯 UI가 구매 여부를 표시하도록 지시

                // 2. 골드 부족 여부 확인
                bool canAfford = PlayerManager.Instance.currentGold >= slot.CurrentItemData.itemPrice;
                slot.SetAffordableState(canAfford); // 슬롯 UI가 골드 여부를 표시하도록 지시
            }
        }
    }


    // 플레이어의 골드 UI를 업데이트
    public void UpdatePlayerGoldUI()
    {
        if (playerGoldText != null && PlayerManager.Instance != null)
        {
            playerGoldText.text = $"GOLD: {PlayerManager.Instance.currentGold}";
        }
    }

    // ShopSlotUI에서 구매 버튼을 눌렀을 때 호출되는 함수
    public void PurchaseItem(ShopItemData itemToBuy)
    {

        if (PlayerManager.Instance == null)
        {
            Debug.LogError("PlayerManager.Instance is null! Is it setup correctly in the first scene?");
            return;
        }

        // 1. 이미 구매한 아이템인지 다시 확인 (방어 로직)
        if (PlayerManager.Instance.playerInventory.Contains(itemToBuy))
        {
            Debug.Log($"이미 {itemToBuy.itemName}을(를) 가지고 있습니다!");
            // TODO: "이미 소유한 아이템입니다" 팝업 띄우기
            return;
        }

        // 2. 골드가 충분한지 확인 (부족하면 구매 불가능)
        if (PlayerManager.Instance.TryRemoveGold(itemToBuy.itemPrice)) // TryRemoveGold는 PlayerManager에 있는 함수!
        {
            // 3. 아이템 구매 및 적용
            PlayerManager.Instance.AddItem(itemToBuy); // PlayerManager에 아이템 추가
            Debug.Log($"{itemToBuy.itemName} 구매 성공!");
            // TODO: 구매 성공 효과 (사운드, 파티클, 메시지 등)

            RefreshShopUI(); // 상점 UI 상태 새로고침 (구매한 아이템 비활성화 및 골드 업데이트)
        }
        else
        {
            Debug.Log($"{itemToBuy.itemName} 구매 실패: 골드가 부족합니다!");
            // TODO: "골드가 부족합니다" 팝업 띄우기
        }
    }
}