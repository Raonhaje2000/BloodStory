using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RSA
{
    public class ShopSlotsEvent : MonoBehaviour, IPointerClickHandler
    {
        InventoryItem shopItem; // 상점 슬롯의 아이템

        int shopSlotIndex;      // 상점 슬롯의 인덱스

        // 해당 아이템으로 상점 슬롯의 아이템 세팅
        public void InitializeSlots(InventoryItem item, int index)
        {
            shopItem = item;
            shopSlotIndex = index;
        }

        // 마우스 클릭 처리
        public void OnPointerClick(PointerEventData eventData)
        {
            if (shopItem != null)
            {
                // 상점 아이템이 있는 경우 해당 아이템 구매
                ShopManager.instance.PurchaseItem(shopItem, shopSlotIndex);
            }
        }
    }
}
