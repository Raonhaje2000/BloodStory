using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace RSA
{
    public class UserInventorySlotsEvent : MonoBehaviour, IPointerClickHandler
    {
        InventoryItem invenItem; // 인벤토리 아이템

        public InventoryItem InvenItem
        { 
            get { return invenItem; }
            set { invenItem = value; }
        }

        // 인벤토리 슬롯 초기화
        public void InitializeSlots(InventoryItem item)
        {
            // 해당 인벤토리 아이템을 슬롯에 등록
            invenItem = item;

            // 인벤토리 아이템이 null이 아닌 경우
            if (invenItem != null)
            {
                // 슬롯 이미지와 아이템 개수 텍스트 변경
                GetComponent<Image>().sprite = item.ItemIcon;
                GetComponentInChildren<TextMeshProUGUI>().text = item.InvenCurrentCount.ToString();
            }
        }

        // 마우스 클릭 이벤트 처리
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("아이템: " + invenItem.ItemName + " / 현재 개수 : " + invenItem.InvenCurrentCount);

            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if(InventoryItemSettingManager.instance != null
                        && (invenItem.ItemType == Item.Type.recovery || invenItem.ItemType == Item.Type.attack || invenItem.ItemType == Item.Type.aggro))
                {
                    // 아이템 세팅 UI가 활성화된 상태에서 회복 아이템, 공격 아이템, 어그로 아이템을 클릭 한 경우
                    // 아이템 세팅 슬롯에 클릭한 아이템 등록
                    InventoryItemSettingManager.instance.AddItemSettingSlot(invenItem);
                }
                if (AttributeManager.instance != null && invenItem.ItemType == Item.Type.attributeMaterial)
                {
                    // 속성 부여 UI가 활성화 된 상태에서 속성 부여 아이템을 클릭한 경우

                    AttributeItem attrbuteiItem = (AttributeItem)invenItem; // 언박싱

                    if (AttributeManager.instance.IsInstallationPossible(attrbuteiItem))
                    {
                        // 해당 속성 부여 아이템을 사용해 속성 부여가 가능할 경우 해당 속성 부여 아이템을 사용해 속성을 부여함
                        // 슬롯에 관련된 데이터를 넣는 이유는 아이템을 장착하기 전 장착할 것인지 확인 메세지를 보내는데, 장착하겠다고 한 경우에만 해당 슬롯의 속성 아이템을 제거하기 위함
                        AttributeManager.instance.InstallAttribute(attrbuteiItem, this);
                    }
                }
                if(ShopManager.instance != null)
                {
                    // 상점 UI가 활성화 된 상태에서 아이템을 클릭한 경우

                    // 해당 아이템 판매
                    ShopManager.instance.SellItem(invenItem);
                }
            }
        }

        // 유저가 속성 부여 아이템을 사용했을 경우 슬롯 UI 변경
        public void UseAtAttributeUI()
        {
            // 해당 슬롯의 속성 부여 아이템 사용
            invenItem.UseInventoryItem();

            if (invenItem.InvenCurrentCount == 0)
            {
                // 해당 슬롯의 속성 부여 아이템 개수가 0이 되었으면

                // 인벤토리에서 해당 속성부여 아이템이 있는 위치를 찾아 속성 부여 아이템 제거
                int index = GameManager.instance.InventoryItems.IndexOf(invenItem);
                GameManager.instance.InventoryItems.RemoveAt(index);

                // 슬롯 초기화 (아이템 아이콘 및 개수 변경을 위함)
                UserInventorySlotsManager.instance.SetSlots();
            }
        }
    }
}
