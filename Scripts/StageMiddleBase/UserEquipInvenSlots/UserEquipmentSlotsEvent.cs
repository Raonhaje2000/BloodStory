using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace RSA
{
    public class UserEquipmentSlotsEvent : MonoBehaviour, IPointerClickHandler
    {
        EquipmentItem equipItem; // 해당 슬롯의 장비 아이템

        public EquipmentItem EquipItem
        {
            get { return equipItem; }
            set { equipItem = value; }
        }

        // 슬롯 초기화
        public void InitializedSlots(EquipmentItem item)
        {
            // 해당 장비아이템을 슬롯에 등록
            equipItem = item;

            // 슬롯 이미지 변경
            GetComponent<Image>().sprite = item.ItemIcon;
        }

        // 마우스 클릭 이벤트 처리
        public void OnPointerClick(PointerEventData eventData)
        {
            // 마우스 오른쪽 클릭을 했을때만 동작하도록 함
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                // 강화 상태인 경우 클릭된 아이템을 강화 하려는 장비로 등록함
                if (ReinforcementManager.instance != null) ReinforcementManager.instance.SetCurrentEquipment(equipItem);

                // 속성 부여 상태인 경우 클릭된 아이템을 속성 부여하려는 장비로 등록함
                if (AttributeManager.instance != null) AttributeManager.instance.SetCurrentEquipment(equipItem);
            }
        }
    }
}
