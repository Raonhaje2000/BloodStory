using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class ShopRepurchaseMessageBox : MonoBehaviour
    {
        TextMeshProUGUI npcItmeText;     // NPC의 아이템 언급 텍스트
        TextMeshProUGUI npcCountText;    // NPC의 개수 언급 텍스트

        Image itemIcon;                  // 아이템의 아이콘
        TextMeshProUGUI itemTotalCount;  // 아이템의 총 재구매 개수
        TextMeshProUGUI itemName;        // 아이템 이름
        TextMeshProUGUI itemTotalPrice;  // 아이템의 총 재구매 가격

        InventoryItem currentItem;       // 현재 재구매하려는 아이템
        int repurchaseCount;             // 재구매하려는 아이템 개수
        int currentSlotIndex;            // 현재 재구매하려는 아이템의 슬롯 인덱스

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
            npcItmeText = GameObject.Find("RepurchaseMessageBoxNPCItmeText").GetComponent<TextMeshProUGUI>();
            npcCountText = GameObject.Find("RepurchaseMessageBoxNPCCountText").GetComponent<TextMeshProUGUI>();

            itemIcon = GameObject.Find("RepurchaseMessageBoxItemIconImage").GetComponent<Image>();
            itemTotalCount = GameObject.Find("RepurchaseMessageBoxItemCountText").GetComponent<TextMeshProUGUI>();
            itemName = GameObject.Find("RepurchaseMessageBoxItemNameText").GetComponent<TextMeshProUGUI>();
            itemTotalPrice = GameObject.Find("RepurchaseMessageBoxItemPriceText").GetComponent<TextMeshProUGUI>();
        }

        // 재구매 메세지 박스 세팅
        public void SetRepurchaseMessageBox(InventoryItem item, int slotIndex)
        {
            currentItem = item;
            currentSlotIndex = slotIndex;
            repurchaseCount = ShopManager.instance.RepurchaseItemsCount[slotIndex];

            npcItmeText.text = string.Format("정말 \'{0}\'을 재구매 할거야?", item.ItemName);
            npcCountText.text = string.Format("총 {0}개 있는데 다 구매해.", repurchaseCount);

            itemIcon.sprite = item.ItemIcon;
            itemTotalCount.text = repurchaseCount.ToString();
            itemName.text = item.ItemName;
            itemTotalPrice.text = item.RepurchasePrice.ToString();
        }

        // 재구매한다는 버튼을 클릭한 경우
        public void ClickButtonYes()
        {
            if (currentItem.RepurchasePrice * repurchaseCount <= GameManager.instance.Oxygen)
            {
                // 보유한 산소가 해당 아이템의 총 재구매 가격 이상인 경우

                // 해당 아이템 인벤토리에 등록 후 보유한 산소를 재구매 금액만큼 감소
                GameManager.instance.AddInventoryItem(currentItem, repurchaseCount);
                GameManager.instance.Oxygen -= (currentItem.RepurchasePrice * repurchaseCount);

                // 상점의 재구매 아이템 목록에서 해당 아이템 제거 (해당 아이템의 개수 목록에서도 제거)
                ShopManager.instance.RepurchaseItems.RemoveAt(currentSlotIndex);
                ShopManager.instance.RepurchaseItemsCount.RemoveAt(currentSlotIndex);

                // 유저 인벤토리 슬롯과 보유 게임 재화 세팅
                UserInventorySlotsManager.instance.SetSlots();
                UserInventoryMoneyManager.instance.SetUserInventoryMoney();

                // 상점 아이템 슬롯 세팅
                ShopManager.instance.SetItemSlots();

                // 해당 메세지 창 비활성화
                gameObject.SetActive(false);
            }
            else
            {
                // 보유한 산소가 해당 아이템의 총 재구매 가격보다 적은 경우
                Debug.Log("재화 부족");
            }
        }

        // 재구매하지 않는다는 버튼을 클릭한 경우
        public void ClickButtonNo()
        {
            // 해당 메세지 창 비활성화
            gameObject.SetActive(false);
        }
    }
}
