using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class ShopPurchaseMessageBox : MonoBehaviour
    {
        TextMeshProUGUI npcItmeText;     // NPC의 아이템 언급 텍스트

        Image itemIcon;                  // 아이템의 아이콘
        TextMeshProUGUI itemTotalCount;  // 아이템의 총 구매 개수
        TextMeshProUGUI itemName;        // 아이템 이름
        TextMeshProUGUI itemTotalPrice;  // 아이템의 총 구매 가격

        InventoryItem currentItem;       // 현재 구매하려는 아이템
        int purchaseCount;               // 구매하려는 아이템 개수

        private void Awake()
        {
            LoadObjects();
        }

        void Start()
        {

        }

        // 관련 오브젝트 불러오기
        void LoadObjects()
        {
            npcItmeText = GameObject.Find("PurchaseMessageBoxNPCItmeText").GetComponent<TextMeshProUGUI>();

            itemIcon = GameObject.Find("PurchaseMessageBoxItemIconImage").GetComponent<Image>();
            itemTotalCount = GameObject.Find("PurchaseMessageBoxItemCountText").GetComponent<TextMeshProUGUI>();
            itemName = GameObject.Find("PurchaseMessageBoxItemNameText").GetComponent<TextMeshProUGUI>();
            itemTotalPrice = GameObject.Find("PurchaseMessageBoxItemPriceText").GetComponent<TextMeshProUGUI>();
        }

        // 구매 메세지 박스 세팅
        public void SetPurchaseMessageBox(InventoryItem item)
        {
            currentItem = item;
            purchaseCount = 1; // 구매 개수는 1부터 시작

            npcItmeText.text = string.Format("정말 \'{0}\'을 구매 할거야?", item.ItemName);

            itemIcon.sprite = item.ItemIcon;
            itemTotalCount.text = purchaseCount.ToString();
            itemName.text = item.ItemName;
            itemTotalPrice.text = item.PurchasePrice.ToString();
        }

        // 구매하려는 아이템 개수와 총 구매 가격 변경
        public void ChangeCountAndPrice()
        {
            itemTotalCount.text = purchaseCount.ToString();
            itemTotalPrice.text = (currentItem.PurchasePrice * purchaseCount).ToString();
        }

        // 10개 감소 버튼을 클릭한 경우
        public void ClickSub10()
        {
            // 총 구매 개수를 10 감소시키고 1 미만이 된 경우 1로 수정
            purchaseCount -= 10;
            if (purchaseCount < 1) purchaseCount = 1;

            // 구매하려는 아이템 개수와 총 구매 가격 변경
            ChangeCountAndPrice();
        }

        // 1개 감소 버튼을 클릭한 경우
        public void ClickSub1()
        {
            // 총 구매 개수를 10 감소시키고 1 미만이 된 경우 1로 수정
            purchaseCount -= 1;
            if (purchaseCount < 1) purchaseCount = 1;

            // 구매하려는 아이템 개수와 총 구매 가격 변경
            ChangeCountAndPrice();
        }

        // 1개 증가 버튼을 클릭한 경우
        public void ClickAdd1()
        {
            // 총 구매 개수를 1 증가시키고 99 초과가 된 경우 99로 수정
            purchaseCount += 1;
            if (purchaseCount > 99) purchaseCount = 99;

            // 구매하려는 아이템 개수와 총 구매 가격 변경
            ChangeCountAndPrice();
        }

        // 10개 증가 버튼을 클릭한 경우
        public void ClickAdd10()
        {
            // 총 구매 개수를 10 증가시키고 99 초과가 된 경우 99로 수정
            purchaseCount += 10;
            if (purchaseCount > 99) purchaseCount = 99;

            // 구매하려는 아이템 개수와 총 구매 가격 변경
            ChangeCountAndPrice();
        }

        // 구매한다는 버튼을 클릭한 경우
        public void ClickButtonYes()
        {
            if (currentItem.PurchasePrice * purchaseCount <= GameManager.instance.Oxygen)
            {
                // 보유한 산소가 해당 아이템의 총 구매 가격 이상인 경우

                // 해당 아이템 인벤토리에 등록 후 보유한 산소를 구매 금액만큼 감소
                GameManager.instance.AddInventoryItem(currentItem, purchaseCount);
                GameManager.instance.Oxygen -= (currentItem.PurchasePrice * purchaseCount);

                // 유저 인벤토리 슬롯과 보유 게임 재화 세팅
                UserInventorySlotsManager.instance.SetSlots();
                UserInventoryMoneyManager.instance.SetUserInventoryMoney();

                // 해당 메세지 창 비활성화
                gameObject.SetActive(false);
            }
            else
            {
                // 보유한 산소가 해당 아이템의 총 구매 가격보다 적은 경우
                Debug.Log("재화 부족");
            }
        }

        // 구매하지 않는다는 버튼을 클릭한 경우
        public void ClickButtonNo()
        {
            // 해당 메세지 창 비활성화
            gameObject.SetActive(false);
        }
    }
}