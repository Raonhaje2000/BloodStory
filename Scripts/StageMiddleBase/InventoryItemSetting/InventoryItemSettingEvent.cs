using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RSA
{
    public class InventoryItemSettingEvent : MonoBehaviour, IPointerClickHandler
    {
        InventoryItem settingItem; // 세팅된 인벤토리 아이템

        void Start()
        {

        }

        // 세팅 아이템 슬롯 초기화
        public void SetItemSettingSlot(InventoryItem item)
        {
            // 해당 인벤토리 아이템을 세팅 아이템 슬롯에 등록
            settingItem = item;
        }

        // 마우스 클릭 이벤트 처리
        public void OnPointerClick(PointerEventData eventData)
        {
            if(settingItem != null)
            {
                // 세팅된 인벤토리 아이템이 존재하는 경우
                // 해당 슬롯에서 아이템 제거 (null로 초기화)
                InventoryItemSettingManager.instance.RemoveItemSettingSlot(settingItem);
                settingItem = null;
            }
        }
    }
}
