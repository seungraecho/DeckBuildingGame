// Assets/Scripts/ShopSlotUI.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TMP_Text를 사용할 경우

public class ShopSlotUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image itemIconImage;            // 아이템 아이콘
    [SerializeField] private TextMeshProUGUI itemNameText;   // 아이템 이름
    [SerializeField] private TextMeshProUGUI itemPriceText;  // 아이템 가격
    [SerializeField] private Button buyButton;               // 구매 버튼
    [SerializeField] private GameObject purchasedOverlay;    // 구매 완료 시 표시될 오버레이 (e.g. "SOLD OUT" 텍스트, 반투명 패널)
    [SerializeField] private Image buyButtonBackground;      // 구매 버튼 배경 이미지 (색상 변경용)

    [Header("Button Colors")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color disabledColor = Color.gray;
    [SerializeField] private Color notAffordableColor = Color.red;


    private ShopItemData _currentItemData;
    public ShopItemData CurrentItemData => _currentItemData; // 다른 스크립트에서 현재 아이템 데이터를 가져갈 수 있게 함

    private Shop shopManager; // Shop 스크립트 참조

    // Shop 스크립트에서 이 슬롯을 초기화할 때 호출
    public void Setup(ShopItemData data, Shop manager)
    {
        _currentItemData = data;
        shopManager = manager;

        itemIconImage.sprite = data.itemIcon;
        itemNameText.text = data.itemName;
        itemPriceText.text = data.itemPrice.ToString();

        // 버튼 클릭 리스너 연결
        buyButton.onClick.RemoveAllListeners(); // 기존 리스너 제거 (재활용 시 중요)
        buyButton.onClick.AddListener(OnBuyButtonClicked);

        // 초기 상태 설정
        if (purchasedOverlay != null)
        {
            purchasedOverlay.SetActive(false); // 일단 비활성화
        }
        SetPurchasedState(false); // 처음에는 구매 안 된 상태로 시작
        SetAffordableState(true); // 처음에는 구매 가능 상태로 시작
    }

    // 구매 버튼이 클릭되었을 때 호출
    private void OnBuyButtonClicked()
    {
        if (shopManager != null && _currentItemData != null)
        {
            shopManager.PurchaseItem(_currentItemData); // Shop 스크립트에 구매 요청
            shopManager.UpdatePlayerGoldUI();
        }
    }

    // 아이템 구매 여부에 따라 UI 상태 변경
    public void SetPurchasedState(bool isPurchased)
    {
        if (purchasedOverlay != null)
        {
            purchasedOverlay.SetActive(isPurchased); // SOLD OUT 등의 오버레이 활성화/비활성화
        }
        buyButton.interactable = !isPurchased; // 구매했으면 버튼 비활성화

        // 구매했으면 버튼 배경색 변경 (옵션)
        if (isPurchased)
        {
            if (buyButtonBackground != null) buyButtonBackground.color = disabledColor;
        }
        else // 구매 안 했으면 다시 원래 색깔로 (골드 부족 상태는 여기서 처리하지 않고 SetAffordableState에서 처리)
        {
            if (buyButtonBackground != null) buyButtonBackground.color = normalColor;
        }
    }

    // 골드 부족 여부에 따라 UI 상태 변경
    public void SetAffordableState(bool canAfford)
    {
        // 이미 구매한 아이템이라면, 골드 여부는 더 이상 중요하지 않음
        if (purchasedOverlay != null && purchasedOverlay.activeSelf) return;

        buyButton.interactable = canAfford; // 골드 없으면 버튼 비활성화

        // 골드 부족 시 버튼 색상 변경 (옵션)
        if (buyButtonBackground != null)
        {
            buyButtonBackground.color = canAfford ? normalColor : notAffordableColor;
        }
    }
}