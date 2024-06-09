using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class ShopSellingMessageBox : MonoBehaviour
    {
        TextMeshProUGUI npcItmeText;    // NPC의 아이템 언급 텍스트
        TextMeshProUGUI npcCountText;   // NPC의 개수 언급 텍스트

        Image itemIcon;                 // 아이템의 아이콘
        TextMeshProUGUI itemTotalCount; // 아이템의 총 판매 개수
        TextMeshProUGUI itemName;       // 아이템 이름
        TextMeshProUGUI itemTotalPrice; // 아이템의 총 판매 가격

        InventoryItem currentItem;      // 현재 판구매하려는 아이템
        int sellingCount;               // 판매하려는 아이템 개수
        int inventoryTotalCount;        // 판매하려는 아이템의 인벤토리에 보유한 총 개수

        private void Awake()
        {
            // 관련 오브젝트 불러오기
            LoadObjects();
        }

        void Start()
        {

        }

        // 관련 오브젝트 불러오기
        void LoadObjects()
        {
            npcItmeText = GameObject.Find("SellingMessageBoxNPCItmeText").GetComponent<TextMeshProUGUI>();
            npcCountText = GameObject.Find("SellingMessageBoxNPCCountText").GetComponent<TextMeshProUGUI>();

            itemIcon = GameObject.Find("SellingMessageBoxItemIconImage").GetComponent<Image>();
            itemTotalCount = GameObject.Find("SellingMessageBoxItemCountText").GetComponent<TextMeshProUGUI>();
            itemName = GameObject.Find("SellingMessageBoxItemNameText").GetComponent<TextMeshProUGUI>();
            itemTotalPrice = GameObject.Find("SellingMessageBoxItemPriceText").GetComponent<TextMeshProUGUI>();
        }

        // 판매 메세지 박스 세팅
        public void SetSellingMessageBox(InventoryItem item)
        {
            currentItem = item;
            sellingCount = 1;
            inventoryTotalCount = GameManager.instance.FindItemTotalCount(item); // 인벤토리 내에서 해당하는 아이템의 총 보유 개수 구하기

            npcItmeText.text = string.Format("정말 \'{0}\'을 판매 할거야?", item.ItemName);
            npcCountText.text = string.Format("인벤토리에 총 {0}개 있어.", inventoryTotalCount);

            itemIcon.sprite = item.ItemIcon;
            itemTotalCount.text = sellingCount.ToString();
            itemName.text = item.ItemName;
            itemTotalPrice.text = item.SellingPrice.ToString();
        }

        // 구매하려는 아이템 개수와 총 구매 가격 변경
        public void ChangeCountAndPrice()
        {
            itemTotalCount.text = sellingCount.ToString();
            itemTotalPrice.text = (currentItem.SellingPrice * sellingCount).ToString();
        }

        // 10개 감소 버튼을 클릭한 경우
        public void ClickSub10()
        {
            // 총 판매 개수를 10 감소시키고 1 미만이 된 경우 1로 수정
            sellingCount -= 10;
            if (sellingCount < 1) sellingCount = 1;

            // 판매하려는 아이템 개수와 총 판매 가격 변경
            ChangeCountAndPrice();
        }

        public void ClickSub1()
        {
            // 총 판매 개수를 1 감소시키고 1 미만이 된 경우 1로 수정
            sellingCount -= 1;
            if (sellingCount < 1) sellingCount = 1;

            // 판매하려는 아이템 개수와 총 판매 가격 변경
            ChangeCountAndPrice();
        }

        public void ClickAdd1()
        {
            // 총 판매 개수를 1 증가시키고 인벤토리에 보유한 총 개수보다 초과가 된 경우 인벤토리에 보유한 총 개수로 수정
            sellingCount += 1;
            if (sellingCount > inventoryTotalCount) sellingCount = inventoryTotalCount;

            // 판매하려는 아이템 개수와 총 판매 가격 변경
            ChangeCountAndPrice();
        }

        public void ClickAdd10()
        {
            // 총 판매 개수를 10 증가시키고 인벤토리에 보유한 총 개수보다 초과가 된 경우 인벤토리에 보유한 총 개수로 수정
            sellingCount += 10;
            if (sellingCount > inventoryTotalCount) sellingCount = inventoryTotalCount;

            // 판매하려는 아이템 개수와 총 판매 가격 변경
            ChangeCountAndPrice();
        }

        // 판매한다는 버튼을 클릭한 경우
        public void ClickButtonYes()
        {
            // 인벤토리에서 해당 아이템을 판매한 개수만큼 제거하고 총 판매 금액만큼 보유한 산소량 증가
            GameManager.instance.RemoveInventoryItem(currentItem, sellingCount);
            GameManager.instance.Oxygen += (currentItem.SellingPrice * sellingCount);

            if (ShopManager.instance.RepurchaseItems.Count == ShopManager.SHOP_SLOTS_MAX)
            {
                // 재구매 아이템 목록 수가 상점 최대 수가 되는 경우 목록 맨 앞의 아이템 제거
                // 최근에 판매한 아이템 기준 8개만 보이도록 하기 위함
                ShopManager.instance.RepurchaseItems.RemoveAt(0);
                ShopManager.instance.RepurchaseItemsCount.RemoveAt(0);
            }

            // 재구매 아이템 목록에 해당 아이템 등록 후 개수 목록에 판매한 개수 저장
            ShopManager.instance.RepurchaseItems.Add(currentItem);
            ShopManager.instance.RepurchaseItemsCount.Add(sellingCount);

            // 유저 인벤토리 슬롯과 보유 게임 재화 세팅
            UserInventorySlotsManager.instance.SetSlots();
            UserInventoryMoneyManager.instance.SetUserInventoryMoney();

            // 상점 아이템 슬롯 세팅
            ShopManager.instance.SetItemSlots();

            // 해당 메세지 창 비활성화
            gameObject.SetActive(false);
        }

        // 판매하지 않는다는 버튼을 클릭한 경우
        public void ClickButtonNo()
        {
            // 해당 메세지 창 비활성화
            gameObject.SetActive(false);
        }
    }
}